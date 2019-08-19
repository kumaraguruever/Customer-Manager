using CustomerManagementApp.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Wolnik.Azure.TableStorage.Repository;

namespace CustomerManagementApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new APIUnityResolver(RegisterUnityContainer());

            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static IUnityContainer RegisterUnityContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IAzureTableStorage, AzureTableStorage>();
            return container;
        }
    }
}
