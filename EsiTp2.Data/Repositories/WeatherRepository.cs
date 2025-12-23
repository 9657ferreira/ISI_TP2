using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EsiTp2.Data.Config;
using EsiTp2.Domain.Weather;

namespace EsiTp2.Data.Repositories
{
    public class WeatherRepository
    {
        private readonly DbConfig _config;

        public WeatherRepository(DbConfig config)
        {
            _config = config;
        }

        public void Insert(WeatherLog log)
        {
            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(
                   @"INSERT INTO WeatherLogs (Cidade, Temperatura, Humidade, Descricao, DataHora)
                     VALUES (@Cidade, @Temperatura, @Humidade, @Descricao, @DataHora);", conn))
            {
                cmd.Parameters.AddWithValue("@Cidade", log.Cidade);
                cmd.Parameters.AddWithValue("@Temperatura", log.Temperatura);
                cmd.Parameters.AddWithValue("@Humidade", log.Humidade);
                cmd.Parameters.AddWithValue("@Descricao", log.Descricao);
                cmd.Parameters.AddWithValue("@DataHora", log.DataHora);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public WeatherLog GetLastByCity(string cidade)
        {
            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(
                   @"SELECT TOP 1 Id, Cidade, Temperatura, Humidade, Descricao, DataHora
                       FROM WeatherLogs
                      WHERE Cidade = @Cidade
                      ORDER BY DataHora DESC;", conn))
            {
                cmd.Parameters.AddWithValue("@Cidade", cidade);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read()) return null;

                    return new WeatherLog
                    {
                        Id = (int)rdr["Id"],
                        Cidade = (string)rdr["Cidade"],
                        Temperatura = Convert.ToDouble(rdr["Temperatura"]),
                        Humidade = (int)rdr["Humidade"],
                        Descricao = (string)rdr["Descricao"],
                        DataHora = (DateTime)rdr["DataHora"]
                    };
                }
            }
        }

        public WeatherLog GetByDateAndCity(DateTime dia, string cidade)
        {
            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(
                   @"SELECT TOP 1 Id, Cidade, Temperatura, Humidade, Descricao, DataHora
                       FROM WeatherLogs
                      WHERE Cidade = @Cidade
                        AND CAST(DataHora AS DATE) = @Dia
                      ORDER BY DataHora DESC;", conn))
            {
                cmd.Parameters.AddWithValue("@Cidade", cidade);
                cmd.Parameters.AddWithValue("@Dia", dia.Date);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read()) return null;

                    return new WeatherLog
                    {
                        Id = (int)rdr["Id"],
                        Cidade = (string)rdr["Cidade"],
                        Temperatura = Convert.ToDouble(rdr["Temperatura"]),
                        Humidade = (int)rdr["Humidade"],
                        Descricao = (string)rdr["Descricao"],
                        DataHora = (DateTime)rdr["DataHora"]
                    };
                }
            }
        }
    }
}
