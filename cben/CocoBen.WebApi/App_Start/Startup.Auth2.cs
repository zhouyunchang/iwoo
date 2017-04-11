using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace CocoBen.WebApi
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


        public const string AuthenticationType = "OAuth2";

        private readonly ConcurrentDictionary<string, string> authenticationCodes = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        private void ConfigureAuth2(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = AuthenticationType,
                AuthenticationMode = AuthenticationMode.Passive,
                LoginPath = new PathString(Paths.LoginPath),
                LogoutPath = new PathString(Paths.LogoutPath)
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
                // 验证客户端标识与安全码
                if (clientId == Clients.Client1.Id && clientSecret == Clients.Client1.Secret)
                {
                    context.Validated();
                }
                else if (clientId == Clients.Client2.Id && clientSecret == Clients.Client2.Secret)
                {
                    context.Validated();
                }
            }
            return Task.FromResult(0);
        }

        private Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            // 验证客户端标识与重定向地址
            if (context.ClientId == Clients.Client1.Id)
            {
                context.Validated("http://localhost:61357/home/CallApi");
            }
            else if (context.ClientId == Clients.Client2.Id)
            {
                context.Validated("http://localhost:61357/home/CallApi");
            }
            return Task.FromResult(0);
        }

    }


    #region Clients

    public static class Clients
    {

        public readonly static Client Client1 = new Client
        {
            Id = "123456",
            Secret = "abcdef",
            RedirectUrl = Paths.AuthorizeCodeCallBackPath
        };

        public readonly static Client Client2 = new Client
        {
            Id = "7890ab",
            Secret = "7890ab",
            RedirectUrl = Paths.ImplicitGrantCallBackPath
        };
    }

    public class Client
    {

        public string Id { get; set; }

        public string Secret { get; set; }

        public string RedirectUrl { get; set; }
    }

    #endregion
}