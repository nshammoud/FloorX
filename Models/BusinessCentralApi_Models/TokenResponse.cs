using Newtonsoft.Json;
using System;

namespace KQF.Floor.Web.Models.BusinessCentralApi_Models
{
    public class TokenResponse
    {
        [JsonProperty(PropertyName = "token_type")]
        public string Token_type { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int Expires_in { get; set; }

        public DateTime Expires_date { get; set; }

        [JsonProperty(PropertyName = "ext_expires_in")]
        public int Ext_expires_in { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string Access_token { get; set; }
    }
}
