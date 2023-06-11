using System.Security.Claims;
using NearMessage.Common.Primitives.Maybe;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Abstraction;

public interface IJwtProvider
{
    string Generate(User user);
    Maybe<Guid> GetUserId(ClaimsPrincipal principal);
}