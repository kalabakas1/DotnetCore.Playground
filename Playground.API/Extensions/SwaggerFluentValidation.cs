using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Playground.Application.Validators.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Playground.API.Extensions
{
    public class SwaggerFluentValidation : ISchemaFilter
    {
        private readonly IServiceProvider ServiceProvider;

        public SwaggerFluentValidation(IServiceProvider provider)
        {
            ServiceProvider = provider;
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var validator = ServiceProvider
                .GetService(typeof(IValidator<>)
                    .MakeGenericType(context.ApiModel.Type)) as IValidator;
            if (validator == null)
            {
                return;
            }

            if (schema.Required == null)
            {
                schema.Required = new HashSet<string>();
            }

            var validatorDescriptor = validator.CreateDescriptor();
            foreach (var key in schema.Properties.Keys)
            {
                foreach (var propertyValidator in validatorDescriptor
                    .GetValidatorsForMember(ToPascalCase(key)))
                {
                    if (propertyValidator is NotNullValidator
                        || propertyValidator is NotEmptyValidator)
                    {
                        schema.Required.Add(key);
                    }

                    if (propertyValidator is LengthValidator lengthValidator)
                    {
                        if (lengthValidator.Max > 0)
                        {
                            schema.Properties[key].MaxLength = lengthValidator.Max;
                        }

                        schema.Properties[key].MinLength = lengthValidator.Min;
                    }

                    if (propertyValidator is RegularExpressionValidator expressionValidator)
                    {
                        schema.Properties[key].Pattern = expressionValidator.Expression;
                    }

                    if (propertyValidator is FixedListValidator itemListValidator)
                    {
                        //ToDo: Find a better solution for this - might convert it to an enum?
                        var oldType = schema.Type;
                        schema.Type = "string";
                        schema.Properties[key].Enum =
                            itemListValidator.ValidItems.Select(x =>
                            {
                                IOpenApiAny obj = null;
                                var ok = OpenApiAnyFactory.TryCreateFor(schema, x, out obj);
                                return obj;
                            }).ToList();

                        schema.Type = oldType;
                    }
                }
            }
        }

        private static string ToPascalCase(string inputString)
        {
            if (string.IsNullOrEmpty(inputString) || inputString.Length < 2)
            {
                return null;
            }

            return inputString.Substring(0, 1).ToUpper() + inputString.Substring(1);
        }
    }
}