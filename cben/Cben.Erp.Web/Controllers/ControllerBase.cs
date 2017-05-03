using Cben.Erp.Api;
using Cben.Erp.Exceptions;
using Cben.Erp.Web.Models;
using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DotNetOpenAuth.Messaging;
using Cben.Application.Users.Dto;
using System.Web.Mvc.Filters;

namespace Cben.Erp.Web.Controllers
{
    public class ControllerBase : Controller
    {
        static ControllerBase()
        {
        }

        public const string CurrentUserSessionKey = "__CurrentUser__";

        /// <summary>
        /// 当前用户
        /// </summary>
        public UserListDto CurrentUser
        {
            get { return Session[CurrentUserSessionKey] as UserListDto; }
            set { Session[CurrentUserSessionKey] = value; }
        }

        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            base.OnAuthenticationChallenge(filterContext);

            if (CurrentUser == null)
            {
                filterContext.Result = new RedirectResult("/");
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            if (ex is NeedAuthorizationException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = ((NeedAuthorizationException)ex)
                    .OAuthResponse.AsActionResultMvc5();
            }

            base.OnException(filterContext);
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

    }
}