using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult CallApi()
        {
            if (!string.IsNullOrEmpty(Request.Form.Get("submit.callapi")))
            {
                var authServer = new AuthorizationServerDescription
                {
                    AuthorizationEndpoint = new Uri("http://localhost:56923/authorize"),
                    TokenEndpoint = new Uri("http://localhost:56923/token")
                };
                var webServerClient = new WebServerClient(authServer, "123456", "abcdef");

                var userAuthorization = webServerClient.PrepareRequestUserAuthorization(new[] { "bio", "notes" });
                userAuthorization.Send(HttpContext);
                Response.End();

                // get access token from request (redirect from oauth server)
                var authorizationState = webServerClient.ProcessUserAuthorization(Request);
                if (authorizationState != null)
                {
                    ViewBag.AccessToken = authorizationState.AccessToken;
                    ViewBag.RefreshToken = authorizationState.RefreshToken;
                    ViewBag.Action = Request.Path;
                }

                //refresh token
                var state = new AuthorizationState
                {
                    AccessToken = Request.Form["AccessToken"],
                    RefreshToken = Request.Form["RefreshToken"]
                };
                if (webServerClient.RefreshAuthorization(state))
                {
                    ViewBag.AccessToken = state.AccessToken;
                    ViewBag.RefreshToken = state.RefreshToken;
                }
            }

            return View();
        }


    }
}