using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entity
{
    public class UserCollection
    {
        [JsonProperty("users")]
        public List<User> Users;

        [JsonProperty("total")]
        public int Total;

        [JsonProperty("skip")]
        public int Skip;

        [JsonProperty("limit")]
        public int Limit;
    }
}
