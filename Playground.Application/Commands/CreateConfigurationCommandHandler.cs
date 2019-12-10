using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Playground.Domain.Constants;
using Playground.Domain.Dtos;
using Playground.Domain.Factories;
using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;
using Playground.Domain.Repositories;

namespace Playground.Application.Commands
{
    public class CreateConfigurationCommandHandler : IRequestHandler<CreateConfigurationCommand, Guid>
    {
        private readonly IHealthCheckConfigurationRepository _healthCheckConfigurationRepository;
        private readonly IEnumerable<IHealthCheckFactory> _healthCheckFactories;
        private readonly IEnumerable<SubscriptionTypeAbstract> _subscriptionTypeAbstracts;

        public CreateConfigurationCommandHandler(
            IHealthCheckConfigurationRepository healthCheckConfigurationRepository,
            IEnumerable<IHealthCheckFactory> healthCheckFactories,
            IEnumerable<SubscriptionTypeAbstract> subscriptionTypeAbstracts)
        {
            _healthCheckConfigurationRepository = healthCheckConfigurationRepository;
            _healthCheckFactories = healthCheckFactories;
            _subscriptionTypeAbstracts = subscriptionTypeAbstracts;
        }
        
        public Task<Guid> Handle(CreateConfigurationCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var healthChecks = new List<HealthCheckAbstract>();
                foreach (var requestHealthCheck in request.HealthChecks ?? new List<HealthCheckDto>())
                {
                    var factory = _healthCheckFactories.FirstOrDefault(x => x.Type == requestHealthCheck.Type);
                    if (factory == null)
                    {
                        throw new ArgumentException(
                            $"{ExceptionMessage.NoValueFound}: {nameof(requestHealthCheck.Type)}");
                    }

                    var healthCheckNotification = factory.Create(requestHealthCheck);
                    if (healthCheckNotification.HasError())
                    {
                        throw new ArgumentException(healthCheckNotification.ToString());
                    }

                    healthChecks.Add(healthCheckNotification.Value);
                }

                var subscription = _subscriptionTypeAbstracts.FirstOrDefault(x =>
                    x.Type.Equals(request.SubscriptionTypeName, StringComparison.InvariantCultureIgnoreCase));
                if (subscription == null)
                {
                    throw new ArgumentException(
                        $"{ExceptionMessage.NoValueFound}: {nameof(request.SubscriptionTypeName)}");
                }

                var configuration = new HealthCheckConfiguration(request.Retries, request.SleepInMillsBetweenRetry,
                    healthChecks, subscription);

                _healthCheckConfigurationRepository.Add(configuration);

                return configuration.Id;
            }, cancellationToken);
        }
    }
}