using NearMessage.Domain.Shared;
using MediatR;

namespace NearMessage.Application.Abstraction.Messaging;

public interface ICommand : IRequest<Result> { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }