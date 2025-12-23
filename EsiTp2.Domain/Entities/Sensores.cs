using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsiTp2.Domain.Entities
{
    public class Sensores
    {
        public int Id { get; set; }
        public int IdCondominio { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
