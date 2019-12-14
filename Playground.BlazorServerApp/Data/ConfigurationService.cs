using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Playground.Api.Client.Api;
using Playground.BlazorServerApp.Data.ViewDtos;

namespace Playground.BlazorServerApp.Data
{
    public class ConfigurationService
    {
        private const string ApiPath = "http://localhost:5000";
        private HealthCheckConfigurationApi _service = new HealthCheckConfigurationApi(ApiPath);

        public Task<ConfigurationDto[]> GetConfigurationsAsync()
        {
            return Task.Run(() =>
            {
                var configurations = _service.ApiConfigurationGet(0, 200);

                return configurations.Select(x => new ConfigurationDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    HealthCheckCount = x.HealthCheckCount
                }).ToArray();
            });
        }

        public Task<ConfigurationDto> GetConfiguration(Guid id)
        {
            return Task.Run(() =>
            {
                var configuration = _service.ApiConfigurationIdGet(id);
                return new ConfigurationDto
                {
                    Id = configuration.Id,
                    Name = configuration.Name,
                    CreatedOn = configuration.CreatedOn,
                    HealthCheckCount = configuration.HealthCheckCount
                };
            });
        }

        public Task<List<HealthCheckDto>> GetHealthChecks(Guid id)
        {
            return Task.Run(() =>
            {
                var configuration = _service.ApiConfigurationIdChecksGet(id);
                return configuration.Select(x => new HealthCheckDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Path = x.Path
                }).ToList();
            });
        }
    }
}