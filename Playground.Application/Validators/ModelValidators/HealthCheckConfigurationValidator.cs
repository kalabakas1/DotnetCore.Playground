using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Playground.Domain.Dtos;
using Playground.Domain.Factories;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Application.Validators
{
    public class HealthCheckConfigurationValidator : AbstractValidator<HealthCheckConfiguration>
    {
        public HealthCheckConfigurationValidator(
            IValidator<HealthCheckAbstract> healthcheckAbstractValidator,
            IValidator<SubscriptionTypeAbstract> subscriptionTypeValidator)
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Retries).InclusiveBetween(0, 10);
            RuleFor(x => x.SleepInMillsBetweenRetry).InclusiveBetween(0, 60000);
            RuleFor(x => x.SubscriptionType).SetValidator(subscriptionTypeValidator);
            RuleForEach(x => x.HealthChecks).SetValidator(healthcheckAbstractValidator);
        }
    }
}