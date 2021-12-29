using MediatR;
using System.Threading.Tasks;

namespace CSB.Core.Application.Services
{
    public abstract class ServiceBase
    {
        protected IMediator Mediator { get; }
        public ServiceBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected async Task<TResponse> SendRequest<TResponse>(IRequest<TResponse> request)
        {
            var response = await Mediator.Send(request);
            return response;
        }
    }
}