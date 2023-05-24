using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Entities;

namespace NearMessage.Domain.Users;

public interface IUserRepository
{
    Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken);

    Task<Maybe<User>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Maybe<User>> GetUserByNameAsync(string userName, CancellationToken cancellationToken);
}
