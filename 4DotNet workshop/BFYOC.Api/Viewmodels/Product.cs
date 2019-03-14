using Newtonsoft.Json;

namespace BFYOC.Api.Viewmodels
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("flavor")]
        public string Flavor { get; set; }
        [JsonProperty("price-per-scoop")]
        public decimal PricePerScoop { get; set; }
    }
}
