using Microsoft.Extensions.DependencyInjection;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.DAL;
using System.Configuration;


namespace SmileBoyClient.Extentions
{
    public static class MongoExtentions
    {
        public static IServiceCollection AddMongoProvider(this IServiceCollection services)
        {
            var connection = ConfigurationManager.ConnectionStrings["smileboydb"];

            return services.AddScoped<IUnitOfWork, UnitOfWork>(p => new UnitOfWork(connection.ConnectionString, connection.Name));
        }
    }
}
