using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EsiTp2.Data.Config;
using EsiTp2.Data.Queries;
using EsiTp2.Domain.Entities;

namespace EsiTp2.Data.Repositories
{
    public class CondominioRepository
    {
        private readonly DbConfig _config;
        private readonly IQueryStore _queries;

        public CondominioRepository(DbConfig config, IQueryStore queries)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        public List<Condominio> GetAll()
        {
            var lista = new List<Condominio>();
            var sql = _queries.Get(QueryKeys.Condominios_GetAll);

            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new Condominio
                        {
                            Id = (int)rdr["Id"],
                            Nome = rdr["Nome"].ToString(),
                            Morada = rdr["Morada"].ToString(),
                            CodigoPostal = rdr["CodigoPostal"].ToString(),
                            Latitude = (double)rdr["Latitude"],
                            Longitude = (double)rdr["Longitude"],
                            Ativo = (bool)rdr["Ativo"]
                        });
                    }
                }
            }

            return lista;
        }

        public Condominio GetById(int id)
        {
            var sql = _queries.Get(QueryKeys.Condominios_GetById);

            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    return new Condominio
                    {
                        Id = (int)rdr["Id"],
                        Nome = rdr["Nome"].ToString(),
                        Morada = rdr["Morada"].ToString(),
                        CodigoPostal = rdr["CodigoPostal"].ToString(),
                        Latitude = (double)rdr["Latitude"],
                        Longitude = (double)rdr["Longitude"],
                        Ativo = (bool)rdr["Ativo"]
                    };
                }
            }
        }

        public void Delete(int id)
        {
            var sql = _queries.Get(QueryKeys.Condominios_Delete);

            using (var conn = new SqlConnection(_config.ConnectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(sql, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
