namespace NearMessage.Domain.Primitives;

public abstract class Entity
{
    protected Entity(Guid id)
        : this()
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public Guid Id { get; protected set; }
}