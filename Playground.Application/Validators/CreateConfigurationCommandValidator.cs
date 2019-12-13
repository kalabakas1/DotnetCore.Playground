using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Playground.Application.Commands;
using Playground.Domain.Constants;
using Playground.Domain.Dtos;
using Playground.Domain.Factories;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Application.Validators
{
    public class CreateConfigurationCommandValidator : AbstractValidator<CreateConfigurationCommand>
    {
        public CreateConfigurationCommandValidator(
            IValidator<HealthCheckDto> healthCheckDtoValidator)
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Retries).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SleepInMillsBetweenRetry).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SubscriptionTypeName).IsInEnum().NotNull();
            RuleForEach(x => x.HealthChecks).SetValidator(healthCheckDtoValidator);
        }
    }
}