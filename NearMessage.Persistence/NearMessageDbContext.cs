using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Users;
using NearMessage.Persistence.EntityTypeConfigurations;
using System.Reflection;

namespace NearMessage.Persistence;

public sealed class NearMessageDbContext : DbContext, INearMessageDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Chat> Chats { get; set; }

    public NearMessageDbContext(DbContextOptions<NearMessageDbContext> options)
        : base(options) 
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ChatConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
