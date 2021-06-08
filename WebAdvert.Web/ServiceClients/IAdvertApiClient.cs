using System.Threading.Tasks;
using AdvertApi.Models;
// using CodeNja.AdvertApi.Models;

namespace WebAdvert.Web.ServiceClients
{
    public interface IAdvertApiClient
    {
        Task<AdvertResponse> Create(AdvertModel model);
        Task<bool> Confirm(ConfirmAdvertRequest request);
    }
}