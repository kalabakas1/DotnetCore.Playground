using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Commands;
using Playground.Application.Validators;
using Playground.Domain.Dtos;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCommandHandlerDependencies(this IServiceCollection collection)
        {
            return collection.AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient<IValidator<CreateConfigurationCommand>, CreateConfigurationCommandValidator>()
                
                .AddTransient<IValidator<HealthCheckDto>, HealthCheckDtoValidator>()
                
                .AddTransient<IValidator<HealthCheckConfiguration>, HealthCheckConfigurationValidator>()
                .AddTransient<IValidator<HealthCheckAbstract>, HealthCheckAbstractValidator>()
                .AddTransient<IValidator<SubscriptionTypeAbstract>, SubscriptionTypeValidator>();
        }
    }
}