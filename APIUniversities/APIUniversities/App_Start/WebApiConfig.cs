using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APIUniversities
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "UniversitiesRoute",
                routeTemplate: "api/universities/{id}",
                defaults: new { controller="Universities", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CareersRoute",
                routeTemplate: "api/universities/{universityId}/{controller}/{careerId}",
                defaults: new { controller = "Careers", careerId = RouteParameter.Optional }
            );
        }
    }
}
