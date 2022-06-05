using AutoMapper;
using SympliDevelopment.Api.Models;

namespace SympliDevelopment.Api.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<HttpResponseMessage, GSResponse>();
        }
    }
}
