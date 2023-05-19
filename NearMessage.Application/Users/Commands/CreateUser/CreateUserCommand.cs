using MediatR;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    Guid Id,
    string UserName,
    string Password) : IRequest;
