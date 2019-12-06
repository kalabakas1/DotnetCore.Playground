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
//        private readonly IHealthCheckConfigurationRepository<HealthCheckConfigurationDto> _healthCheckConfigurationHealthCheckConfigurationRepository;
//        private readonly IHealthCheckConfigurationRepository<HealthCheckDto> _healthCheckHealthCheckConfigurationRepository;
        private readonly IEnumerable<IHealthCheckFactory> _healthCheckFactories;

        public HealthCheckConfigurationFactory(
//            IHealthCheckConfigurationRepository<HealthCheckConfigurationDto> healthCheckConfigurationHealthCheckConfigurationRepository,
//            IHealthCheckConfigurationRepository<HealthCheckDto> healthCheckHealthCheckConfigurationRepository,
            IEnumerable<IHealthCheckFactory> healthCheckFactories)
        {
//            _healthCheckConfigurationHealthCheckConfigurationRepository = healthCheckConfigurationHealthCheckConfigurationRepository;
//            _healthCheckHealthCheckConfigurationRepository = healthCheckHealthCheckConfigurationRepository;
            _healthCheckFactories = healthCheckFactories;
        }
        
        public Notification<HealthCheckConfiguration> Create(Guid? configurationId = null)
        {
            var notification = new Notification<HealthCheckConfiguration>();
//            var configuration = new HealthCheckConfiguration();
//            notification.Value = configuration;
//            
//            if (configurationId == null)
//            {
//                return notification;
//            }
//            
//            var configurationDto = _healthCheckConfigurationHealthCheckConfigurationRepository.GetById(configurationId.Value);
//            if (configurationDto == null)
//            {
//                notification.AddError(ExceptionMessage.NoValueFound);
//                return notification;
//            } 
//            
//            configuration.Retries = configurationDto.Retries;
//            configuration.SleepInMillsBetweenRetry = configurationDto.MillsBetweenRetries;
//            configuration.Id = configurationDto.Id;
//            
//            var healthCheckDtos = _healthCheckHealthCheckConfigurationRepository.GetAllByAggregateId(configurationId.Value);
//            foreach (var check in healthCheckDtos)
//            {
//                var factoryToUse = _healthCheckFactories.FirstOrDefault(x =>
//                    x.Type.ToString().Equals(check.Type, StringComparison.InvariantCultureIgnoreCase));
//                
//                if (factoryToUse == null)
//                {
//                    notification.AddError($"No factory implemented for type ${check.Type}:{check.Name}");
//                    continue;
//                }
//
//                var creationNotification = factoryToUse.Create(check);
//                if (!creationNotification.HasError())
//                {
//                    configuration.AddHealthCheck(creationNotification.Value);
//                }
//            }

            return notification;
        }
    }
}