using MediatR;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string UserName,
    string Password) : IRequest;
