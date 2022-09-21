using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WK.Catalog.Web.Response
{


    public partial class ProductsResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public Product[] Data { get; set; }
    }

    public partial class ProductResponse
    {
        [JsonProperty("data")]
        public Product Data { get; set; }
    }

    public partial class Product
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

    }


}