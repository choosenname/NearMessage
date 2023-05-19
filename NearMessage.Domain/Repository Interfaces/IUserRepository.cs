using NearMessage.Domain.Entities;

namespace NearMessage.Domain.Repository_Interfaces;

public interface IUserRepository
{
    List<User> GetAllUsers();
    Task<IEnumerable<User>> GetAllUsersAsync();

    User GetById(Guid userId);
    Task<User> GetByIdAsync(Guid userId);

    User GetByUsername(string username);
    Task<User> GetByUsernameAsync(string username);

    void CreateUser();
    void CreateUserAsync(User user);

    void Update(User user);
    void Delete(Guid userId);
}
