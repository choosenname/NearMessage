using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, UsersResponse>
{
    private readonly INearMessageDbContext _nearMessageDbContext;

    public GetAllUsersQueryHandler(INearMessageDbContext nearMessageDbContext)
    {
        _nearMessageDbContext = nearMessageDbContext;
    }

    public async Task<UsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _nearMessageDbContext.Users
        .Select(u => u.ToContact())
        .ToListAsync(cancellationToken);

        return new UsersResponse(users);
    }
}