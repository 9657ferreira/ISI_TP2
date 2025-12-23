
using EsiTp2.Api.Logging;
using EsiTp2.Api.Security;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EsiTp2.Api
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
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Swagger


            // Filtro global para erros log em JSON
            config.Filters.Add(new JsonExceptionFilterAttribute());

            // Handler de autenticação JWT (tem de ir antes ou depois do logging, como preferires)
            config.MessageHandlers.Add(new JwtAuthenticationHandler());

            // Handler para logar TODOS os pedidos em JSON
            config.MessageHandlers.Add(new RequestLoggingHandler());

        }
    }
}
