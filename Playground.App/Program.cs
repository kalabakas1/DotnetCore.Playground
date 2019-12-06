using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Playground.Data;
using Playground.Data.Extensions;
using Playground.Data.Repositories;
using Playground.Domain.Dtos;
using Playground.Domain.Extensions;
using Playground.Domain.Factories;
using Playground.Domain.Models.HealthChecks;
using Playground.Domain.Repositories;
using Playground.Domain.Services;

namespace Playground.App
{
    class Program
    { 
        static void Main(string[] args)
        {
//            if (File.Exists("./Database.db"))
//            {
//                File.Delete("./Database.db");    
//            }
            
            var serviceProvider = new ServiceCollection()
                .RegisterDomainDependencies()
                .RegisterRepositories()
                .BuildServiceProvider();

            var repository = serviceProvider.GetService<IHealthCheckConfigurationRepository>();
            var all = repository.GetAll(); 
            
            var configuration = repository.GetById(Guid.Parse("570234D8-B2A9-45F6-97B0-6F7E216A83DB"));
//            var configuration = new HealthCheckConfiguration{Id = Guid.Parse("570234D8-B2A9-45F6-97B0-6F7E216A83DB")};
//
                 
            
//            configuration.AddHealthCheck(new DefaultHealthCheck("Default"));
            
//            repository.Add(configuration);
             
//            repository.Update(configuration);
            
            var json = JsonSerializer.Serialize(configuration, 
                new JsonSerializerOptions {WriteIndented = true, IgnoreNullValues = true});
            Console.WriteLine(json);
//            
//            var configurationFactory = serviceProvider.GetService<IHealthCheckConfigurationFactory>();
//            var monitoringConfiguration = configurationFactory.Create(new Guid("7b4de58b-db31-4d61-a1b2-d8bcdaa742ee")).Value;
//            var healthCheckService = serviceProvider.GetService<IHealthCheckService>();
//
//            var executionReport = healthCheckService.Execute(monitoringConfiguration);
//
//            var json = JsonSerializer.Serialize(executionReport, new JsonSerializerOptions {WriteIndented = true, IgnoreNullValues = true});
//            
//            Console.WriteLine(json);
        }
    }
}