using System;
namespace
    {
    public class ProductService
    {

        [JsonProperty("products")]
        public List<Product> Products { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("skip")]
        public long Skip { get; set; }
    }
}
