using NearMessage.Domain.Primitives;
using NearMessage.Domain.Users;

namespace NearMessage.Domain.Chats;

public class Chat : Entity
{
    public Chat(Guid id,
        Guid senderId, Guid receiverId)
        : base(id)
    {
        Id = id;
        SenderId = senderId;
        ReceiverId = receiverId;
    }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public User Sender { get; set; }

    public User Receiver { get; set; }
}
