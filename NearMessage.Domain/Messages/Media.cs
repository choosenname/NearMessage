namespace NearMessage.Domain.Messages;

public class Media
{
    public Media(Message message, StreamContent streamContent)
    {
        Message = message;
        StreamContent = streamContent;
    }

    public Message Message { get; set; }

    public StreamContent StreamContent { get; set; }
}
