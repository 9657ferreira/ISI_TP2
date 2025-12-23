using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsiTp2.Domain.Entities
{
    public class Condominio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }
        public string CodigoPostal { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Ativo { get; set; }  
    }
}
