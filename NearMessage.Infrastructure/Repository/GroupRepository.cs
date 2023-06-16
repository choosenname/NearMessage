using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Groups;
using NearMessage.Domain.Users;

namespace NearMessage.Infrastructure.Repository;

public class GroupRepository : IGroupRepository
{
    private readonly INearMessageDbContext _context;

    public GroupRepository(INearMessageDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Group>> CreateGroupAsync(Group group, CancellationToken cancellationToken)
    {
        await _context.Groups.AddAsync(group, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(group);
    }
}