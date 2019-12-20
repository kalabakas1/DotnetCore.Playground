using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Playground.Api.Client.Model;
using Playground.BlazorServerApp.Data;

namespace Playground.BlazorServerApp.Components
{
    public partial class HealthCheckList : ComponentBase
    {
        [Inject] public ConfigurationService ConfigurationService { get; set; }
        [Parameter] public Guid ConfigurationId { get; set; }
        private List<HealthCheckViewModel> HealthCheckDtos;

        protected override async Task OnParametersSetAsync()
        {
            HealthCheckDtos = await ConfigurationService.GetHealthChecks(ConfigurationId);
        }
    }
}