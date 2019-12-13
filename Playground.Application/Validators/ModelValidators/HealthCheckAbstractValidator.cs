using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Playground.Application.Validators.Extensions;
using Playground.Domain.Factories;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Application.Validators
{
    public class HealthCheckAbstractValidator : AbstractValidator<HealthCheckAbstract>
    {
        public HealthCheckAbstractValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Alias).IsInEnum().NotNull().NotEmpty();
        }
    }
}