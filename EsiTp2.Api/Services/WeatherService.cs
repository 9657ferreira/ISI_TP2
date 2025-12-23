using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using EsiTp2.Domain.Weather;
using EsiTp2.Data.Config;
using EsiTp2.Data.Repositories;
using Newtonsoft.Json.Linq;

namespace EsiTp2.Api.Services
{
    public class WeatherService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly WeatherRepository _repo;

        public WeatherService()
        {
            _apiKey = ConfigurationManager.AppSettings["OpenWeatherApiKey"];
            _baseUrl = ConfigurationManager.AppSettings["OpenWeatherBaseUrl"];

            if (string.IsNullOrEmpty(_apiKey))
                throw new InvalidOperationException("OpenWeatherApiKey não está definido no Web.config.");

            // criar repositorio para gravar na BD
            var cs = ConfigurationManager.ConnectionStrings["SmartCondoDb"].ConnectionString;
            var dbConfig = new DbConfig(cs);
            _repo = new WeatherRepository(dbConfig);
        }

        public async Task<WeatherLog> GetWeatherByCityAsync(string cidade, string countryCode = "PT")
        {
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}weather?q={cidade},{countryCode}&appid={_apiKey}&units=metric&lang=pt";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var obj = JObject.Parse(json);

                var log = new WeatherLog
                {
                    Cidade = (string)obj["name"],
                    Temperatura = (double)obj["main"]["temp"],
                    Humidade = (int)obj["main"]["humidity"],
                    Descricao = (string)obj["weather"][0]["description"],
                    DataHora = DateTime.Now
                };

                // guardar na BD
                _repo.Insert(log);

                return log;
            }
        }

        public WeatherLog GetLastFromDb(string cidade)
        {
            return _repo.GetLastByCity(cidade);
        }
    }
}
