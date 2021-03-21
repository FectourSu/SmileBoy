using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SmileBoy.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
