using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cben.WebApi
{

    /// <summary>
    /// HttpConfiguration 环境
    /// </summary>
    public static class HttpConfigurationEnvironment
    {

        /// <summary>
        /// MVC HelpPage
        /// </summary>
        public static readonly HttpConfiguration GlobalConfiguration = System.Web.Http.GlobalConfiguration.Configuration;


        /// <summary>
        /// Web Api
        /// </summary>
        public static readonly HttpConfiguration ApiHttpConfiguration = new HttpConfiguration();

    }
}