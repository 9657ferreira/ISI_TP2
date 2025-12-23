using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EsiTp2.Client
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        private string _token;

        //URLda API
        private const string BaseUrl = "https://localhost:44349/";

        public ApiClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void SetToken(string token)
        {
            _token = token;
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        //DTOs

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class LoginResponse
        {
            public string Username { get; set; }
            public string Role { get; set; }
            public string Token { get; set; }
        }

        public class SensorDto
        {
            public int Id { get; set; }
            public int IdCondominio { get; set; }
            public string Tipo { get; set; }
            public string Codigo { get; set; }
            public string Descricao { get; set; }
            public bool Ativo { get; set; }
        }

        public class CondominioDto
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Morada { get; set; }        
            public string CodigoPostal { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public bool Ativo { get; set; }
        }

        public class DashboardCidadeDto
        {
            public string Cidade { get; set; }
            public int TotalSensores { get; set; }
            public int TotalAlertas { get; set; }
            public int AlertasUltimas24h { get; set; }
            public Dictionary<string, int> AlertasPorTipo { get; set; }
        }

        public class WeatherDto
        {
            public string Cidade { get; set; }
            public decimal Temperatura { get; set; }
            public int Humidade { get; set; }
            public string Descricao { get; set; }
            public DateTime DataHora { get; set; }
        }

        //AUTENTICACAO

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var req = new LoginRequest { Username = username, Password = password };
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/auth/login", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var body = await response.Content.ReadAsStringAsync();
            var loginResp = JsonConvert.DeserializeObject<LoginResponse>(body);

            SetToken(loginResp.Token);
            return loginResp;
        }

        //SENSORES

        public async Task<List<SensorDto>> GetSensoresAsync()
        {
            var response = await _http.GetAsync("api/sensores");
            if (!response.IsSuccessStatusCode)
                return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SensorDto>>(body);
        }

        public async Task<bool> AddSensorAsync(SensorDto sensor)
        {
            var json = JsonConvert.SerializeObject(sensor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _http.PostAsync("api/sensores", content);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateSensorAsync(SensorDto sensor)
        {
            var json = JsonConvert.SerializeObject(sensor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _http.PutAsync("api/sensores", content);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSensorAsync(int id)
        {
            var resp = await _http.DeleteAsync("api/sensores/" + id);
            return resp.IsSuccessStatusCode;
        }

        //CONDOMINIOS

        public async Task<List<CondominioDto>> GetCondominiosAsync()
        {
            var response = await _http.GetAsync("api/condominios");
            if (!response.IsSuccessStatusCode)
                return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CondominioDto>>(body);
        }

        public async Task<bool> AddCondominioAsync(CondominioDto cond)
        {
            var json = JsonConvert.SerializeObject(cond);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _http.PostAsync("api/condominios", content);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCondominioAsync(CondominioDto cond)
        {
            var json = JsonConvert.SerializeObject(cond);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _http.PutAsync("api/condominios", content);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCondominioAsync(int id)
        {
            var resp = await _http.DeleteAsync("api/condominios/" + id);
            return resp.IsSuccessStatusCode;
        }

        //DASHBOARD

        public async Task<DashboardCidadeDto> GetDashboardCidadeAsync(string cidade)
        {
            var response = await _http.GetAsync("api/dashboard/" + cidade);
            if (!response.IsSuccessStatusCode)
                return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DashboardCidadeDto>(body);
        }

        //WEATHER

        public async Task<WeatherDto> GetWeatherByCidadeAsync(string cidade)
        {
            var response = await _http.GetAsync("api/weather/" + cidade);
            if (!response.IsSuccessStatusCode)
                return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherDto>(body);
        }
    }
}
