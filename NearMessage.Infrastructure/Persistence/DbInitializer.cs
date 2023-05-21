namespace NearMessage.Infrastructure.Persistence;

public class DbInitializer
{
    public static void Initialize(NearMessageDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
