using NearMessage.Domain.Primitives;

namespace NearMessage.Domain.Entities;

public class User : Entity
{
    public User(Guid id, string userName, string password)
        : base(id)
    {
        UserName = userName;
        Password = password;
        CreatedAt = DateTime.Now;
    }

    public string UserName { get; set; }
    public string Password { get; set; }

    public bool IsActive { get; }
    public DateTime CreatedAt { get; set; }
    //public List<int> Connections { get; set; }

    public bool VerifyPassword(string password) => password == Password;
}
