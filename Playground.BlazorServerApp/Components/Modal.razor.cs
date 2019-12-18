using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Playground.BlazorServerApp.Services;

namespace Playground.BlazorServerApp.Components
{
    public partial class Modal : IDisposable
    {
        [Inject] private ModalService ModalService { get; set; }

        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public RenderFragment Content { get; set; }
        public ModalParameters Parameters { get; set; }

        protected override void OnInitialized()
        {
            ModalService.OnShow += ShowModal;
            ModalService.OnClose += CloseModal;
        }

        public void ShowModal(string title, RenderFragment content, ModalParameters parameters)
        {
            Title = title;
            Content = content;
            Parameters = parameters;
            IsVisible = true;

            StateHasChanged();
        }

        public void CloseModal()
        {
            IsVisible = false;
            Title = string.Empty;
            Content = null;
            Parameters = new ModalParameters();

            StateHasChanged();
        }

        public void Dispose()
        {
            ModalService.OnShow -= ShowModal;
            ModalService.OnClose -= CloseModal;
        }
    }
}