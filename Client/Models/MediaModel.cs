﻿using System;

namespace Client.Models;

public class MediaModel : MessageModel
{
    public MediaModel(Guid id, string content, Guid sender,
        Guid receiverChatId, byte[]? fileData = null, string? fileName = null)
        : base(id, content, sender, receiverChatId)
    {
        FileData = fileData;
        FileName = fileName;
    }

    public string? FileName { get; set; }

    public byte[]? FileData { get; set; }
}