namespace Playground.Application.Validators.Extensions
{
    using FluentValidation.Validators;
    using System.Collections.Generic;
    using System.Linq;
    using static System.String;

    public class FixedListValidator
        : PropertyValidator
    {
        public IEnumerable<string> ValidItems { get; private set; }

        public FixedListValidator(IEnumerable<string> validItems)
            : base("{PropertyName} can contain only these values {ElementValues}.")
        {
            ValidItems = validItems;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context?.PropertyValue as string;
            if (!IsNullOrEmpty(value) && !ValidItems.Contains(value))
            {
                context.MessageFormatter.AppendArgument("ElementValues", Join(", ", ValidItems));
                return false;
            }

            return true;
        }
    }
}