using System;
using MediatR;
using Playground.Domain.Models;

namespace Playground.Application.Commands
{
    public class UpdateConfigurationCommand : IRequest<Notification>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}