using Microsoft.Owin.BuilderProperties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entity.PropersUser
{
    public class Company
    {
        [JsonProperty("address")]
        public Endereco Address;

        [JsonProperty("department")]
        public string Department;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("title")]
        public string Title;
    }
}
