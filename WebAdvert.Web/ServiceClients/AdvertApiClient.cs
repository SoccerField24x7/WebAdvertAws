using System;
using System.Net.Http;
using System.Threading.Tasks;
using AdvertApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AutoMapper;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;
using System.Linq;

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

            var createUrl = _config.GetSection("AdvertApi").GetValue<string>("BaseUrl");
            _client.BaseAddress = new Uri(createUrl);
            //_client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        public async Task<bool> ConfirmAsync(ConfirmAdvertRequest model)
        {
            var advertModel = _mapper.Map<ConfirmAdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertModel);

            var response = await _client.PutAsync(new Uri($"{_client.BaseAddress}/confirm"), new StringContent(jsonModel, Encoding.UTF8, "application/json"));

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<AdvertResponse> CreateAsync(CreateAdvertModel model)
        {
            var advertApiModel = _mapper.Map<AdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertApiModel);
            var response = await _client.PostAsync(new Uri($"{_client.BaseAddress}/create"), new StringContent(jsonModel, Encoding.UTF8, "application/json"));

            var responseJson = await response.Content.ReadAsStringAsync();

            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);

            var advertResponse = _mapper.Map<AdvertResponse>(createAdvertResponse);

            return advertResponse;
        }

        public async Task<System.Collections.Generic.List<Advertisement>> GetAllAsync()
        {
            var apiCallResponse = await _client.GetAsync(new Uri($"{_client.BaseAddress}/all"));
            var allAdvertModels = await apiCallResponse.Content.ReadAsAsync<List<AdvertModel>>();

            return allAdvertModels.Select(x => _mapper.Map<Advertisement>(x)).ToList();
        }
    }
}