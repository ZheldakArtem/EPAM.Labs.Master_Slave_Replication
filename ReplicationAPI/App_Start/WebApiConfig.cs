using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using ReplicationAPI.DependencyResolver;

namespace ReplicationAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
            );

			//IUnityContainer container = UnityConfig.BuildUnityContainer();
			//config.DependencyResolver = new UnityResolver(container);
        }
    }
}
