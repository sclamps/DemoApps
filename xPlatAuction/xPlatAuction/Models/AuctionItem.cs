using Newtonsoft.Json;

namespace xPlatAuction.Models
{
    public class AuctionItem
    {
        [JsonProperty(PropertyName = "id")] 
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "Text")]
        public string Text { get; set; }

        public bool Done { get; set; }
    }
}