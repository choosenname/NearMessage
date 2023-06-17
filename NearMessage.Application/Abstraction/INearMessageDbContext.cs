using Microsoft.EntityFrameworkCore;
using NearMessage.Domain.Chats;
using NearMessage.Domain.UserGroups;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Abstraction;

public interface INearMessageDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<Chat> Chats { get; set; }

    public DbSet<Domain.Groups.Group> Groups { get; set; }

    public DbSet<Domain.UserGroups.UserGroup> UserGroups { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}