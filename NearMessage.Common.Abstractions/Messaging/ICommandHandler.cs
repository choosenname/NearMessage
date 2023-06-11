using MediatR;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Common.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : Result
{
}