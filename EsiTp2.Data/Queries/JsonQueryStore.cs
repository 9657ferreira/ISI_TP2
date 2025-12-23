using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EsiTp2.Data.Queries
{
    public class JsonQueryStore : IQueryStore
    {
        private readonly IDictionary<string, string> _queries;

        public JsonQueryStore(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("filePath não pode ser nulo ou vazio.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Ficheiro de queries JSON não encontrado.", filePath);

            var json = File.ReadAllText(filePath);
            _queries = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                       ?? new Dictionary<string, string>();
        }

        public string Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("key não pode ser nula ou vazia.", nameof(key));

            if (!_queries.TryGetValue(key, out var sql) || string.IsNullOrWhiteSpace(sql))
                throw new KeyNotFoundException($"Query com a chave '{key}' não encontrada no ficheiro JSON.");

            return sql;
        }
    }
}
