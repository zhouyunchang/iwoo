using Cben.Application.Users.Dto;
using Cben.AutoMapper;
using Cben.Configuration.Startup;
using Cben.Core.Authorization;
using Cben.Core.MultiTenancy;
using Cben.Core.Users;
using Cben.Domain.Uow;
using Cben.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Cben.WebApi.Controllers
{
    [RoutePrefix("api/Identity")]
    public class IdentityController : ApiControllerBase
    {
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly LogInManager _logInManager;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        public IdentityController(
            TenantManager tenantManager,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager,
            IMultiTenancyConfig multiTenancyConfig,
            LogInManager loginManager
            )
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _multiTenancyConfig = multiTenancyConfig;
            _logInManager = loginManager;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _userManager.CreateAsync(new User
            {
                TenantId = null,
                UserName = userModel.UserName,
                Name = userModel.UserName,
                Surname = "",
                EmailAddress = "",
                Password = new PasswordHasher().HashPassword(userModel.Password)
            });

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }


        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserListDto> GetUserInfo()
        {
            if (CbenSession.UserId.HasValue)
            {
                var user = await _userManager.GetUserByIdAsync(CbenSession.UserId.Value);
                return user.MapTo<UserListDto>();
            }
            else
            {
                return null;
            }
        }


        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
