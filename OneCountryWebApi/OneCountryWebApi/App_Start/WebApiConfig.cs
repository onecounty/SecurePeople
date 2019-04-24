using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace OneCountryWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "one10/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //string location, int actionId, string description,int page,int itemsPerPage
            config.Routes.MapHttpRoute(
                name: "WebApplicationApi",
                routeTemplate: "one10/{controller}/{location}/{actionId}/{description}/{page}/{itemsPerPage}",
                defaults: new { location = RouteParameter.Optional , actionId = RouteParameter.Optional, description = RouteParameter.Optional, page = 0, itemsPerPage = 20, }
            );
        }
    }
}
