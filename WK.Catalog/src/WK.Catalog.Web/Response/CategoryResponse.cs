using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WK.Catalog.Web.Response
{ 
    public partial class CategoriesResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public Category[] Data { get; set; }
    }


    public partial class CategoryResponse
    {
        [JsonProperty("data")]
        public Category Data { get; set; }
    }
    public partial class Category
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }

    public partial class Meta
    {
        [JsonProperty("current_page")]
        public long CurrentPage { get; set; }

        [JsonProperty("per_page")]
        public long PerPage { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

}
