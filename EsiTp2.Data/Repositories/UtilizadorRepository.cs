using EsiTp2.Data.Config;
using EsiTp2.Data.Queries;
using EsiTp2.Domain.Entities;
using System;
using System.Data.SqlClient;

namespace EsiTp2.Data.Repositories
{
    public class UtilizadorRepository
    {
        private readonly DbConfig _config;
        private readonly IQueryStore _queries;

        public UtilizadorRepository(DbConfig config, IQueryStore queries)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        public Utilizador GetByUsername(string username)
        {
            var sql = _queries.Get("Auth.GetByUsername"); 

            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    return new Utilizador
                    {
                        Id = (int)rdr["Id"],
                        Username = rdr["Username"].ToString(),
                        PasswordHash = rdr["PasswordHash"].ToString(),
                        Role = rdr["Role"].ToString(),
                        Ativo = (bool)rdr["Ativo"]
                    };
                }
            }
        }
    }
}
