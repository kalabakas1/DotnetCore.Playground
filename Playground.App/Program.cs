using System;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Playground.Data.Dtos;
using Playground.Data.Repositories;
using Playground.Domain.Factories;
using Playground.Domain.Services;

namespace Playground.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IHealthCheckFactory, UrlHealthCheckFactory>()
                .AddSingleton(typeof(IRepository<HealthCheckDto>),typeof(InMemoryRepository<HealthCheckDto>))
                .AddSingleton<IMonitoringConfigurationFactory, MonitoringConfigurationFactory>()
                .AddSingleton<IReportFactory, ReportFactory>()
                .AddSingleton<IHealthCheckService, HealthCheckService>()
                .BuildServiceProvider();
            
            var repository = serviceProvider.GetService<IRepository<HealthCheckDto>>();
            var configurationFactory = serviceProvider.GetService<IMonitoringConfigurationFactory>();
            var checks = repository.GetAll();

            var monitoringConfiguration = configurationFactory.Create(checks).Value;
            var healthCheckService = serviceProvider.GetService<IHealthCheckService>();

            var executionReport = healthCheckService.Execute(monitoringConfiguration);

            var json = JsonSerializer.Serialize(executionReport, new JsonSerializerOptions {WriteIndented = true, IgnoreNullValues = true});
            
            Console.WriteLine(json);
        }
    }
}