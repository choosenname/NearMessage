using NearMessage.Domain.Entities;
using NearMessage.Domain.Repository_Interfaces;
using NearMessage.Domain.Shared;
using NearMessage.Infrastructure.Persistence;

namespace NearMessage.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Result<User> CreateUser()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<User>> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Result<User>.Success(user);
    }

    public void Delete(Guid userId)
    {
        throw new NotImplementedException();
    }

    public List<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public User GetById(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public User GetByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }
}
