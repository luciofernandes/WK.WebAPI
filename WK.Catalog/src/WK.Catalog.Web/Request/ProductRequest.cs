using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WK.Catalog.Web.Request
{
   public partial class ProductRequest 
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("category_id")]
        public string CategoryId { get; set; }
    }
}
