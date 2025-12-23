using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using EsiTp2.Data.Config;
using EsiTp2.Data.Repositories;
using EsiTp2.Domain.Entities;
using EsiTp2.Domain.Weather;

namespace EsiTp2.Api.Controllers
{
    [RoutePrefix("api/alertas")]
    public class AlertasController : ApiController
    {
        private readonly AlertasRepository _alertasRepo;
        private readonly WeatherRepository _weatherRepo;

        public AlertasController()
        {
            var cs = ConfigurationManager.ConnectionStrings["SmartCondoDb"].ConnectionString;
            var dbConfig = new DbConfig(cs);

            _alertasRepo = new AlertasRepository(dbConfig);
            _weatherRepo = new WeatherRepository(dbConfig);
        }

        // GET api/alertas
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            List<Alerta> alertas = _alertasRepo.GetAll();
            return Ok(alertas);
        }

        // GET api/alertas/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var alerta = _alertasRepo.GetById(id);
            if (alerta == null)
                return NotFound();

            return Ok(alerta);
        }

        /// <summary>
        /// Devolve todos os alertas + clima correspondente (por data/hora) para a cidade indicada.
        /// Ex: GET api/alertas/comclima/Braga
        /// </summary>
        [HttpGet]
        [Route("comclima/{cidade}")]
        public IHttpActionResult GetAlertasComClima(string cidade)
        {
            var alertas = _alertasRepo.GetAll();

            var lista = new List<object>();

            foreach (var alerta in alertas)
            {
                WeatherLog clima = _weatherRepo.GetByDateAndCity(alerta.DataHora, cidade);

                lista.Add(new
                {
                    alerta.Id,
                    alerta.IdSensor,
                    alerta.DataHora,
                    alerta.Tipo,
                    alerta.Descricao,
                    alerta.Resolvido,
                    Clima = clima  // pode ser null se não houver registo para esse dia
                });
            }

            return Ok(lista);
        }
    }
}
