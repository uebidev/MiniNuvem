using Newtonsoft.Json;

namespace Dominio.Entity
{
    public class Product
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("discountPercentage")]
        public double DiscountPercentage { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("stock")]
        public long Stock { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonProperty("images")]
        public Uri[] Images { get; set; }
    }
}