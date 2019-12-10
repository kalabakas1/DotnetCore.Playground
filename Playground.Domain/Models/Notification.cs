using System;
using System.Collections.Generic;
using System.Linq;
using Playground.Domain.Constants;

namespace Playground.Domain.Models
{

    public class Notification<T> : Notification
    {
        private T _value;
        public T Value
        {
            get
            {
                if (HasError() || _value == null)
                {
                    throw new ArgumentException(ExceptionMessage.NoValueFound);
                }

                return _value;
            }
            set => _value = value;
        }
    }
    public class Notification
    {
        public List<Error> Errors { get; }

        public Notification()
        {
            Errors = new List<Error>();
        }

        public void AddError(string message, Exception exception = null)
        {
            Errors.Add(new Error(message, exception));
        }

        public void AddError(Exception exception)
        {
            Errors.Add(new Error(exception));
        }

        public bool HasError()
        {
            return Errors.Any();
        }

        public override string ToString()
        {
            return
                $"Error: {HasError()} \n {string.Join("\n", Errors?.Select(x => x.Message ?? string.Empty).ToList() ?? new List<string>())}";
        }

        public class Error
        {
            public string Message { get; }
            public Exception Exception { get; }

            public Error(string message)
            {
                Message = message;
            }

            public Error(string message, Exception exception)
            {
                Message = message;
                Exception = exception;
            }

            public Error(Exception exception)
            {
                Message = exception.Message;
                Exception = exception;
            }
        }
    }
}