using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Domain.Users;

public interface IUserRepository
{
    Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<Maybe<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Maybe<User>> GetByUsernameAsync(string userName, CancellationToken cancellationToken);
}
