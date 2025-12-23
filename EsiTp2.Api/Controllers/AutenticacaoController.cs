using System.Configuration;
using System.Web;
using System.Web.Http;
using EsiTp2.Api.Security;
using EsiTp2.Data.Config;
using EsiTp2.Data.Queries;
using EsiTp2.Data.Repositories;

namespace EsiTp2.Api.Controllers
{
    [RoutePrefix("api/auth")]
    public class AutenticacaoController : ApiController
    {
        private readonly UtilizadorRepository _usersRepo;
        private readonly JwtTokenService _tokenService;

        public AutenticacaoController()
        {
            // DbConfig a partir do dbsettings.json
            var dbSettingsKey = ConfigurationManager.AppSettings["DbSettingsFile"];
            var dbSettingsPath = HttpContext.Current.Server.MapPath(dbSettingsKey);
            var dbConfig = DbConfig.FromJsonFile(dbSettingsPath);

            // QueryStore (queries.json)
            var queriesPath = HttpContext.Current.Server.MapPath("~/App_Data/queries.json");
            var queryStore = new JsonQueryStore(queriesPath);

            _usersRepo = new UtilizadorRepository(dbConfig, queryStore);
            _tokenService = new JwtTokenService();
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        /// <summary>
        /// Autenticação de utilizador. Devolve um JWT se a password estiver correta.
        /// </summary>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IHttpActionResult Login([FromBody] LoginRequest req)
        {
            if (req == null ||
                string.IsNullOrWhiteSpace(req.Username) ||
                string.IsNullOrWhiteSpace(req.Password))
            {
                return BadRequest("Username e password são obrigatórios.");
            }

            // 1) Vai buscar o utilizador à BD
            var user = _usersRepo.GetByUsername(req.Username);


            // 2) Verificar password com bcrypt
            //    user.PasswordHash deve conter o hash gerado por BCrypt.HashPassword(...)
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            {
                return Unauthorized();
            }

            // 3) Gera o token JWT com o role do utilizador
            var token = _tokenService.GenerateToken(user.Username, user.Role);

            // 4) Devolver dados básicos + token
            return Ok(new
            {
                Username = user.Username,
                Role = user.Role,
                Token = token
            });
        }
    }
}
