using NearMessage.Domain.Contacts;

namespace NearMessage.Domain.Messages;

public class Media : Message
{
    public Media(Guid id, string content, Contact contact,
        DateTime sendTime, byte[] fileData, string fileName) 
        : base(id, content, sendTime, contact)
    {
        FileData = fileData;
        FileName = fileName;
    }

    public string FileName { get; set; }

    public byte[] FileData { get; set; }
}
