using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entity
{
    public class ProductsCollection
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("skip")]
        public long Skip { get; set; }
    }
}
