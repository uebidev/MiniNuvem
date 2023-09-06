using Dominio.Entity.PropersUser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entity
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class User
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName;

        [JsonProperty("maidenName")]
        public string MaidenName;

        [JsonProperty("age")]
        public int Age;

        [JsonProperty("gender")]
        public string Gender;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("phone")]
        public string Phone;

        [JsonProperty("username")]
        public string Username;

        [JsonProperty("password")]
        public string Password;

        [JsonProperty("birthDate")]
        public string BirthDate;

        [JsonProperty("image")]
        public string Image;

        [JsonProperty("bloodGroup")]
        public string BloodGroup;

        [JsonProperty("height")]
        public int Height;

        [JsonProperty("weight")]
        public double Weight;

        [JsonProperty("eyeColor")]
        public string EyeColor;

        [JsonProperty("hair")]
        public Hair Hair;

        [JsonProperty("domain")]
        public string Domain;

        [JsonProperty("ip")]
        public string Ip;

        [JsonProperty("address")]
        public Endereco Address;

        [JsonProperty("macAddress")]
        public string MacAddress;

        [JsonProperty("university")]
        public string University;

        [JsonProperty("bank")]
        public Bank Bank;

        [JsonProperty("company")]
        public Company Company;

        [JsonProperty("ein")]
        public string Ein;

        [JsonProperty("ssn")]
        public string Ssn;

        [JsonProperty("userAgent")]
        public string UserAgent;
    }
}
