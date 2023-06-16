using System;

namespace Client.Models;

public class UserInformationModel : EntityModel
{
    public UserInformationModel(Guid id, string? about) : base(id)
    {
        About = about;
    }

    public string? About { get; set; }
}