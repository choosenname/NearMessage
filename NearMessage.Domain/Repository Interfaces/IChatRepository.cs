using NearMessage.Domain.Entities;

namespace NearMessage.Domain.Repository_Interfaces;

public interface IChatRepository
{
    Chat GetById(int chatId);
    void Create(Chat chat);
    void Update(Chat chat);
    void Delete(int chatId);
}
