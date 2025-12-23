using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace EsiTp2.Api.Logging
{
    public class RequestLoggingHandler : DelegatingHandler
    {
        private static JsonFileLogger _logger;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_logger == null)
            {
                var logPath = HttpContext.Current.Server.MapPath("~/App_Data/logs");
                _logger = new JsonFileLogger(logPath);
            }

            var stopwatch = Stopwatch.StartNew();
            HttpResponseMessage response = null;

            try
            {
                response = await base.SendAsync(request, cancellationToken);
                return response;
            }
            finally
            {
                stopwatch.Stop();

                try
                {
                    _logger.LogRequest(request, response, stopwatch.ElapsedMilliseconds);
                }
                catch
                {
                    // Nunca deixar o log estragar o pedido
                }
            }
        }
    }
}
