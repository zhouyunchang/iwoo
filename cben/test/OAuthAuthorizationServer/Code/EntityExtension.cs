using Cben.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthAuthorizationServer.Code
{
    public static class EntityExtension
    {

        public static IList<ClientAuthorization> GetClientAuthorizations(this User user)
        {
            return MvcApplication.DataContext.ClientAuthorizations
                .Where(i => i.UserId == user.UserId).ToList();
        }

        public static IList<ClientAuthorization> GetClientAuthorizations(this Client client)
        {
            return MvcApplication.DataContext.ClientAuthorizations
                .Where(i => i.ClientId == client.ClientId).ToList();
        }

        public static Client GetClient(this Cben.Authorization.Client client)
        {

            return new Client()
            {
                Callback = client.Callback,
                ClientId = client.ClientId,
                ClientIdentifier = client.ClientIdentifier,
                ClientSecret = client.ClientSecret,
                ClientType = client.ClientType,
                Name = client.Name
            };
        }

        public static Client GetClient(this ClientAuthorization ca)
        {
            var m = MvcApplication.DataContext.Clients
                .Where(i => i.ClientId == ca.ClientId).FirstOrDefault();
            if (m == null) return null;
            return m.GetClient();
        }

        public static User GetUser(this ClientAuthorization ca)
        {
            return MvcApplication.DataContext.Users
                .Where(i => i.UserId == ca.UserId).FirstOrDefault();
        }

    }
}