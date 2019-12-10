using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Playground.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCommandHandlerDependencies(this IServiceCollection collection)
        {
            return collection.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}