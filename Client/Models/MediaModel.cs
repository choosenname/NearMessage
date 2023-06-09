using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
