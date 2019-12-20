using System;
using Microsoft.AspNetCore.Components;

namespace Playground.BlazorServerApp.Pages
{
    public partial class Configurations : ComponentBase
    {
        public Guid Id { get; set; }

        void ConfigurationSelected(Guid id)
        {
            Id = id;
        }
    }
}