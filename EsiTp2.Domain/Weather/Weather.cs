using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsiTp2.Domain.Weather
{
    public class WeatherInfo
    {
        public string Cidade { get; set; }
        public double Temperatura { get; set; }
        public int Humidade { get; set; }
        public string Descricao { get; set; }
    }
}
