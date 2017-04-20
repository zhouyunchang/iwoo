using Cben.Core.Authorization;
using Cben.Dependency;
using Cben.Domain.Repositories;
using Cben.Domain.Uow;
using Cben.Threading;
using Cben.Web.Controllers;
using Cben.Zero.OAuth2;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Cben.Web
{

    public static class Paths
    {
        public const string LoginPath = "/Account/Login";
        public const string LogoutPath = "/Account/Logout";

        public const string AuthorizePath = "/Authorize";
        public const string TokenPath = "/Token";

    }

    public partial class Startup
    {

        private readonly ConcurrentDictionary<string, string> authenticationCodes = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        private void ConfigureAuth(IAppBuilder app)
        {
            //app.UseOAuthBearerAuthentication(AuthorizeController.OAuthBearerOptions);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(Paths.LoginPath),
                LogoutPath = new PathString(Paths.LogoutPath),
                ReturnUrlParameter = "ReturnUrl"
            });

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AuthorizeEndpointPath = new PathString(Paths.AuthorizePath),
                TokenEndpointPath = new PathString(Paths.TokenPath),
                ApplicationCanDisplayErrors = true,
                AllowInsecureHttp = true,

                // Authorization server provider which controls the lifecycle of Authorization Server
                Provider = new OAuthAuthorizationServerProvider
                {
                    OnValidateClientRedirectUri = ValidateClientRedirectUri,
                    OnValidateAuthorizeRequest = ValidateAuthorizeRequest,
                    OnValidateClientAuthentication = ValidateClientAuthentication,
                    OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
                    OnGrantClientCredentials = GrantClientCredetails
                },

                // Authorization code provider which creates and receives authorization code
                AuthorizationCodeProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateAuthenticationCode,
                    OnReceive = ReceiveAuthenticationCode,
                },

                // Refresh token provider which creates and receives referesh token
                RefreshTokenProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateRefreshToken,
                    OnReceive = ReceiveRefreshToken,
                }
            });
        }

        private Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            string clientId = context.ClientId;

            var unitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();
            var clientRepo = IocManager.Instance.Resolve<IRepository<Client, int>>();
            using (var uow = unitOfWorkManager.Begin())
            {
                // 验证客户端标识与安全码
                var client = clientRepo.GetAll()
                    .Where(i => i.ClientIdentifier == clientId)
                    .FirstOrDefault();

                if (client != null)
                {
                    // 验证客户端标识与重定向地址
                    if (context.RedirectUri != null &&
                        context.RedirectUri.Equals(client.Callback, StringComparison.OrdinalIgnoreCase))
                    {
                        context.Validated();
                    }
                    else
                    {
                        context.Validated(client.Callback);
                    }
                }

                uow.Complete();
            }
            IocManager.Instance.Release(clientRepo);
            IocManager.Instance.Release(unitOfWorkManager);

            return Task.FromResult(0);
        }

        private Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            context.Validated();

            return Task.FromResult(0);
        }

        private void ReceiveRefreshToken(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }

        private void CreateRefreshToken(AuthenticationTokenCreateContext context)
        {
            context.SetToken(context.SerializeTicket());
        }

        private void ReceiveAuthenticationCode(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (authenticationCodes.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }

        private void CreateAuthenticationCode(AuthenticationTokenCreateContext context)
        {
            var tokenValue = Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n");
            context.SetToken(tokenValue);
            var authenticationCode = context.SerializeTicket();
            authenticationCodes[context.Token] = authenticationCode;
        }

        private Task GrantClientCredetails(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(
                new GenericIdentity(context.ClientId, OAuthDefaults.AuthenticationType),
                context.Scope.Select(x => new Claim("urn:oauth:scope", x))
            );
            context.Validated(identity);

            return Task.FromResult(0);
        }

        private Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            string userName = context.UserName;
            string password = context.Password;

            var unitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();
            var logInManager = IocManager.Instance.Resolve<LogInManager>();
            using (var uow = unitOfWorkManager.Begin())
            {
                var loginResult = AsyncHelper.RunSync(() => logInManager.LoginAsync(userName, password));
                if (loginResult != null && loginResult.Identity != null)
                {
                    var identity = loginResult.Identity;

                    loginResult.Identity.AddClaims(context.Scope.Select(x => new Claim("urn:oauth:scope", x)));

                    identity = new ClaimsIdentity(loginResult.Identity.Claims, OAuthDefaults.AuthenticationType);

                    context.Validated(identity);

                }
                else
                {
                    context.Rejected();
                }
            }
            IocManager.Instance.Release(logInManager);
            IocManager.Instance.Release(unitOfWorkManager);

            return Task.FromResult(0);
        }

        private Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (context.TryGetBasicCredentials(out clientId, out clientSecret) ||
                context.TryGetFormCredentials(out clientId, out clientSecret))
            {
                var unitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();
                var clientRepo = IocManager.Instance.Resolve<IRepository<Client, int>>();
                using (var uow = unitOfWorkManager.Begin())
                {

                    // 验证客户端标识与安全码
                    var client = clientRepo.GetAll()
                                .Where(i => i.ClientIdentifier == clientId && i.ClientSecret == clientSecret)
                                .FirstOrDefault();

                    if (client != null)
                    {
                        context.Validated();
                    }

                    uow.Complete();
                }
                IocManager.Instance.Release(clientRepo);
                IocManager.Instance.Release(unitOfWorkManager);
            }
            return Task.FromResult(0);
        }



    }


}