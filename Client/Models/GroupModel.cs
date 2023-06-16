using System;

namespace Client.Models;

public class GroupModel
{
    public GroupModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
}