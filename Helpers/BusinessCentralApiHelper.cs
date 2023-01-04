using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace KQF.Floor.Web.Helpers
{
    public class BusinessCentralApiHelper
    {
        private  static LoginBaseSettingConfig _config;
        public BusinessCentralApiHelper(LoginBaseSettingConfig config)
        {
            _config = config;
        }

        //NSH not using this just a test ..
        public  TokenResponse GetTokenV2()
        {
            var httpClient = new HttpClient();
            var paramets = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", _config.Client_id },
                { "username", "nabil.hammoud@nicerp.com" },
                { "password", "YourPassword"},
                { "client_secret", _config.Client_secret },
                { "scope", "https://api.businesscentral.dynamics.com/.default"},
                { "redirect_uri", _config.Redirect_uri},
            };
            var urlEncoded = new FormUrlEncodedContent(paramets);

            var url = $"{_config.BaseUrl}{_config.Tenant_id}/oauth2/v2.0/token";
            var result = httpClient.PostAsync(url, urlEncoded).Result;
            var content = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<TokenResponse>(content);
            return response;
        }

        public  TokenResponse GetToken(string code)
        {
            try
            {
                var httpClient = new HttpClient();

                var paramets = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", _config.Client_id },
                { "client_secret", _config.Client_secret},
                { "refresh_token", code},
                { "scope", "https://api.businesscentral.dynamics.com/.default"},
                { "redirect_uri", _config.Redirect_uri},
            };

                var urlEncoded = new FormUrlEncodedContent(paramets);
                var url = $"{_config.BaseUrl}{_config.Tenant_id}/oauth2/v2.0/token";
                var result = httpClient.PostAsync(url, urlEncoded).Result;
                var content = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<TokenResponse>(content);

                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public  CompanyApiResponse GetCompanyApiResponse(string accessToken)
        {
            try
            {
                var client = new RestClient("https://api.businesscentral.dynamics.com/v2.0/Sandbox/api/microsoft/automation/v2.0/companies");
                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = client.GetAsync(request).Result;
                var companies = JsonConvert.DeserializeObject<CompanyApiResponse>(response.Content);
                return companies;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class CompanyApiResponse
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string Odatacontext { get; set; }

        [JsonProperty(PropertyName = "value")]
        public Company_Detail[] Companies { get; set; }
    }

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

    public class TokenResponse
    {
        [JsonProperty(PropertyName = "token_type")]
        public string Token_type { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int Expires_in { get; set; }

        [JsonProperty(PropertyName = "ext_expires_in")]
        public int Ext_expires_in { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string Access_token { get; set; }
    }

    public class LoginBaseSettingConfig
    {
        public string BaseUrl { get; set; }

        public string Client_id { get; set; }

        public string Client_secret { get; set; }

        public string Redirect_uri { get; set; }

        public string Tenant_id { get; set; }
    }
}
