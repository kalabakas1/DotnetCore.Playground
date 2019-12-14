using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Playground.Data;

namespace Playground.Application.Queries
{
    public class ConfigurationQueries : DbContext, IConfigurationQueries
    {
        private DbSet<ConfigurationView> Configurations { get; set; }
        private DbSet<HealthCheckViewModel> HealthChecks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite(DataConstants.ConnectionString);

        public ConfigurationView Get(Guid id)
        {
            return Configurations.FromSqlRaw($@"
            SELECT 
            hcc.Id
            , hcc.Name
            , hcc.CreatedOn
            , hcc.Retries
            , hcc.SleepInMillsBetweenRetry AS SleepInMills
            , Count(*) AS HealthCheckCount
            FROM HealthCheckConfiguration hcc 
            JOIN HealthCheck hc ON hcc.Id = hc.HealthCheckConfigurationId
            WHERE hcc.Id = '{id.ToString().ToUpperInvariant()}'
            GROUP BY
            hcc.Id, hcc.Name, hcc.CreatedOn, hcc.Retries, hcc.SleepInMillsBetweenRetry").FirstOrDefault();
        }

        public IEnumerable<ConfigurationView> GetPagedConfigurations(int pageNumber, int pageSize)
        {
            return Configurations.FromSqlRaw($@"
            SELECT 
            hcc.Id
            , hcc.Name
            , hcc.CreatedOn
            , hcc.Retries
            , hcc.SleepInMillsBetweenRetry AS SleepInMills
            , Count(*) AS HealthCheckCount
            FROM HealthCheckConfiguration hcc 
            JOIN HealthCheck hc ON hcc.Id = hc.HealthCheckConfigurationId
            GROUP BY
            hcc.Id, hcc.Name, hcc.CreatedOn, hcc.Retries, hcc.SleepInMillsBetweenRetry").Skip(pageNumber * pageSize).Take(pageSize);
        }

        public IEnumerable<HealthCheckViewModel> GetChecksByConfiguration(Guid id)
        {
            return HealthChecks.FromSqlRaw($@"
                SELECT 
                Id
                , Name
                , Path
                , Alias
                FROM HealthCheck 
                WHERE HealthCheckConfigurationId = '{id.ToString().ToUpperInvariant()}'");
        }
    }
}