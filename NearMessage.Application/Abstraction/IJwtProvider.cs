using NearMessage.Domain.Users;

namespace NearMessage.Application.Abstraction;

public interface IJwtProvider
{
    string Generate(User user);
}
