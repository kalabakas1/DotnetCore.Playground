using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Playground.Application.Commands;
using Playground.Domain.Constants;
using Playground.Domain.Factories;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Application.Validators
{
    public class CreateConfigurationCommandValidator : AbstractValidator<CreateConfigurationCommand>
    {
        private readonly IEnumerable<SubscriptionTypeAbstract> _subscriptionTypeAbstracts;
        private readonly IEnumerable<IHealthCheckFactory> _healthCheckFactories;

        public CreateConfigurationCommandValidator(
            IEnumerable<SubscriptionTypeAbstract> subscriptionTypeAbstracts,
            IEnumerable<IHealthCheckFactory> healthCheckFactories)
        {
            _subscriptionTypeAbstracts = subscriptionTypeAbstracts;
            _healthCheckFactories = healthCheckFactories;
            
            var subscriptionTypes = new HashSet<string>(_subscriptionTypeAbstracts.Select(x => x.Type));
            var healthChecks = new HashSet<string>(_healthCheckFactories.Select(x => x.Type));
            
            RuleFor(x => x.SubscriptionTypeName).NotNull();
            RuleFor(x => x.Retries).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SleepInMillsBetweenRetry).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SubscriptionTypeName).Must(x => subscriptionTypes.Contains(x)).WithMessage(string.Format(ExceptionMessage.MustContainValue, string.Join(',', subscriptionTypes)));

            RuleForEach(x => x.HealthChecks).ChildRules(x =>
            {
                x.RuleFor(z => z.Type).Must(z => healthChecks.Contains(z))
                    .WithMessage(string.Format(ExceptionMessage.MustContainValue, string.Join(',', healthChecks)));
                x.RuleFor(z => z.Name).NotNull();
            });
        }
    }
}