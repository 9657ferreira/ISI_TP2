using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace EsiTp2.Api.Security
{
    public class JwtAuthenticationHandler : DelegatingHandler
    {
        private readonly JwtTokenService _tokenService;

        public JwtAuthenticationHandler()
        {
            _tokenService = new JwtTokenService();
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Ler header Authorization: Bearer xxx
            if (request.Headers.Authorization != null &&
                request.Headers.Authorization.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                var token = request.Headers.Authorization.Parameter;

                try
                {
                    var principal = _tokenService.ValidateToken(token);

                    // Associar o utilizador autenticado ao contexto
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                        HttpContext.Current.User = principal;
                }
                catch
                {
                    // Token inválido → 401
                    var response = request.CreateResponse(HttpStatusCode.Unauthorized, new
                    {
                        Message = "Token inválido ou expirado."
                    });
                    return response;
                }
            }

            // Se não houver token, segue normal (endpoints com [AllowAnonymous] funcionam)
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
