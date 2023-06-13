using NearMessage.Common.Primitives.Result;

namespace NearMessage.Application.Users.Commands.UserAuthentication;

public sealed record AuthenticationResponse(string Token, Guid Id);