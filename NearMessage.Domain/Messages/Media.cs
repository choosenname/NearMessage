namespace NearMessage.Domain.Messages;

public class Media : Message
{
    public Media(Guid id, string content, Guid receiver,
        DateTime sendTime, byte[] fileData, string fileName) 
        : base(id, content, receiver, sendTime)
    {
        FileData = fileData;
        FileName = fileName;
    }

    public string FileName { get; set; }

    public byte[] FileData { get; set; }
}
