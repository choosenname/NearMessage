using MediatR;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Common.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : Result
{
}