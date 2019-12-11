using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Playground.Application.Extensions;
using Playground.Domain.Constants;
using Playground.Domain.Dtos;
using Playground.Domain.Factories;
using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;
using Playground.Domain.Repositories;

namespace Playground.Application.Commands
{
    public class CreateConfigurationCommandHandler : IRequestHandler<CreateConfigurationCommand, Notification<Guid>>
    {
        private readonly IHealthCheckConfigurationRepository _healthCheckConfigurationRepository;
        private readonly HashSet<IHealthCheckFactory> _healthCheckFactories;
        private readonly IEnumerable<SubscriptionTypeAbstract> _subscriptionTypeAbstracts;
        private readonly IValidator<CreateConfigurationCommand> _validator;

        public CreateConfigurationCommandHandler(
            IHealthCheckConfigurationRepository healthCheckConfigurationRepository,
            IEnumerable<IHealthCheckFactory> healthCheckFactories,
            IEnumerable<SubscriptionTypeAbstract> subscriptionTypeAbstracts,
            IValidator<CreateConfigurationCommand> validator)
        {
            _healthCheckConfigurationRepository = healthCheckConfigurationRepository;
            _healthCheckFactories = new HashSet<IHealthCheckFactory>(healthCheckFactories);
            _subscriptionTypeAbstracts = subscriptionTypeAbstracts;
            _validator = validator;
        }
        
        public Task<Notification<Guid>> Handle(CreateConfigurationCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var notification = _validator.Validate(request).ToNotification<Guid>();
                if (notification.HasError())
                {
                    return notification;
                }
                
                var healthChecks = new List<HealthCheckAbstract>();
                foreach (var requestHealthCheck in request.HealthChecks ?? new List<HealthCheckDto>())
                {
                    var factory = _healthCheckFactories.FirstOrDefault(x => x.Type == requestHealthCheck.Type);
                    if (factory == null)
                    {
                        notification.AddError($"{ExceptionMessage.NoValueFound}: {nameof(requestHealthCheck.Type)}");
                    }
                    else
                    {

                        var healthCheckNotification = factory.Create(requestHealthCheck);
                        if (healthCheckNotification.HasError())
                        {
                            notification.AddError(healthCheckNotification.ToString());
                        }

                        healthChecks.Add(healthCheckNotification.Value);
                    }
                }

                var subscription = _subscriptionTypeAbstracts.FirstOrDefault(x =>
                    x.Type.Equals(request.SubscriptionTypeName, StringComparison.InvariantCultureIgnoreCase));
                if (subscription == null)
                {
                    notification.AddError($"{ExceptionMessage.NoValueFound}: {nameof(request.SubscriptionTypeName)}");
                }

                var configuration = new HealthCheckConfiguration(request.Retries, request.SleepInMillsBetweenRetry,
                    healthChecks, subscription);

                if (!notification.HasError())
                {
                    notification.Value = configuration.Id;
                    _healthCheckConfigurationRepository.Add(configuration);
                }

                return notification;
            }, cancellationToken);
        }
    }
}