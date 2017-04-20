using Cben.Core.Users;
using Cben.Domain.Repositories;
using Cben.Domain.Uow;
using Cben.Web.Models;
using Cben.Zero.OAuth2;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Cben.Web.Controllers
{
    public class AuthorizeController : ControllerBase
    {

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserManager _userManager;
        private readonly IRepository<Client, int> _clientRepository;

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        static AuthorizeController()
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
        }

        public AuthorizeController(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Client, int> clientRepository,
            UserManager userManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _clientRepository = clientRepository;
            _userManager = userManager;
        }

        // GET: Authorize
        public async Task<ActionResult> Index()
        {
            if (Response.StatusCode != 200)
            {
                return View("Error");
            }

            var ticket = AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie).Result;
            var identity = ticket != null ? ticket.Identity : null;
            if (identity == null)
            {
                AuthenticationManager.Challenge(DefaultAuthenticationTypes.ApplicationCookie);
                return new HttpUnauthorizedResult();
            }

            var scopes = (Request.QueryString.Get("scope") ?? "").Split(' ');
            var clientId = Request.QueryString.Get("client_id");
            var redirectUrl = Request.QueryString.Get("redirect_uri");

            if (Request.HttpMethod == "POST")
            {
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Grant")))
                {
                    identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType, identity.RoleClaimType);
                    foreach (var scope in scopes)
                    {
                        identity.AddClaim(new Claim("urn:oauth:scope", scope));
                    }
                    AuthenticationManager.SignIn(identity);

                    // save ClientAuthorization
                    await SaveClientAuthorization(clientId, scopes);

                }
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Login")))
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.Challenge(DefaultAuthenticationTypes.ApplicationCookie);
                    return new HttpUnauthorizedResult();
                }
            }

            return View();

        }

        private async Task SaveClientAuthorization(string clientId, string[] scopes)
        {
            if (CbenSession.UserId != null)
            {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    var client = _clientRepository.GetAll().Where(m => m.ClientIdentifier == clientId).FirstOrDefault();
                    var user = await _userManager.GetUserByIdAsync(CbenSession.UserId.Value);
                    var clientAuthorization = client.Authorizations.Where(i => i.ClientId == client.Id && i.UserId == user.Id).FirstOrDefault();
                    if (clientAuthorization == null)
                    {
                        clientAuthorization = new ClientAuthorization
                        {
                            ClientId = client.Id,
                            UserId = user.Id,
                            CreatedOnUtc = DateTime.UtcNow,
                            ExpirationDateUtc = DateTime.UtcNow.AddYears(1),
                            Scope = string.Join(" ", scopes)
                        };
                        client.Authorizations.Add(clientAuthorization);
                    }
                    else
                    {
                        clientAuthorization.CreatedOnUtc = DateTime.UtcNow;
                        clientAuthorization.ExpirationDateUtc = DateTime.UtcNow.AddYears(1);
                        clientAuthorization.Scope = string.Join(" ", scopes);
                    }

                    uow.Complete();
                }
            }
        }
    }
}