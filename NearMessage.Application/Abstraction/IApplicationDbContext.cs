using Microsoft.EntityFrameworkCore;
using NearMessage.Domain.Entities;

namespace NearMessage.Application.Abstraction;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}