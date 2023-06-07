using Microsoft.EntityFrameworkCore;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Abstraction;

public interface INearMessageDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<Chat> Chats { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}