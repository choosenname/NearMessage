using System;

namespace Client.Models;

public class EntityModel
{
    public EntityModel(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; protected set; }
}