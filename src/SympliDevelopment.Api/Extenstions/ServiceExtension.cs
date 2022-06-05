using AutoMapper;
using AutoMapper.Configuration;
using SympliDevelopment.Api.Mappings;
using SympliDevelopment.Api.Interface;
using SympliDevelopment.Api.Models;
using SympliDevelopment.Api.SearchEngine;

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
            //services.AddTransient<GoogleSearchConfig>();
            
            return services;
        }
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IGSearch, GoogleSearch>();
        }

        public static IMapper RegisterMapperProfile(this IServiceCollection collection)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            return config.CreateMapper();
        }
    }
}
