namespace NearMessage.Domain.Messages;

public class Media : Message
{
    public Media(Guid id, string content, Guid receiver,
        DateTime sendTime, StreamContent streamContent) 
        : base(id, content, receiver, sendTime)
    {
        StreamContent = streamContent;
    }

    public StreamContent StreamContent { get; set; }
}
