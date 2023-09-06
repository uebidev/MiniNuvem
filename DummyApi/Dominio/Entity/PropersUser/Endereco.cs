﻿using Microsoft.Owin.BuilderProperties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entity.PropersUser
{
    public class Endereco
    {

        [JsonProperty("address")]
        public string Address ;

        [JsonProperty("city")]
        public string City;

        //[JsonProperty("coordinates")]
        //public string Coordinates;

        [JsonProperty("postalCode")]
        public string PostalCode;

        [JsonProperty("state")]
        public string State;
    }
}
