using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Entities;
using NearMessage.Domain.Users;

namespace NearMessage.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly INearMessageDbContext _nearMessageDbContext;

    public UserRepository(INearMessageDbContext nearMessageDbContext)
    {
        _nearMessageDbContext = nearMessageDbContext;
    }

    public Task<bool> AuthenticateUserAsync(string requestEmail, string requestPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<User>> CreateUserAsync(User user)
    {
        await _nearMessageDbContext.Users.AddAsync(user);

        return Result<User>.Success(user);
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByNameAsync(string userName)
    {
        throw new NotImplementedException();
    }
}
