using FluentValidation;  
using System.Collections.Generic;  

namespace Playground.Application.Validators.Extensions
{
    internal static class CustomRuleExtensions  
    {  
        public static IRuleBuilderOptions<TModel, string> LimitedChoice<TModel>(  
            this IRuleBuilder<TModel, string> ruleBuilder, IEnumerable<string> validItems)  
            => ruleBuilder.SetValidator(new FixedListValidator(validItems));  
    }  
}