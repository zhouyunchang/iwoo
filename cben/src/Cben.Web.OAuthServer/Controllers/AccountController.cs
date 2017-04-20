using Cben.Authorization.Users;
using Cben.Configuration.Startup;
using Cben.Core.Authorization;
using Cben.Core.MultiTenancy;
using Cben.Core.Users;
using Cben.Domain.Repositories;
using Cben.Domain.Uow;
using Cben.UI;
using Cben.Web.Models.Account;
using Cben.Zero.OAuth2;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.OAuth;

namespace Cben.Web.Controllers
{
    [Authorize]
    public class AccountController : ControllerBase
    {

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Client, int> _clientRepository;
        private readonly LogInManager _logInManager;
        private readonly UserManager _userManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;


        public AccountController(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Client, int> clientRepository,
            LogInManager logInManager,
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _clientRepository = clientRepository;
            _logInManager = logInManager;
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            return View(new LoginFormViewModel
            {
                ReturnUrl = returnUrl,
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            var isPersistent = !string.IsNullOrEmpty(Request.Form.Get("RememberMe"));
            // 作为示例程序， 这里没有对用户进行验证， 直接登录用户输入的账户。
            var userName = Request.Form["UserSuppliedIdentifier"];
            var password = Request.Form["Password"];
            var tenancyName = Request.Form["TenancyName"];
            string retUrl = Request.QueryString["ReturnUrl"];

            var loginResult = await GetLoginResultAsync(userName, password, tenancyName);

            if (!string.IsNullOrEmpty(Request.Form.Get("submit.Signin")))
            {
                await SignInAsync(loginResult.User, loginResult.Identity, isPersistent);
                
                return Redirect(retUrl);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        private async Task<CbenLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case CbenLoginResultType.Success:
                    return loginResult;
                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        }

        private Exception CreateExceptionForFailedLoginAttempt(CbenLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case CbenLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case CbenLoginResultType.InvalidUserNameOrEmailAddress:
                case CbenLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case CbenLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case CbenLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case CbenLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case CbenLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), "Your email address is not confirmed. You can not login"); //TODO: localize message
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}