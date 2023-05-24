using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Application.Users.Commands.UserAuthentication;

public sealed record UserAuthenticationCommand (
    string UserName,
    string Password) : ICommand<Result>;