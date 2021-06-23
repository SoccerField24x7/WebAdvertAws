using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebAdvert.Web.Models;

namespace WebAdvert.Web.ServiceClients
{
    public class SearchApiClient : ISearchApiClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly string BaseAddress = string.Empty;

        public SearchApiClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
            BaseAddress = config.GetSection("SearchApi").GetValue<string>("url");
        }
        public async Task<List<AdvertType>> Search(string keyword)
        {
            var result = new List<AdvertType>();
            var callUrl = $"{BaseAddress}/search/v1/{keyword.ToLower()}";
            var httpResponse = await _client.GetAsync(new Uri(callUrl));

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var allAdverts = await httpResponse.Content.ReadAsAsync<List<AdvertType>>();
                result.AddRange(allAdverts);
            }

            return result;
        }
    }
}