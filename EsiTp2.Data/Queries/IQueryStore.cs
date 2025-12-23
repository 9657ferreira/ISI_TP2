namespace EsiTp2.Data.Queries
{
    public interface IQueryStore
    {

        string Get(string key);
    }
    public static class QueryKeys
    {
        public const string Sensores_GetAll = "Sensores.GetAll";
        public const string Sensores_GetById = "Sensores.GetById";
        public const string Sensores_Insert = "Sensores.Insert";
        public const string Sensores_Update = "Sensores.Update";
        public const string Sensores_Delete = "Sensores.Delete";

        // Condominios
        public const string Condominios_GetAll = "Condominios.GetAll";
        public const string Condominios_GetById = "Condominios.GetById";
        public const string Condominios_Insert = "Condominios.Insert";
        public const string Condominios_Update = "Condominios.Update";
        public const string Condominios_Delete = "Condominios.Delete";
    }
}

