﻿using App1.Interfaces;
using App1.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpHandler))]
namespace App1.Services
{
    public class HttpHandler : IHttpHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpHandler()
        {
            _httpClientFactory = App.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        }

        public T AuthPost<T>(string token, string baseUrl, string api, string messageBody = null) where T : class
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

                var result = httpClient.PostAsync(api, stringContent);
                string resultContent = result.Result.Content.ReadAsStringAsync().Result;

                var resultObject = JsonConvert.DeserializeObject<T>(resultContent);

                return resultObject;
            }
        }

        public async Task<T> AuthPostAsync<T>(string token, string baseUrl, string api, string messageBody = null) where T : class
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
