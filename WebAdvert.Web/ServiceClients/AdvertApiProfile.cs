using AdvertApi.Models;
using AutoMapper;

namespace WebAdvert.Web.ServiceClients
{
    public class AdvertApiProfile : Profile
    {
        public AdvertApiProfile() 
        {
            CreateMap<AdvertModel, CreateAdvertModel>().ReverseMap();
            CreateMap<CreateAdvertResponse, AdvertResponse>();
            CreateMap<ConfirmAdvertRequest, ConfirmAdvertModel>();
        }
    }
}