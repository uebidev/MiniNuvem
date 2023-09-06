using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entity.PropersUser
{
    public class Hair
    {
        [JsonProperty("color")]
        public string Color;

        [JsonProperty("type")]
        public string Type;
    }
}
