using Newtonsoft.Json;

namespace WK.Catalog.Web.Request
{
    public class CategoryRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
       
    }
}
