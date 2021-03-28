using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SmileBoy.Client.Core;

namespace SmileBoyClient.Extentions
{
    public static class AutoMapperExention
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            return services.AddSingleton(mapper);
        }
    }
}
