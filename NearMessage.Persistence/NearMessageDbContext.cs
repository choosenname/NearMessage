using Microsoft.EntityFrameworkCore;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Groups;
using NearMessage.Domain.UserGroups;
using NearMessage.Domain.Users;
using NearMessage.Persistence.EntityTypeConfigurations;
using System.Reflection;

namespace NearMessage.Persistence;

public sealed class NearMessageDbContext : DbContext, INearMessageDbContext
{
    public NearMessageDbContext(DbContextOptions<NearMessageDbContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
