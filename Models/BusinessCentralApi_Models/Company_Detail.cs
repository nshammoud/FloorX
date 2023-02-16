using Newtonsoft.Json;
using System;

namespace KQF.Floor.Web.Models.BusinessCentralApi_Models
{
    public class Company_Detail
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "systemVersion")]
        public string SystemVersion { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "businessProfileId")]
        public string BusinessProfileId { get; set; }

        [JsonProperty(PropertyName = "systemCreatedAt")]
        public DateTime SystemCreatedAt { get; set; }

        [JsonProperty(PropertyName = "systemCreatedBy")]
        public string SystemCreatedBy { get; set; }

        [JsonProperty(PropertyName = "systemModifiedAt")]
        public DateTime SystemModifiedAt { get; set; }

        [JsonProperty(PropertyName = "systemModifiedBy")]
        public string SystemModifiedBy { get; set; }
    }
}
