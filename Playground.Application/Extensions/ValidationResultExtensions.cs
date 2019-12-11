using FluentValidation.Results;
using Playground.Domain.Models;

namespace Playground.Application.Extensions
{
    public static class ValidationResultExtensions
    {
        public static Notification<T> ToNotification<T>(this ValidationResult result)
        {
            var notification = new Notification<T>();
            if (result.Errors.Count > 0)
            {
                foreach (var validationFailure in result.Errors)
                {
                    notification.AddError(validationFailure.ToString());
                }
            }

            return notification;
        }
    }
}