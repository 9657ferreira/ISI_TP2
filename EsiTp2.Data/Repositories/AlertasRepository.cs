using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EsiTp2.Data.Config;
using EsiTp2.Domain.Entities;

namespace EsiTp2.Data.Repositories
{
    public class AlertasRepository
    {
        private readonly DbConfig _config;

        public AlertasRepository(DbConfig config)
        {
            _config = config;
        }

        public List<Alerta> GetAll()
        {
            var lista = new List<Alerta>();

            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(
                @"SELECT Id, IdSensor, DataHora, Tipo, Descricao, Resolvido 
                  FROM Alertas
                  ORDER BY DataHora", conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var alerta = new Alerta
                        {
                            Id = (int)rdr["Id"],
                            IdSensor = (int)rdr["IdSensor"],
                            DataHora = (DateTime)rdr["DataHora"],
                            Tipo = rdr["Tipo"].ToString(),
                            Descricao = rdr["Descricao"].ToString(),
                            Resolvido = Convert.ToBoolean(rdr["Resolvido"])
                        };

                        lista.Add(alerta);
                    }
                }
            }

            return lista;
        }

        public Alerta GetById(int id)
        {
            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(
                @"SELECT Id, IdSensor, DataHora, Tipo, Descricao, Resolvido 
                  FROM Alertas
                  WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    return new Alerta
                    {
                        Id = (int)rdr["Id"],
                        IdSensor = (int)rdr["IdSensor"],
                        DataHora = (DateTime)rdr["DataHora"],
                        Tipo = rdr["Tipo"].ToString(),
                        Descricao = rdr["Descricao"].ToString(),
                        Resolvido = Convert.ToBoolean(rdr["Resolvido"])
                    };
                }
            }
        }
    }
}
