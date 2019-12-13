using FluentValidation;
using Playground.Application.Queries;

namespace Playground.Application.Validators.ViewModelValidators
{
    public class ConfigurationValidator : AbstractValidator<ConfigurationView>
    {
        public ConfigurationValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(1).MaximumLength(256);
            RuleFor(x => x.CreatedOn).NotNull().NotEmpty();
            RuleFor(x => x.Retries).InclusiveBetween(0, 30);
            RuleFor(x => x.SleepInMills).InclusiveBetween(0, 60000);
            RuleFor(x => x.HealthCheckCount).InclusiveBetween(0, 12);
        }
    }
}