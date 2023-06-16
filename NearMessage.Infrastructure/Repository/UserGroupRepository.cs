using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.UserGroups;

namespace NearMessage.Infrastructure.Repository;

public class UserGroupRepository : IUserGroupRepository
{
    private readonly INearMessageDbContext _context;

    public UserGroupRepository(INearMessageDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserGroup>> AddUserAsync(UserGroup userGroup, CancellationToken cancellationToken)
    {
        await _context.UserGroups.AddAsync(userGroup, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(userGroup);
    }
}