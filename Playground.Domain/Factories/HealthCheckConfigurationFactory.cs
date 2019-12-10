using System;
using System.Collections.Generic;
using System.Linq;
using Playground.Domain.Constants;
using Playground.Domain.Dtos;
using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;
using Playground.Domain.Repositories;

namespace Playground.Domain.Factories
{
    public class HealthCheckConfigurationFactory : IHealthCheckConfigurationFactory
    {
        private readonly IEnumerable<IHealthCheckFactory> _healthCheckFactories;

        public HealthCheckConfigurationFactory(
            IEnumerable<IHealthCheckFactory> healthCheckFactories)
        {
            _healthCheckFactories = healthCheckFactories;
        }
        
        public Notification<HealthCheckConfiguration> Create(int retries, int millsBetween, List<HealthCheckAbstract> healtchecks)
        {
            var notification = new Notification<HealthCheckConfiguration>();
            var configuration = new HealthCheckConfiguration();
            
            configuration.Retries = retries;
            configuration.SleepInMillsBetweenRetry = millsBetween;
            
            foreach (var check in healtchecks)
            {
                var factoryToUse = _healthCheckFactories.FirstOrDefault(x =>
                    x.Type.ToString().Equals(check.Type, StringComparison.InvariantCultureIgnoreCase));
                
                if (factoryToUse == null)
                {
                    notification.AddError($"No factory implemented for type ${check.Type}:{check.Name}");
                    continue;
                }

                var creationNotification = factoryToUse.Create(check);
                if (!creationNotification.HasError())
                {
                    configuration.AddHealthCheck(creationNotification.Value);
                }
            }

            return notification;
        }
    }
}