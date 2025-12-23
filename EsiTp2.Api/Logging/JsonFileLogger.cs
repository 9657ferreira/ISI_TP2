using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace EsiTp2.Api.Logging
{
    public class JsonFileLogger
    {
        private readonly string _logFolder;

        public JsonFileLogger(string logFolder)
        {
            _logFolder = logFolder;

            if (!Directory.Exists(_logFolder))
                Directory.CreateDirectory(_logFolder);
        }

        public void LogError(Exception ex, HttpRequestMessage request)
        {
            var logEntry = new
            {
                TimeUtc = DateTime.UtcNow,
                Level = "Error",
                Message = ex.Message,
                ExceptionType = ex.GetType().FullName,
                StackTrace = ex.ToString(),
                Request = new
                {
                    Method = request?.Method.Method,
                    Url = request?.RequestUri?.ToString(),
                    Headers = request?.Headers?
                                  .ToDictionary(h => h.Key, h => string.Join(";", h.Value))
                }
            };

            string json = JsonConvert.SerializeObject(logEntry);
            string fileName = $"errors-{DateTime.UtcNow:yyyyMMdd}.log";
            string fullPath = Path.Combine(_logFolder, fileName);

            File.AppendAllText(fullPath, json + Environment.NewLine);
        }

        public void LogRequest(HttpRequestMessage request, HttpResponseMessage response, long elapsedMs)
        {
            var logEntry = new
            {
                TimeUtc = DateTime.UtcNow,
                Level = "Info",
                Request = new
                {
                    Method = request?.Method.Method,
                    Url = request?.RequestUri?.ToString(),
                    Headers = request?.Headers?
                                  .ToDictionary(h => h.Key, h => string.Join(";", h.Value))
                },
                Response = new
                {
                    StatusCode = (int)(response?.StatusCode ?? 0),
                    ReasonPhrase = response?.ReasonPhrase
                },
                ElapsedMs = elapsedMs
            };

            string json = JsonConvert.SerializeObject(logEntry);
            string fileName = $"requests-{DateTime.UtcNow:yyyyMMdd}.log";
            string fullPath = Path.Combine(_logFolder, fileName);

            File.AppendAllText(fullPath, json + Environment.NewLine);
        }
    }
}
