using System;
using System.Net.Http;
using System.Threading.Tasks;
using AdvertApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace WebAdvert.Web.ServiceClients
{
    public class AdvertApiClient : IAdvertApiClient
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public AdvertApiClient(IConfiguration config, HttpClient client)
        {
            _client = client;
            _config = config;

            var createUrl = _config.GetSection("AdvertApi").GetValue<string>("CreateUrl");
            _client.BaseAddress = new Uri(createUrl);
            _client.DefaultRequestHeaders.Add("Content-type", "application/json");

        }
        public async Task<AdvertResponse> Create(AdvertModel model)
        {
            var advertApiModel = new AdvertModel(); // automapper
            var jsonModel = JsonConvert.SerializeObject(model);
            var response = await _client.PostAsync(_client.BaseAddress, new StringContent(jsonModel));

            var responseJson = await response.Content.ReadAsStringAsync();

            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);

            var advertResponse = new AdvertResponse(); // automapper

            return advertResponse;
        }
    }
}