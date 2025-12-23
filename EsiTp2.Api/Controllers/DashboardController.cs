using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using EsiTp2.Data.Config;
using EsiTp2.Data.Queries;      
using EsiTp2.Data.Repositories;
using EsiTp2.Domain.Entities;

namespace EsiTp2.Api.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        private readonly AlertasRepository _alertasRepo;
        private readonly SensoresRepository _sensoresRepo;
        private readonly WeatherRepository _weatherRepo;
        private readonly CondominioRepository _condominiosRepo;

        public DashboardController()
        {
            var cs = ConfigurationManager.ConnectionStrings["SmartCondoDb"].ConnectionString;
            var dbConfig = new DbConfig(cs);

            // caminho para o ficheiro App_Data/queries.json
            var queriesPath = HttpContext.Current.Server.MapPath("~/App_Data/queries.json");
            var queryStore = new JsonQueryStore(queriesPath);

            _alertasRepo = new AlertasRepository(dbConfig);         
            _sensoresRepo = new SensoresRepository(dbConfig, queryStore);
            _weatherRepo = new WeatherRepository(dbConfig);         
            _condominiosRepo = new CondominioRepository(dbConfig,queryStore);      
        }

        /// <summary>
        /// Mostra todos os sensores + todos os alertas + clima.
        /// Exemplo: GET api/dashboard/Braga
        /// </summary>
        [HttpGet]
        [Route("{cidade}")]
        public IHttpActionResult GetDashboardPorCidade(string cidade)
        {
            var sensores = _sensoresRepo.GetAll();
            var alertas = _alertasRepo.GetAll();
            var clima = _weatherRepo.GetLastByCity(cidade);

            var agora = DateTime.Now;
            var limite = agora.AddHours(-24);

            var alertas24 = alertas
                .Where(a => a.DataHora >= limite)
                .ToList();

            var alertasPorTipo = alertas
                .GroupBy(a => a.Tipo)
                .ToDictionary(g => g.Key, g => g.Count());

            var resposta = new
            {
                Cidade = cidade,
                TotalSensores = sensores.Count,
                TotalAlertas = alertas.Count,
                AlertasUltimas24h = alertas24.Count,
                AlertasPorTipo = alertasPorTipo,
                ClimaAtual = clima == null ? null : new
                {
                    clima.Cidade,
                    clima.Temperatura,
                    clima.Humidade,
                    clima.Descricao,
                    clima.DataHora
                }
            };

            return Ok(resposta);
        }

        /// <summary>
        /// Dashboard por condomínio: sensores e alertas desse condomínio + clima associado.
        /// Exemplo: GET api/dashboard/condominio/1
        /// </summary>
        [HttpGet]
        [Route("condominio/{id:int}")]
        public IHttpActionResult GetDashboardPorCondominio(int id)
        {
            // 1) Condomínio
            Condominio cond = _condominiosRepo.GetById(id);
            if (cond == null)
                return NotFound();

            // 2) Sensores desse condomínio (usa IdCondominio)
            var todosSensores = _sensoresRepo.GetAll();
            var sensoresCondo = todosSensores
                .Where(s => s.IdCondominio == id)
                .ToList();

            var sensoresIds = sensoresCondo
                .Select(s => s.Id)
                .ToList();

            // 3) Alertas só desses sensores
            var todosAlertas = _alertasRepo.GetAll();
            var alertasCondo = todosAlertas
                .Where(a => sensoresIds.Contains(a.IdSensor))
                .ToList();

            // 
            var agora = DateTime.Now;
            var limite = agora.AddHours(-24);

            var alertas24 = alertasCondo
                .Where(a => a.DataHora >= limite)
                .ToList();

            var alertasPorTipo = alertas24
                .GroupBy(a => a.Tipo)
                .ToDictionary(g => g.Key, g => g.Count());

            // 5) Clima — aqui usas o nome do condomínio como "cidade"
            var cidade = cond.Nome; // ou cond.Cidade se tiveres essa prop.
            var clima = _weatherRepo.GetLastByCity(cidade);

            var resposta = new
            {
                CondominioId = cond.Id,
                Condominio = cond.Nome,
                Cidade = cidade,

                TotalSensores = sensoresCondo.Count,
                TotalAlertas = alertasCondo.Count,
                AlertasUltimas24h = alertas24.Count,
                AlertasPorTipo = alertasPorTipo,

                ClimaAtual = clima == null ? null : new
                {
                    clima.Cidade,
                    clima.Temperatura,
                    clima.Humidade,
                    clima.Descricao,
                    clima.DataHora
                }
            };

            return Ok(resposta);
        }
    }
}
