using Cben.Dependency;
using Cben.Domain.Repositories;
using Cben.Domain.Uow;
using Cben.WebApi.Controllers;
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

namespace Cben.WebApi
{

    public static class Paths
    {

        public const string ImplicitGrantCallBackPath = "http://localhost:8080/Owin04_OAuthClient/Implicit/Login";

        public const string AuthorizeCodeCallBackPath = "http://localhost:8080/Owin04_OAuthClient/AuthCode";

        public const string ResourceUserApiPath = "http://localhost:8080/Owin04_OAuthResource/api/user";

        public const string AuthorizationServerBaseAddress = "http://localhost:8080/OWin04_OAuthServer";

        public const string LoginPath = "/Account/Login";
        public const string LogoutPath = "/Account/Logout";

        public const string AuthorizePath = "/Authorize";
        public const string TokenPath = "/Token";

    }

    public partial class Startup
    {

        private static IRepository<Client, int> ClientRepository = IocManager.Instance.Resolve<IRepository<Client, int>>();

        private static IUnitOfWorkManager UnitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();

        private readonly ConcurrentDictionary<string, string> authenticationCodes = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        private readonly static OAuthBearerAuthenticationOptions OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString(Paths.LoginPath),
            //    ReturnUrlParameter = "ReturnUrl"
            //});

            //app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            //{
            //    AuthorizeEndpointPath = new PathString(Paths.AuthorizePath),
            //    TokenEndpointPath = new PathString(Paths.TokenPath),
            //    ApplicationCanDisplayErrors = true,
            //    AllowInsecureHttp = true,

            //    // Authorization server provider which controls the lifecycle of Authorization Server
            //    Provider = new OAuthAuthorizationServerProvider
            //    {
            //        OnValidateClientRedirectUri = ValidateClientRedirectUri,
            //        OnValidateAuthorizeRequest = ValidateAuthorizeRequest,
            //        OnValidateClientAuthentication = ValidateClientAuthentication,
            //        OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
            //        OnGrantClientCredentials = GrantClientCredetails
            //    },

            //    // Authorization code provider which creates and receives authorization code
            //    AuthorizationCodeProvider = new AuthenticationTokenProvider
            //    {
            //        OnCreate = CreateAuthenticationCode,
            //        OnReceive = ReceiveAuthenticationCode,
            //    },

            //    // Refresh token provider which creates and receives referesh token
            //    RefreshTokenProvider = new AuthenticationTokenProvider
            //    {
            //        OnCreate = CreateRefreshToken,
            //        OnReceive = ReceiveRefreshToken,
            //    }
            //});
        }

        private Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            string clientId = context.ClientId;

            using (var uow = UnitOfWorkManager.Begin())
            {
                // 验证客户端标识与安全码
                var client = ClientRepository.GetAll()
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
            var identity = new ClaimsIdentity(
                new GenericIdentity(context.UserName, OAuthDefaults.AuthenticationType),
                context.Scope.Select(x => new Claim("urn:oauth:scope", x))
            );

            context.Validated(identity);

            return Task.FromResult(0);
        }

        private Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (context.TryGetBasicCredentials(out clientId, out clientSecret) ||
                context.TryGetFormCredentials(out clientId, out clientSecret))
            {
                using (var uow = UnitOfWorkManager.Begin())
                {

                    // 验证客户端标识与安全码
                    var client = ClientRepository.GetAll()
                                .Where(i => i.ClientIdentifier == clientId && i.ClientSecret == clientSecret)
                                .FirstOrDefault();

                    if (client != null)
                    {
                        context.Validated();
                    }

                    uow.Complete();
                }
            }
            return Task.FromResult(0);
        }

       

    }


}