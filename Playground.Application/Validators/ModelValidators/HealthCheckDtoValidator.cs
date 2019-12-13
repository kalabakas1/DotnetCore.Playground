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
        public HealthCheckDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Type).IsInEnum().NotNull().NotEmpty();
        }
    }
}