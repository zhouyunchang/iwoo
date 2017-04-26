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

namespace Cben.Erp.Web.Controllers
{
    public class ControllerBase : Controller
    {
        static ControllerBase()
        {
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