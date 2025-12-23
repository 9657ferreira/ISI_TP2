using EsiTp2.Data.Config;
using EsiTp2.Data.Queries;
using EsiTp2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EsiTp2.Data.Repositories
{
    public class SensoresRepository
    {
        private readonly DbConfig _config;
        private readonly IQueryStore _queries;

        public SensoresRepository(DbConfig config, IQueryStore queries)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        #region SÍNCRONO

        public List<Sensores> GetAll()
        {
            var lista = new List<Sensores>();
            var sql = _queries.Get(QueryKeys.Sensores_GetAll);

            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new Sensores
                        {
                            Id = (int)rdr["Id"],
                            IdCondominio = (int)rdr["IdCondominio"],
                            Tipo = rdr["Tipo"].ToString(),
                            Codigo = rdr["Codigo"].ToString(),
                            Descricao = rdr["Descricao"].ToString(),
                            Ativo = (bool)rdr["Ativo"]
                        });
                    }
                }
            }
            return lista;
        }

        public Sensores GetById(int id)
        {
            var sql = _queries.Get(QueryKeys.Sensores_GetById);

            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    return new Sensores
                    {
                        Id = (int)rdr["Id"],
                        IdCondominio = (int)rdr["IdCondominio"],
                        Tipo = rdr["Tipo"].ToString(),
                        Codigo = rdr["Codigo"].ToString(),
                        Descricao = rdr["Descricao"].ToString(),
                        Ativo = (bool)rdr["Ativo"]
                    };
                }
            }
        }

        public void Add(Sensores sensor)
        {
            var sql = _queries.Get(QueryKeys.Sensores_Insert);

            using (var conn = new SqlConnection(_config.ConnectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(sql, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@IdCondominio", sensor.IdCondominio);
                            cmd.Parameters.AddWithValue("@Tipo", sensor.Tipo);
                            cmd.Parameters.AddWithValue("@Codigo", sensor.Codigo);
                            cmd.Parameters.AddWithValue("@Descricao", sensor.Descricao);
                            cmd.Parameters.AddWithValue("@Ativo", sensor.Ativo);

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

        public void Update(Sensores sensor)
        {
            var sql = _queries.Get(QueryKeys.Sensores_Update);

            using (var conn = new SqlConnection(_config.ConnectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(sql, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@Id", sensor.Id);
                            cmd.Parameters.AddWithValue("@IdCondominio", sensor.IdCondominio);
                            cmd.Parameters.AddWithValue("@Tipo", sensor.Tipo);
                            cmd.Parameters.AddWithValue("@Codigo", sensor.Codigo);
                            cmd.Parameters.AddWithValue("@Descricao", sensor.Descricao);
                            cmd.Parameters.AddWithValue("@Ativo", sensor.Ativo);

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

        public void Delete(int id)
        {
            var sql = _queries.Get(QueryKeys.Sensores_Delete);

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


        #endregion

        #region ASSÍNCRONO

        public async Task<List<Sensores>> GetAllAsync()
        {
            var lista = new List<Sensores>();
            var sql = _queries.Get(QueryKeys.Sensores_GetAll);

            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        lista.Add(new Sensores
                        {
                            Id = (int)rdr["Id"],
                            IdCondominio = (int)rdr["IdCondominio"],
                            Tipo = rdr["Tipo"].ToString(),
                            Codigo = rdr["Codigo"].ToString(),
                            Descricao = rdr["Descricao"].ToString(),
                            Ativo = (bool)rdr["Ativo"]
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Sensores> GetByIdAsync(int id)
        {
            var sql = _queries.Get(QueryKeys.Sensores_GetById);

            using (var conn = new SqlConnection(_config.ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                await conn.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    if (!await rdr.ReadAsync())
                        return null;

                    return new Sensores
                    {
                        Id = (int)rdr["Id"],
                        IdCondominio = (int)rdr["IdCondominio"],
                        Tipo = rdr["Tipo"].ToString(),
                        Codigo = rdr["Codigo"].ToString(),
                        Descricao = rdr["Descricao"].ToString(),
                        Ativo = (bool)rdr["Ativo"]
                    };
                }
            }
        }

        public async Task AddAsync(Sensores sensor, bool simulateError = false)
        {
            var sql = _queries.Get(QueryKeys.Sensores_Insert);

            using (var conn = new SqlConnection(_config.ConnectionString))
            {
                await conn.OpenAsync();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(sql, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@IdCondominio", sensor.IdCondominio);
                            cmd.Parameters.AddWithValue("@Tipo", sensor.Tipo);
                            cmd.Parameters.AddWithValue("@Codigo", sensor.Codigo);
                            cmd.Parameters.AddWithValue("@Descricao", sensor.Descricao);
                            cmd.Parameters.AddWithValue("@Ativo", sensor.Ativo);

                            await cmd.ExecuteNonQueryAsync();
                        }

                        // Delay para teste
                        await Task.Delay(5000);

                        if (simulateError)
                            throw new Exception("Erro simulado para teste de transação");

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

        public async Task UpdateAsync(Sensores sensor)
        {
            var sql = _queries.Get(QueryKeys.Sensores_Update);

            using (var conn = new SqlConnection(_config.ConnectionString))
            {
                await conn.OpenAsync();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(sql, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@Id", sensor.Id);
                            cmd.Parameters.AddWithValue("@IdCondominio", sensor.IdCondominio);
                            cmd.Parameters.AddWithValue("@Tipo", sensor.Tipo);
                            cmd.Parameters.AddWithValue("@Codigo", sensor.Codigo);
                            cmd.Parameters.AddWithValue("@Descricao", sensor.Descricao);
                            cmd.Parameters.AddWithValue("@Ativo", sensor.Ativo);

                            await cmd.ExecuteNonQueryAsync();
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

        public async Task DeleteAsync(int id)
        {
            var sql = _queries.Get(QueryKeys.Sensores_Delete);

            using (var conn = new SqlConnection(_config.ConnectionString))
            {
                await conn.OpenAsync();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(sql, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            await cmd.ExecuteNonQueryAsync();
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

        #endregion
    }
}
