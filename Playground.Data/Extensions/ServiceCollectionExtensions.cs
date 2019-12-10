using Microsoft.Extensions.DependencyInjection;
using Playground.Data.Repositories;
using Playground.Domain.Dtos;
using Playground.Domain.Repositories;

namespace Playground.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkSqlite().AddDbContext<HealthCheckConfigurationContext>();
            
            return serviceCollection
                .AddScoped(typeof(IHealthCheckConfigurationRepository), typeof(HealthCheckConfigurationRepository));
        } 
    }
}