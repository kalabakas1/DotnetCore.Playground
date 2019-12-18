using System;
using System.Collections.Generic;

namespace Playground.BlazorServerApp.Services
{
    public class ModalParameters
    {
        private Dictionary<string, object> _parameters;

        public ModalParameters()
        {
            _parameters = new Dictionary<string, object>();
        }

        public ModalParameters Add(string key, object value)
        {
            _parameters[key] = value;
            return this;
        }

        public T TryGet<T>(string key)
        {
            if (!_parameters.ContainsKey(key))
            {
                return default(T);
            }

            return (T) _parameters[key];
        }

        public T Get<T>(string key)
        {
            if (!_parameters.ContainsKey(key))
            {
                throw new KeyNotFoundException($"No key present called:{key}");
            }

            return (T) _parameters[key];
        }

        //ToDo: Performance might suck...
        public void SetValues<T>(T obj)
        {
            Type type = typeof(T);
            foreach (var item in _parameters)
            {
                type?.GetProperty(item.Key)?.SetValue(obj, item.Value, null);
            }
        }
    }
}