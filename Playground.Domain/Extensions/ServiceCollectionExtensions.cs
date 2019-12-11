using Microsoft.Extensions.DependencyInjection;
using Playground.Domain.Dtos;
using Playground.Domain.Factories;
using Playground.Domain.Models.HealthChecks;
using Playground.Domain.Repositories;
using Playground.Domain.Services;

namespace Playground.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainDependencies(this IServiceCollection collection)
        {
            return collection.AddScoped<IHealthCheckFactory, UrlHealthCheckFactory>()
                .AddScoped<IReportFactory, ReportFactory>()
                .AddScoped<HealthCheckAbstract, UrlPingHealthCheck>()
                .AddScoped<HealthCheckAbstract, DefaultHealthCheck>()
                .AddScoped<SubscriptionTypeAbstract, LimitedHealthCheckSubscriptionType>()
                .AddScoped<SubscriptionTypeAbstract, TwoOfEachSubscriptionType>()
                .AddScoped<IHealthCheckService, HealthCheckService>();
        } 
    }
} 