using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entity.PropersUser
{
    public class Bank
    {
        [JsonProperty("cardExpire")]
        public string CardExpire;

        [JsonProperty("cardNumber")]
        public string CardNumber;

        [JsonProperty("cardType")]
        public string CardType;

        [JsonProperty("currency")]
        public string Currency;

        [JsonProperty("iban")]
        public string Iban;
    }
}
