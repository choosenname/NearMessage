using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Users;
using NearMessage.Persistence.EntityTypeConfigurations;

namespace NearMessage.Persistence;

public class NearMessageDbContext : DbContext, INearMessageDbContext
{
    public NearMessageDbContext(DbContextOptions<NearMessageDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Chat> Chats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ChatConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}