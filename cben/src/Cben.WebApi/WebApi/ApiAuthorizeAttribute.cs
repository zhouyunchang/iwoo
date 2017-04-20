﻿using Cben.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Cben.WebApi
{
    /// <summary>
    /// This attribute is used on a method of an <see cref="ApiController"/>
    /// to make that method usable only by authorized users.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CbenApiAuthorizeAttribute : AuthorizeAttribute, ICbenAuthorizeAttribute
    {
        /// <inheritdoc/>
        public string[] Permissions { get; set; }

        /// <inheritdoc/>
        public bool RequireAllPermissions { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="CbenApiAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        public CbenApiAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                base.HandleUnauthorizedRequest(actionContext);
                return;
            }

            httpContext.Response.StatusCode = httpContext.User.Identity.IsAuthenticated == false
                                      ? (int)System.Net.HttpStatusCode.Unauthorized
                                      : (int)System.Net.HttpStatusCode.Forbidden;

            httpContext.Response.SuppressFormsAuthenticationRedirect = true;
            httpContext.Response.End();
        }
    }
}