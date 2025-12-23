using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace EsiTp2.Api.Security
{
    // AutorizeAttribute customizado baseado em "policies"
    public class PolicyAuthorizeAttribute : AuthorizeAttribute
    {
        public string Policy { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;

            if (user == null || !user.Identity.IsAuthenticated)
                return false;

            switch (Policy)
            {
                case Policies.VerSensores:
                    // Admin, Gestor e Morador podem ver
                    return user.IsInRole("Admin")
                        || user.IsInRole("Gestor")
                        || user.IsInRole("Morador");

                case Policies.GerirSensores:
                    // Só Admin e Gestor podem gerir (criar/editar)
                    return user.IsInRole("Admin")
                        || user.IsInRole("Gestor");

                default:
                    // Se for uma policy desconhecida, usa o comportamento normal
                    return base.IsAuthorized(actionContext);
            }
        }
    }
}
