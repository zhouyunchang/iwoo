using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Cben.WebApi.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {


        [AllowAnonymous]
        public ActionResult Login()
        {
            var authentication = HttpContext.GetOwinContext().Authentication;
            if (Request.HttpMethod == "POST")
            {

                var isPersistent = !string.IsNullOrEmpty(Request.Form.Get("RememberMe"));
                // 作为示例程序， 这里没有对用户进行验证， 直接登录用户输入的账户。
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Signin")))
                {
                    authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = isPersistent },
                        new ClaimsIdentity(
                            new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, Request.Form["UserSuppliedIdentifier"]) },
                            Startup.AuthenticationType)
                    );
                    string retUrl = Request.QueryString["ReturnUrl"];
                    return Redirect(retUrl);
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}