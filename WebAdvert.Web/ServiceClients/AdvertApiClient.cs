using System;
using System.Net.Http;
using System.Threading.Tasks;
using AdvertApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AutoMapper;
using System.Net;

namespace WebAdvert.Web.ServiceClients
{
    public class AdvertApiClient : IAdvertApiClient
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public AdvertApiClient(IConfiguration config, HttpClient client, IMapper mapper)
        {
            _mapper = mapper;
            _client = client;
            _config = config;

            var createUrl = _config.GetSection("AdvertApi").GetValue<string>("CreateUrl");
            _client.BaseAddress = new Uri(createUrl);
            _client.DefaultRequestHeaders.Add("Content-type", "application/json");

        }

        public async Task<bool> Confirm(ConfirmAdvertRequest model)
        {
            var advertModel = _mapper.Map<ConfirmAdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertModel);

            var response = await _client.PutAsync(new Uri($"{_client.BaseAddress}/confirm"), new StringContent(jsonModel));

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<AdvertResponse> Create(AdvertModel model)
        {
            var advertApiModel = _mapper.Map<AdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(model);
            var response = await _client.PostAsync(new Uri($"{_client.BaseAddress}/create"), new StringContent(jsonModel));

            var responseJson = await response.Content.ReadAsStringAsync();

            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);

            var advertResponse = _mapper.Map<AdvertResponse>(createAdvertResponse);

            return advertResponse;
        }
    }
}