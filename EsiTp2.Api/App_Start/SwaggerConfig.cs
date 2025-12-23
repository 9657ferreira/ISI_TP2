using System.Web.Http;
using WebActivatorEx;
using EsiTp2.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace EsiTp2.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "EsiTp2.Api");

                    // ---- CONFIGURAÇÃO DO JWT NO SWAGGER ----
                    // Security scheme do tipo ApiKey, mas a usar o header Authorization
                    // O ID "api_key" é só o nome interno que aparece no UI.
                    c.ApiKey("api_key")
                     .Description("JWT Authorization header. Exemplo: 'Bearer {token}'")
                     .Name("Authorization")   // nome REAL do header
                     .In("header");           // passa no header, não na query
                                              // ----------------------------------------
                })
                .EnableSwaggerUi(c =>
                {
                    // Liga o campo "api_key" do topo ao header Authorization
                    // Assim tudo o que escreveres lá (ex.: 'Bearer eyJ...') vai em:
                    // Authorization: Bearer eyJ...
                    c.EnableApiKeySupport("Authorization", "header");
                });
        }
    }
}
