using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Http;
using EsiTp2.Data.Config;
using EsiTp2.Data.Queries;
using EsiTp2.Data.Repositories;
using EsiTp2.Domain.Entities;

namespace EsiTp2.Api.Controllers
{
    [RoutePrefix("api/condominios")]
    public class CondominiosController : ApiController
    {
        private readonly CondominioRepository _repo;

        public CondominiosController()
        {
            // dbsettings.json
            var dbSettingsKey = ConfigurationManager.AppSettings["DbSettingsFile"];
            var dbSettingsPath = HttpContext.Current.Server.MapPath(dbSettingsKey);
            var dbConfig = DbConfig.FromJsonFile(dbSettingsPath);

            // queries.json
            var queriesPath = HttpContext.Current.Server.MapPath("~/App_Data/queries.json");
            var queryStore = new JsonQueryStore(queriesPath);

            _repo = new CondominioRepository(dbConfig, queryStore);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            List<Condominio> lista = _repo.GetAll();
            return Ok(lista);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetCondominioById")]
        public IHttpActionResult GetById(int id)
        {
            var cond = _repo.GetById(id);
            if (cond == null)
                return NotFound();

            return Ok(cond);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok();
        }
    }
}
