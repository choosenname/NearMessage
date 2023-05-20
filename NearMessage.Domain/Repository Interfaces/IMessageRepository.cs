using NearMessage.Domain.Entities;

namespace NearMessage.Domain.Repository_Interfaces;

public interface IMessageRepository
{
    Message GetById(int messageId);
    Task<Message> GetByIdAsync(int messageId);

    void Create(Message message);
    void Update(Message message);
    void Delete(int messageId);
}
