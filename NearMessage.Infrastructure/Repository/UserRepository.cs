using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Entities;
using NearMessage.Domain.Users;

namespace NearMessage.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly INearMessageDbContext _context;

    public UserRepository(INearMessageDbContext nearMessageDbContext)
    {
        _context = nearMessageDbContext;
    }

    public async Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);

        return Result<User>.Success(user);
    }

    public async Task<Maybe<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Users.SingleOrDefaultAsync(i => i.Id == id, cancellationToken);

    public async Task<Maybe<User>> GetByUsernameAsync(string userName, CancellationToken cancellationToken) =>
        await _context.Users.SingleOrDefaultAsync(i => i.Username == userName, cancellationToken);
}