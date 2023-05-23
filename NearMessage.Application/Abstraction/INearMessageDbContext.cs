using Microsoft.EntityFrameworkCore;
using NearMessage.Domain.Entities;

namespace NearMessage.Application.Abstraction;

public interface INearMessageDbContext
{
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}