using System;
using System.IO;
using Newtonsoft.Json;

namespace EsiTp2.Data.Config
{
    // Classe que representa o conteúdo do JSON
    public class DbSettings
    {
        public string ConnectionString { get; set; }
    }

    public class DbConfig
    {
        public string ConnectionString { get; private set; }

        public DbConfig(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("ConnectionString não pode ser vazia.", nameof(connectionString));

            ConnectionString = connectionString;
        }

        /// <summary>
        /// Lê o ficheiro JSON (dbsettings.json) e cria um DbConfig.
        /// </summary>
        public static DbConfig FromJsonFile(string jsonPath)
        {
            var json = File.ReadAllText(jsonPath);
            var settings = JsonConvert.DeserializeObject<DbSettings>(json);

            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
                throw new InvalidOperationException("ConnectionString não encontrada no ficheiro JSON.");

            return new DbConfig(settings.ConnectionString);
        }
    }
}
