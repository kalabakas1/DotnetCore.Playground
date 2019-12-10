using System;
using System.Collections.Generic;
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
            var serviceProvider = new ServiceCollection()
                .RegisterDomainDependencies()
                .RegisterRepositories()
                .BuildServiceProvider();

            var repository = serviceProvider.GetService<IHealthCheckConfigurationRepository>();
//            var configuration = repository.GetById(Guid.Parse("1535E825-590B-4629-8BE2-2E28866CE7F2"));
//            configuration.AddHealthCheck(new DefaultHealthCheck("Default"));
//            configuration.SubscriptionType = new PreventOldConfigurationSubscriptionType();
//            configuration.AddHealthCheck(new DefaultHealthCheck("Default"));
            
            var configuration = new HealthCheckConfiguration
            {
                Id = Guid.NewGuid(),
                Retries = 5,
                CreatedOn = DateTime.Now,
                SleepInMillsBetweenRetry = 30000,
                SubscriptionType = new TwoOfEachSubscriptionType()
            };
            
            repository.Add(configuration);
            
            var json = JsonSerializer.Serialize(configuration, 
                new JsonSerializerOptions {WriteIndented = true, IgnoreNullValues = true});
            Console.WriteLine(json);
        }
    }
}