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
        Guid receiver, byte[] fileData)
        : base(id, content, receiver)
    {
        FileData = fileData;
    }

    public byte[] FileData { get; set; }
}
