using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Playground.Domain.Constants;
using Playground.Domain.Models;
using Playground.Domain.Repositories;

namespace Playground.Application.Commands
{
    public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand, Notification>
    {
        private readonly IHealthCheckConfigurationRepository _healthCheckConfigurationRepository;

        public UpdateConfigurationCommandHandler(IHealthCheckConfigurationRepository healthCheckConfigurationRepository)
        {
            _healthCheckConfigurationRepository = healthCheckConfigurationRepository;
        }
        
        public Task<Notification> Handle(UpdateConfigurationCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var notification = new Notification();
                var current = _healthCheckConfigurationRepository.GetById(request.Id);

                if (current == null)
                {
                    notification.AddError(ExceptionMessage.NoValueFound);
                    return notification;
                }

                current.Name = request.Name;
                _healthCheckConfigurationRepository.Update(current);
                
                return notification;
            }, cancellationToken);
        }
    }
}