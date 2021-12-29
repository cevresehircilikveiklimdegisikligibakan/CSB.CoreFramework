using CSB.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CSB.Core.Application.Services
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}