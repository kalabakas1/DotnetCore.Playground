using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.Domain.Models;

namespace Playground.API.Extensions
{
    public static class NotificationExtensions
    {
        internal static HttpError  ToHttpError<T>(this Notification<T> notification, HttpStatusCode statusCode)
        {
            return ToHttpError(statusCode, notification.Errors);
        }
        
        internal static HttpError  ToHttpError(this Notification notification, HttpStatusCode statusCode)
        {
            return ToHttpError(statusCode, notification.Errors);
        }

        private static HttpError ToHttpError(HttpStatusCode statusCode, List<Notification.Error> errors)
        {
            var error = new HttpError
            {
                StatusCode = (int)statusCode,
                Messages = errors?.Select(x => x.Message).ToList()
            };

            return error;
        }

        internal static HttpError ToHttpError(this ModelStateDictionary modelState, HttpStatusCode statusCode)
        {
            return new HttpError
            {
                StatusCode = (int) statusCode,
                Messages = modelState.Select(x => x.Value).SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                    .ToList()
            };
        }
    }

    internal class HttpError
    {
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; }
    }
}