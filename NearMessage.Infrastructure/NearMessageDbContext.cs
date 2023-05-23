using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Entities;
using NearMessage.Infrastructure.Persistence.EntityTypeConfigurations;
using System.Reflection;

namespace NearMessage.Persistence;

public class NearMessageDbContext : DbContext, INearMessageDbContext
{
    public DbSet<User> Users { get; set; }

    public NearMessageDbContext(DbContextOptions<NearMessageDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
