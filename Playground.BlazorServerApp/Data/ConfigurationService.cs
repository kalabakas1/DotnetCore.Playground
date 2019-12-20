using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Playground.Api.Client.Api;
using Playground.Api.Client.Model;

namespace Playground.BlazorServerApp.Data
{
    public class ConfigurationService
    {
        private const string ApiPath = "http://localhost:5000";
        private HealthCheckConfigurationApi _service = new HealthCheckConfigurationApi(ApiPath);

        public Task<ConfigurationView[]> GetConfigurationsAsync()
        {
            return Task.Run(() =>
            {
                var configurations = _service.ApiConfigurationGet(0, 200);

                return configurations.ToArray();
            });
        }

        public Task<ConfigurationView> GetConfiguration(Guid id)
        {
            return Task.Run(() =>
            {
                var configuration = _service.ApiConfigurationIdGet(id);
                return configuration;
            });
        }

        public Task<List<HealthCheckViewModel>> GetHealthChecks(Guid id)
        {
            return Task.Run(() =>
            {
                var configuration = _service.ApiConfigurationIdChecksGet(id);
                return configuration;
            });
        }

        public Task<bool> UpdateConfiguration(ConfigurationView configurationView)
        {
            return Task.Run(() =>
            {
                try
                {
                    var response = _service.ApiConfigurationIdPatchAsyncWithHttpInfo(configurationView.Id.ToString().ToUpperInvariant(),
                        new UpdateConfigurationCommand
                        {
                            Id = configurationView.Id,
                            Name = configurationView.Name
                        }).Result;

                    return response.StatusCode == HttpStatusCode.OK;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}