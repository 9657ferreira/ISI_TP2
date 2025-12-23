using System;

namespace EsiTp2.Domain.Entities
{
    public class Alerta
    {
        public int Id { get; set; }
        public int IdSensor { get; set; }
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public bool Resolvido { get; set; }
    }
}
