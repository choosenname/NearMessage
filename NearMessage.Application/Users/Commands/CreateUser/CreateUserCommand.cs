using NearMessage.Application.Abstraction.Messaging;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string UserName,
    string Password) : ICommand;
