using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Entities;

namespace NearMessage.Domain.Users;

public interface IUserRepository
{
    Task<Result<User>> CreateUserAsync(User user);
    Task<bool> AuthenticateUserAsync(string requestEmail, string requestPassword);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByNameAsync(string userName);
}
