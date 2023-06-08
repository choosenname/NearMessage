using NearMessage.Domain.Primitives;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.Chats;

public class Chat
{
    public Chat(Guid chatId, Guid senderId, Guid receiverId)
    {
        ChatId = chatId;
        SenderId = senderId;
        ReceiverId = receiverId;    
    }

    public int Id { get; set; }

    public Guid ChatId { get; set; }

    public virtual User Sender { get; set; }

    public Guid SenderId { get; set; }

    public virtual User Receiver { get; set; }

    public Guid ReceiverId { get; set; }

    public Chat InversedChat => new(ChatId, ReceiverId, SenderId);
}
