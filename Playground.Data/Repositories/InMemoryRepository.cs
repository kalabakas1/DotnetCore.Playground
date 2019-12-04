using System;
using System.Collections.Generic;
using System.Linq;
using Playground.Data.Dtos;

namespace Playground.Data.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        public Dictionary<Type, HashSet<object>> _data;

        public InMemoryRepository()
        {
            _data = new Dictionary<Type, HashSet<object>>
            {
                {
                    typeof(HealthCheckDto), new HashSet<object>(new List<object>
                    {
                        new HealthCheckDto
                        {
                            Id = 1,
                            Name = "Google",
                            Path = "https://www.Google.com",
                            Type = "UrlPing",
                            ValidResponses = new[] {200, 300}
                        },
                        new HealthCheckDto
                        {
                            Id = 2,
                            Name = "Maps",
                            Path = "https://maps.google.com",
                            Type = "UrlPing",
                            ValidResponses = new[] {200}
                        }
                    })
                }
            };
        }

        public List<T> GetAll()
        {
            if (_data.ContainsKey(typeof(T)))
            {
                return _data[typeof(T)].Select(x => (T) x).ToList();
            }
            
            return new List<T>();
        }
    }
}