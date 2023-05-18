using NearMessage.Domain.Entities;

namespace NearMessage.Domain.Repository_Interfaces;

public interface IMessageRepository
{
    Message GetById(int messageId);
    void Create(Message message);
    void Update(Message message);
    void Delete(int messageId);
    List<Message> GetMessagesByChatId(int chatId);
    // Другие методы работы с сообщениями
}
