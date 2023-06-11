using MediatR;

namespace NearMessage.Common.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}