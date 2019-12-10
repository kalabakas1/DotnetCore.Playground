using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Playground.Domain.Models.HealthChecks;
using Playground.Domain.Repositories;

namespace Playground.Data.Repositories
{
    public class HealthCheckConfigurationRepository : IHealthCheckConfigurationRepository
    {
        private readonly HealthCheckConfigurationContext _context;
        private readonly IEnumerable<HealthCheckAbstract> _healthCheckImplementations;
        private string[] _validHealthChecks;

        public HealthCheckConfigurationRepository(
            HealthCheckConfigurationContext context,
            IEnumerable<HealthCheckAbstract> healthCheckImplementations)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _healthCheckImplementations = healthCheckImplementations;
            _validHealthChecks = _healthCheckImplementations.Select(x => x.Alias).ToArray();
        }
        public HealthCheckConfiguration GetById(Guid id)
        {
            var configuration = _context.HealthCheckConfigurations
                .Include(x => x.HealthChecks)    
                .FirstOrDefault(x => x.Id.Equals(id));

            if (configuration == null)
            {
                return null;
            }
            
            configuration.HealthChecks =
                configuration.HealthChecks.Where(x => _validHealthChecks.Contains(x.Alias)).ToList();
            
            return configuration;
        }

        public void Add(HealthCheckConfiguration configuration)
        {
            if (configuration != null)
            {
                var type = _context.SubscriptionAbstracts.FirstOrDefault(x => x.Id == configuration.SubscriptionType.Id);
                if (type != null)
                {
                    configuration.SubscriptionType = type;
                }
            }
            
            _context.HealthCheckConfigurations.Add(configuration);
            _context.SaveChanges(); 
        }

        public void Update(HealthCheckConfiguration configuration)
        {
            _context.Entry(configuration).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<HealthCheckConfiguration> GetAll()
        {
            return _context.HealthCheckConfigurations.ToList();
        }
    }
}