using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Application.Validators
{
    public class SubscriptionTypeValidator : AbstractValidator<SubscriptionTypeAbstract>
    {
        public SubscriptionTypeValidator(IEnumerable<SubscriptionTypeAbstract> subscriptionTypes)
        {
            var types = subscriptionTypes.Select(x => x.Type);
            RuleFor(x => x.Type).NotNull().IsInEnum();
            RuleFor(x => x.Id).NotNull();
        }
    }
}