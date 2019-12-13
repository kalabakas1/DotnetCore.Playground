using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Playground.API.Filters;
using Playground.Application.Validators.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Playground.API.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection SetupSwagger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "1.0"});
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                config.AddFluentValidationRules();
                config.SchemaFilter<SwaggerFluentValidation>(serviceCollection.BuildServiceProvider());
            });
            
            return serviceCollection;
        }

        public static IApplicationBuilder SetupSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                config.DisplayOperationId();
                config.DisplayRequestDuration();
                config.EnableDeepLinking();
            });

            return app;
        }

        public static IServiceCollection SetupValidation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvc(config =>
            {
                config.Filters.Add(typeof(ValidatorActionFilter));
            }).AddFluentValidation(config => config.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Playground."))));
            return serviceCollection;
        }
    }
}