using CSB.Core.Entities;

namespace CSB.Core.Application.Services
{
    public interface IRequestHandler<in TRequest, TResponse> : MediatR.IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
    }
}