using AutoMapper;
using AutoMapper.Configuration;
using SympliDevelopment.Api.Mappings;
using SympliDevelopment.Api.Models;
namespace SympliDevelopment.Api.Extenstions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddSearchConfigurations(
             this IServiceCollection services, IConfiguration config)
        {
            services.Configure<GoogleSearchConfig>(
                config.GetSection(GoogleSearchConfig.ConfigVarName));
            //services.Configure<Bing>(
            //    config.GetSection(BingSearchConfig.ConfigVarName));

            return services;
        }

        public static IMapper RegisterMapperProfile(this IServiceCollection collection)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile.MapperProfile());
            });
            return config.CreateMapper();
        }
    }
}
