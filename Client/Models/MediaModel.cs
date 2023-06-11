using System;

namespace Client.Models;

public class MediaModel : MessageModel
{
    public MediaModel(Guid id, string content,
        Guid receiver, byte[] fileData, string fileName)
        : base(id, content, receiver)
    {
        FileData = fileData;
        FileName = fileName;
    }

    public string FileName { get; set; }

    public byte[] FileData { get; set; }
}