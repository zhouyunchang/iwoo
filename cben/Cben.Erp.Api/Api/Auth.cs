using Cben.Erp.Exceptions;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cben.Erp.Api
{
    public class Auth : IAuth
    {

        private const string _AUTH_SESSION_KEY = "__OAUTH_STATE__";

        private static AuthorizationServerDescription authServer = new AuthorizationServerDescription()
        {
            AuthorizationEndpoint = new Uri("http://localhost:54443/authorize"),
            TokenEndpoint = new Uri("http://localhost:54443/token"),
        };

        private static string _clientId = "CbenWeb";
        private static string _clientSecret = "CbenWeb";
        private static WebServerClient _client = new WebServerClient(authServer, _clientId, _clientSecret);

        public static string Callback;

        private IAuthorizationState _authState;

        public Auth()
        {
            _authState = HttpContext.Current.Session[_AUTH_SESSION_KEY] as IAuthorizationState;
        }

        public void Authorization(params string[] scopes)
        {
            if (_authState == null)
                _authState = HttpContext.Current.Session[_AUTH_SESSION_KEY] as IAuthorizationState;

            if (_authState == null)                             // 无授权
            {
                var response = GetToken(scopes);
                throw new NeedAuthorizationException("unauthorize", response);
            }
            else
            {
                if (!_authState.Scope.IsSubsetOf(scopes))       // 未包括必要Scope, 需要刷新授权
                {
                    foreach (var scope in scopes)
                        _authState.Scope.Add(scope);

                    RefreshToken(true);
                }
                else
                {
                    RefreshToken();
                }
            }
        }

        public static void ProcessUserAuthorization(HttpRequestBase request)
        {
            var state = _client.ProcessUserAuthorization(request);
            if (state == null)
            {
                throw new HttpException((int)HttpStatusCode.Unauthorized, "oauth invalid");
            }
            HttpContext.Current.Session[_AUTH_SESSION_KEY] = state;
        }

        private OutgoingWebResponse GetToken(params string[] scopes)
        {
            if (_authState == null) _authState = new AuthorizationState();

            foreach (var scope in scopes)
            {
                _authState.Scope.Add(scope);
            }
            _authState.Callback = new Uri(getBaseUri(), Callback);
            return _client.PrepareRequestUserAuthorization(_authState);
        }

        private static Uri getBaseUri()
        {
            return new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        private void RefreshToken(bool force = false)
        {
            if (force)          // 强制刷新 AccessToken, 忽略过期时间
            {
                if (_client.RefreshAuthorization(_authState))
                    HttpContext.Current.Session[_AUTH_SESSION_KEY] = _authState;
            }
            else if (_authState.AccessTokenExpirationUtc.HasValue)
            {
                if (_client.RefreshAuthorization(_authState, TimeSpan.FromSeconds(30)))
                    HttpContext.Current.Session[_AUTH_SESSION_KEY] = _authState;
            }
        }

        public void Authorize(HttpWebRequest request)
        {
            Authorization();

            _client.AuthorizeRequest(request, _authState);
        }


    }
}
