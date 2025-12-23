using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace EsiTp2.Api.Logging
{
    public class JsonExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static JsonFileLogger _logger;

        public override void OnException(HttpActionExecutedContext context)
        {
            // Inicializar logger (pasta App_Data/logs)
            if (_logger == null)
            {
                var logPath = HttpContext.Current.Server.MapPath("~/App_Data/logs");
                _logger = new JsonFileLogger(logPath);
            }

            // 1) Registar erro em ficheiro JSON
            _logger.LogError(context.Exception, context.Request);

            // 2) Resposta JSON amigável para o cliente
            var errorBody = new
            {
                Message = "Ocorreu um erro interno. Tente novamente mais tarde.",
                Detail = context.Exception.Message   // se quiseres esconder em produção, podes remover
            };

            var response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                errorBody,
                context.ActionContext.ControllerContext.Configuration.Formatters.JsonFormatter);

            context.Response = response;
        }
    }
}
