namespace NearMessage.Domain.Messages;

public class Media : Message
{
    public Media(Guid id, string content, Guid receiver,
        DateTime sendTime, byte[] fileData) 
        : base(id, content, receiver, sendTime)
    {
        FileData = fileData;
    }

    public byte[] FileData { get; set; }
}
