using System;

namespace Client.Models;

public class UserInformationModel : EntityModel
{
    public UserInformationModel(Guid id, string? about, string name) : base(id)
    {
        About = about;
        Name = name;
    }

    public string? About { get; set; }

    public string Name { get; set; }
}