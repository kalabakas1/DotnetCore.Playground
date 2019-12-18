using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Playground.BlazorServerApp.Services
{
    public class ModalService
    {
        public event Action<string, RenderFragment, ModalParameters> OnShow;
        public event Action OnClose;

        public void Show<T>(string title)
        {
            Show<T>(title, new ModalParameters());
        }

        public void Show<T>(string title, ModalParameters parameters)
        {
            if (parameters == null)
            {
                parameters = new ModalParameters();
            }

            if (typeof(T).BaseType != typeof(ComponentBase))
            {
                throw new ArgumentException($"{typeof(T).FullName} must be a Blazor Component");
            }

            var content = new RenderFragment(x =>
            {
                x.OpenComponent(1, typeof(T));
                x.CloseComponent();
            });
            OnShow?.Invoke(title, content, parameters);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}