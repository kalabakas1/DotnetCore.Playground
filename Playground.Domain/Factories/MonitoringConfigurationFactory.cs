using System;
using System.Collections.Generic;
using System.Linq;
using Playground.Data;
using Playground.Data.Dtos;
using Playground.Domain.Models;

namespace Playground.Domain.Factories
{
    public class MonitoringConfigurationFactory : IMonitoringConfigurationFactory
    {
        private readonly IEnumerable<IHealthCheckFactory> _healthCheckFactories;

        public MonitoringConfigurationFactory(IEnumerable<IHealthCheckFactory> healthCheckFactories)
        {
            _healthCheckFactories = healthCheckFactories;
        }
        
        public Notification<MonitoringConfiguration> Create(List<HealthCheckDto> healthCheckDtos)
        {
            var notification = new Notification<MonitoringConfiguration>();
            var configuration = new MonitoringConfiguration();
            foreach (var check in healthCheckDtos)
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

            notification.Value = configuration;
            return notification;
        }
    }
}