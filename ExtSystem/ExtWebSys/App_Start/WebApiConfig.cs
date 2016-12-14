using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ExtWebSys
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi0",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi1",
                routeTemplate: "api/{controller}/{id}-{idB}",
                defaults: new { id = RouteParameter.Optional, idB = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "DefaultApi2",
               routeTemplate: "api/{controller}/{id}-{idB}-{idC}",
               defaults: new { id = RouteParameter.Optional, idB = RouteParameter.Optional, idC = RouteParameter.Optional }
           );
        }
    }
}
