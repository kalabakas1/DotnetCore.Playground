using FluentValidation;
using Playground.Domain.Dtos;

namespace Playground.Application.Validators
{
    public class HealthCheckDtoValidator : AbstractValidator<HealthCheckDto>
    {
        public HealthCheckDtoValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Type).NotNull();
        }
    }
}