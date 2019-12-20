using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Playground.BlazorServerApp.Enums;

namespace Playground.BlazorServerApp.Components
{
    public partial class FormAlerts : ComponentBase
    {
        [Parameter] public FormStates State { get; set; }
        [Parameter] public Dictionary<FormStates, string> Texts { get; set; }
    }
}