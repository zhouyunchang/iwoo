
using OAuthAuthorizationServer.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OAuthAuthorizationServer
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public static DatabaseKeyNonceStore KeyNonceStore { get; set; }

        /// <summary>
		/// Gets the transaction-protected database connection for the current request.
		/// </summary>
		public static OAuthDbContext DataContext
        {
            get
            {
                OAuthDbContext dataContext = DbContext;
                if (dataContext == null)
                {
                    dataContext = new OAuthDbContext();
                    dataContext.Database.Connection.Open();
                    dataContext.Database.BeginTransaction();
                    DbContext = dataContext;
                }

                return dataContext;
            }
        }

        public static Cben.Authorization.User LoggedInUser
        {
            get { return DataContext.Users.SingleOrDefault(user => user.OpenIDClaimedIdentifier == HttpContext.Current.User.Identity.Name); }
        }

        private static OAuthDbContext DbContext
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Items["DataContext"] as OAuthDbContext;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items["DataContext"] = value;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            KeyNonceStore = new DatabaseKeyNonceStore();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // In the event of an unhandled exception, reverse any changes that were made to the database to avoid any partial database updates.
            var dataContext = DbContext;
            if (dataContext != null)
            {
                if (dataContext.Database.CurrentTransaction != null)
                    dataContext.Database.CurrentTransaction.Rollback();
                dataContext.Database.Connection.Close();
                dataContext.Dispose();
                DbContext = null;
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            CommitAndCloseDatabaseIfNecessary();
        }

        private static void CommitAndCloseDatabaseIfNecessary()
        {
            var dataContext = DbContext;
            if (dataContext != null)
            {
                dataContext.SaveChanges();
                if (dataContext.Database.CurrentTransaction != null)
                    dataContext.Database.CurrentTransaction.Commit();
                dataContext.Database.Connection.Close();
            }
        }


    }
}
