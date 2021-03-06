﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App1.ServiceBase.Handler
{
    public interface IHttpHandler
    {
        Task<T> AuthPostAsync<T>(string token, string baseUrl, string api, string messageBody = null) where T : class;
    }
    public class HttpHandler: IHttpHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> AuthPostAsync<T>(string token, string baseUrl, string api, string messageBody = null) where T: class
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                StringContent stringContent = null;
                if (!string.IsNullOrEmpty(messageBody))
                {
                    stringContent = new StringContent(messageBody, UnicodeEncoding.UTF8, "application/json");
                }

                var result = await httpClient.PostAsync(api, stringContent);
                string resultContent = await result.Content.ReadAsStringAsync();

                var resultObject = JsonConvert.DeserializeObject<T>(resultContent);

                return resultObject;
            }
        }
    }
}
