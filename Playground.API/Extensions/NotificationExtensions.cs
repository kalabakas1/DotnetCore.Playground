using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Playground.Domain.Models;

namespace Playground.API.Extensions
{
    public static class NotificationExtensions
    {
        internal static HttpError  ConvertToHttpError<T>(this Notification<T> notification, int statusCode)
        {
            var error = new HttpError
            {
                StatusCode = statusCode,
                Messages = notification.Errors?.Select(x => x.Message).ToList()
            };

            return error;
        }
    }

    internal class HttpError
    {
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; }
    }
}