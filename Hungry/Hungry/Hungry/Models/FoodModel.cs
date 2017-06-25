using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungry.Models
{
    class FoodModel
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url1")]
        public string URL1 { get; set; }

        [JsonProperty(PropertyName = "url1thumb")]
        public string URL1Thumb { get; set; }

        [JsonProperty(PropertyName = "url2")]
        public string URL2 { get; set; }

        [JsonProperty(PropertyName = "url2thumb")]
        public string URL2Thumb { get; set; }

        [JsonProperty(PropertyName = "url3")]
        public string URL3 { get; set; }

        [JsonProperty(PropertyName = "url3thumb")]
        public string URL3Thumb { get; set; }

        [JsonProperty(PropertyName = "url4")]
        public string URL4 { get; set; }

        [JsonProperty(PropertyName = "url4thumb")]
        public string URL4Thumb { get; set; }

        [JsonProperty(PropertyName = "url5")]
        public string URL5 { get; set; }

        [JsonProperty(PropertyName = "url5thumb")]
        public string URL5Thumb { get; set; }

        [JsonProperty(PropertyName = "url6")]
        public string URL6 { get; set; }

        [JsonProperty(PropertyName = "url6thumb")]
        public string URL6Thumb { get; set; }
    }
}
