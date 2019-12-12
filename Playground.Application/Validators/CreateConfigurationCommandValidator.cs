using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Playground.Application.Commands;
using Playground.Application.Validators.Extensions;
using Playground.Domain.Constants;
using Playground.Domain.Dtos;
using Playground.Domain.Factories;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Application.Validators
{
    public class CreateConfigurationCommandValidator : AbstractValidator<CreateConfigurationCommand>
    {
        public CreateConfigurationCommandValidator(
            IEnumerable<SubscriptionTypeAbstract> subscriptionTypeAbstracts,
            IEnumerable<IHealthCheckFactory> healthCheckFactories,
            IValidator<HealthCheckDto> healthCheckDtoValidator)
        {
            var subscriptionTypes = new HashSet<string>(subscriptionTypeAbstracts.Select(x => x.Type));
            var healthChecks = new HashSet<string>(healthCheckFactories.Select(x => x.Type));
            
            RuleFor(x => x.Retries).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SleepInMillsBetweenRetry).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SubscriptionTypeName).LimitedChoice(subscriptionTypes).NotNull();
            RuleForEach(x => x.HealthChecks).SetValidator(healthCheckDtoValidator);
        }
    }
}