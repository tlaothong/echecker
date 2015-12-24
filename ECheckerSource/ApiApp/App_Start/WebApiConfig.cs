using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiApp
{
    /// <summary>
    /// WebApi Configuration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register the configuration
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
