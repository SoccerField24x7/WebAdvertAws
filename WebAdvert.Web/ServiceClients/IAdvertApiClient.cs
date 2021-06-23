using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertApi.Models;
// using CodeNja.AdvertApi.Models;

namespace WebAdvert.Web.ServiceClients
{
    public interface IAdvertApiClient
    {
        Task<AdvertResponse> CreateAsync(CreateAdvertModel model);
        Task<bool> ConfirmAsync(ConfirmAdvertRequest request);
        Task<List<Advertisement>> GetAllAsync();
    }
}