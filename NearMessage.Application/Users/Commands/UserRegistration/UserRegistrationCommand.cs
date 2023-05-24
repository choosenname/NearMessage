using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed record UserRegistrationCommand(
    string UserName,
    string Password) : ICommand<Result>;
