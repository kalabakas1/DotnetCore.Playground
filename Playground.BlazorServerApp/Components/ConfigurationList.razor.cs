using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Playground.Api.Client.Model;
using Playground.BlazorServerApp.Data;
using Playground.BlazorServerApp.Services;

namespace Playground.BlazorServerApp.Components
{
    public partial class ConfigurationList : ComponentBase
    {
        [Inject] private ConfigurationService ConfigurationService { get; set; }
        [Inject] private ModalService ModalService { get; set; }
        [Parameter] public EventCallback<Guid> OnConfigurationSelection { get; set; }

        private ConfigurationView[] configurations;

        protected override async Task OnInitializedAsync()
        {
            configurations = await ConfigurationService.GetConfigurationsAsync();
        }
    }
}