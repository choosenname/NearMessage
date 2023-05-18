using NearMessage.Domain.Entities;

namespace NearMessage.Domain.Repository_Interfaces;

public interface IUserRepository
{
    User GetById(int userId);
    User GetByUsername(string username);
    void Create(User user);
    void Update(User user);
    void Delete(int userId);
    void SendMessage(Message message);
}
