namespace CSB.Core.Entities
{
    public interface IRequest<out TResponse> : MediatR.IRequest<TResponse>
    {
    }
}