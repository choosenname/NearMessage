namespace NearMessage.Domain.Messages;

public class Media : Message
{
    public Media(Guid id, string content, Guid receiverChat,
        DateTime sendTime, byte[] fileData, string fileName) 
        : base(id, content, receiverChat, sendTime)
    {
        FileData = fileData;
        FileName = fileName;
    }

    public string FileName { get; set; }

    public byte[] FileData { get; set; }
}
