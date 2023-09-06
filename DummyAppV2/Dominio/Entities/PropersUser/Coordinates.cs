using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entity.PropersUser
{
    public class Coordinates
    {
        [JsonProperty("lat")]
        public double Lat;

        [JsonProperty("lng")]
        public double Lng;
    }
}
