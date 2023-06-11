using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;

namespace NearMessage.Domain.Users;

public interface IUserRepository
{
    Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<Maybe<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Maybe<User>> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetUsersByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<Result<Contact>> ConvertToContactAsync(User user, Guid user1, CancellationToken cancellationToken);
}
