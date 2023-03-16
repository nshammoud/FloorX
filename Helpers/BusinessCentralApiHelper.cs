﻿using KQF.Floor.Web.Models.BusinessCentralApi_Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace KQF.Floor.Web.Helpers
{
    public class BusinessCentralApiHelper
    {
        private static LoginBaseSettingConfig _config;
        private readonly HttpContext _httpContext;
        private static TokenResponse tokenResponse;
        public BusinessCentralApiHelper(LoginBaseSettingConfig config, HttpContext httpContext = null)
        {
            _config = config;
            _httpContext = httpContext;
            if(_httpContext != null)
            {
                if(tokenResponse == null)
                {
                    var tokenResposneVal = _httpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token_response")?.Value;
                    if (!string.IsNullOrEmpty(tokenResposneVal))
                    {
                        tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(tokenResposneVal);
                        if (tokenResponse.Expires_date < DateTime.Now)
                        {
                            var refreshToken = _httpContext.User.Claims.FirstOrDefault(c => c.Type == "refresh_token")?.Value;
                            if (!string.IsNullOrEmpty(refreshToken))
                            {
                                tokenResponse = GetToken(refreshToken);
                            }
                        }
                    }
                }
                else
                {
                    if (tokenResponse.Expires_date < DateTime.Now)
                    {
                        var refreshToken = _httpContext.User.Claims.FirstOrDefault(c => c.Type == "refresh_token")?.Value;
                        if (!string.IsNullOrEmpty(refreshToken))
                        {
                            tokenResponse = GetToken(refreshToken);
                        }
                    }
                }
            }
        }

        //NSH not using this just a test ..
        public TokenResponse GetTokenV2()
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

        public TokenResponse GetToken(string code)
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
                var timeSpan = TimeSpan.FromSeconds(response.Expires_in);
                response.Expires_date = DateTime.Now.AddHours(timeSpan.Hours).AddMinutes(timeSpan.Minutes).AddSeconds(timeSpan.Seconds);

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TResponse GetApiResponse<TResponse>(string url,string accessToken = "")
        {
            try
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    accessToken = tokenResponse.Access_token;
                }
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddHeader("Authorization", $"Bearer {accessToken}");
                var response = client.GetAsync(request).Result;
                if (!response.IsSuccessful) 
                {

                return default;
            }
                else
                {
                    var companies = JsonConvert.DeserializeObject<TResponse>(response.Content);
                    return companies;
                }

            }
            catch (Exception ex)
            {
                return default;
            }
        }


        public TResponse PostApiResponse<TResponse>(string url, object bodyParameters)
        {

            var accessToken = tokenResponse.Access_token;
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(bodyParameters);
            var response = client.PostAsync(request).Result;
            var response_ = JsonConvert.DeserializeObject<TResponse>(response.Content);
            return response_;
        }


        public TResponse PostApiResponse_<TResponse>(string url, object bodyParameters)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header  

                //append the parameter at the end of the request url  
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

                //// the following cod will add the jwt token in the http request header.  
                var accessToken = tokenResponse.Access_token;
                request.Headers.Add("Authorization", $"Bearer {accessToken}");


                request.Content = new StringContent(bodyParameters.ToString(),
                                    Encoding.UTF8,
                                    "application/json");//CONTENT-TYPE header

                //StringContent queryString = new StringContent(bodyParameters.ToString());
                //request.Content = queryString;

                HttpResponseMessage response = client.SendAsync(request).Result;
                var response_ = JsonConvert.DeserializeObject<TResponse>(response.Content.ToString());
                return response_;
            }
            catch (Exception ex) 
            {
                return default;
            }
        }
    }
}
