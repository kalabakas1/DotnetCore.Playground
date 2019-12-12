using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Playground.Application.Validators.Extensions;
using Playground.Domain.Dtos;
using Playground.Domain.Factories;

namespace Playground.Application.Validators
{
    public class HealthCheckDtoValidator : AbstractValidator<HealthCheckDto>
    {
        private readonly IEnumerable<IHealthCheckFactory> _healthCheckFactories;

        public HealthCheckDtoValidator(IEnumerable<IHealthCheckFactory> healthCheckFactories)
        {
            var healthChecks = new HashSet<string>(healthCheckFactories.Select(x => x.Type));

            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Type).LimitedChoice(healthChecks);
        }
    }
}