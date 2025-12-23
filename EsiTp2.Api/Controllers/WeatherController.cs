using System.Threading.Tasks;
using System.Web.Http;
using EsiTp2.Api.Services;
using EsiTp2.Domain.Weather;

namespace EsiTp2.Api.Controllers
{
    [RoutePrefix("api/weather")]
    public class WeatherController : ApiController
    {
        private readonly WeatherService _weatherService = new WeatherService();

        [HttpGet]
        [Route("{cidade}")]
        public async Task<IHttpActionResult> GetByCity(string cidade)
        {
            var info = await _weatherService.GetWeatherByCityAsync(cidade);
            return Ok(info); 
        }
    }

}
