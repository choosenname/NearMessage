using NearMessage.Domain.Entities;

namespace NearMessage.Application.Abstraction;

public interface IJwtProvider
{
    string Generate(User user);
}
