using NearMessage.Domain.Entities;
using NearMessage.Domain.Shared;

namespace NearMessage.Domain.Repository_Interfaces;

public interface IUserRepository
{
    List<User> GetAllUsers();
    Task<List<User>> GetAllUsersAsync();

    User GetById(Guid userId);
    Task<User> GetByIdAsync(Guid userId);

    User GetByUsername(string username);
    Task<User> GetByUsernameAsync(string username);

    Result<User> CreateUser();
    Task<Result<User>> CreateUserAsync(User user);

    void Update(User user);
    void Delete(Guid userId);
}
