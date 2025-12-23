using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Http;
using EsiTp2.Api.Security;
using EsiTp2.Data.Config;
using EsiTp2.Data.Queries;
using EsiTp2.Data.Repositories;
using EsiTp2.Domain.Entities;

namespace EsiTp2.Api.Controllers
{
    // Exige que esteja autenticado (tenha JWT válido)
    [Authorize]
    [RoutePrefix("api/sensores")]
    public class SensoresController : ApiController
    {
        private readonly SensoresRepository _repo;

        public SensoresController()
        {
            // Ler caminho do ficheiro JSON com a connection string
            var dbSettingsKey = ConfigurationManager.AppSettings["DbSettingsFile"];
            var dbSettingsPath = HttpContext.Current.Server.MapPath(dbSettingsKey);
            var dbConfig = DbConfig.FromJsonFile(dbSettingsPath);

            // Ler ficheiro com as queries parametrizadas
            var queriesPath = HttpContext.Current.Server.MapPath("~/App_Data/queries.json");
            var queryStore = new JsonQueryStore(queriesPath);

            _repo = new SensoresRepository(dbConfig, queryStore);
        }

        // GET api/sensores
        // Admin, Gestor e Morador podem ver lista de sensores
        [HttpGet]
        [Route("")]
        [PolicyAuthorize(Policy = Policies.VerSensores)]
        public IHttpActionResult GetAll()
        {
            List<Sensores> sensores = _repo.GetAll();
            return Ok(sensores);
        }

        // GET api/sensores/5
        // Admin, Gestor e Morador podem ver detalhe
        [HttpGet]
        [Route("{id:int}")]
        [PolicyAuthorize(Policy = Policies.VerSensores)]
        public IHttpActionResult GetById(int id)
        {
            var sensor = _repo.GetById(id);
            if (sensor == null)
                return NotFound();

            return Ok(sensor);
        }

        // POST api/sensores
        // Só Admin e Gestor podem criar sensores
        [HttpPost]
        [Route("")]
        [PolicyAuthorize(Policy = Policies.GerirSensores)]
        public IHttpActionResult Add([FromBody] Sensores sensor)
        {
            if (sensor == null)
                return BadRequest("Dados do sensor em falta.");

            _repo.Add(sensor);
            return Ok(sensor);
        }

        // PUT api/sensores
        // Só Admin e Gestor podem atualizar sensores
        [HttpPut]
        [Route("")]
        [PolicyAuthorize(Policy = Policies.GerirSensores)]
        public IHttpActionResult Update([FromBody] Sensores sensor)
        {
            if (sensor == null)
                return BadRequest("Dados do sensor em falta.");

            _repo.Update(sensor);
            return Ok(sensor);
        }

        // DELETE api/sensores/5
        // Só Admin pode apagar sensores (regra ainda mais restritiva)
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok();
        }
    }
}
