using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Playground.Api.Client.Model;
using Playground.BlazorServerApp.Data;
using Playground.BlazorServerApp.Enums;
using Playground.BlazorServerApp.Services;

namespace Playground.BlazorServerApp.Components
{
    public partial class EditConfiguration : ComponentBase
    {
        [Inject] ConfigurationService ConfigurationService { get; set; }
        [Inject] private ModalService ModalService { get; set; }
        [CascadingParameter] private ModalParameters Parameters { get; set; }
        [Parameter] public Guid Id { get; set; }
        
        private readonly Dictionary<FormStates, string> _alertMessages = new Dictionary<FormStates, string>
        {
            {FormStates.Failed, "Failed updating the configuration"},
            {FormStates.Success, "Successfully update configuration"}
        };

        private FormStates State { get; set; } = FormStates.Init;
        ConfigurationView Configuration { get; set; } = new ConfigurationView();
    
        protected override async Task OnInitializedAsync()
        {
            Parameters.SetValues(this);
            Configuration = await ConfigurationService.GetConfiguration(Id);
        }

        private void HandleInvalidSubmit()
        {
            State = FormStates.Failed;
        }

        private async void HandleValidSubmit()
        {
            var result = await ConfigurationService.UpdateConfiguration(Configuration);
            if (result)
            {
                State = FormStates.Success;
                if (Parameters.Any())
                {
                    ModalService.Close();
                }
            }
            else
            {
                State = FormStates.Failed;
            }
        }
    }
}