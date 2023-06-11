using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Application.Users.Commands.UserRegistration;

public sealed record UserRegistrationCommand(
    string Username,
    string Password) : ICommand<Result<string>>;