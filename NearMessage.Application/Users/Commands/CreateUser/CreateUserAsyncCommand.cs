using NearMessage.Application.Abstraction.Messaging;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed record CreateUserAsyncCommand(
    Guid Id,
    string UserName,
    string Password) : ICommand;
