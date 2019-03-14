using Newtonsoft.Json;

namespace BFYOC.Api.ViewModels
{
    public class IcecreamOrder
    {
        [JsonProperty("orderId")]
        public string OrderId { get; set; }
        [JsonProperty("itemOrdered")]
        public string ItemOrdered { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
