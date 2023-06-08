using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models;

public class MediaModel : MessageModel
{
    public MediaModel(Guid id, string content, 
        Guid receiver, StreamContent streamContent)
        : base(id, content, receiver)
    {
        StreamContent = streamContent;
    }

    public StreamContent StreamContent { get; set; }
}
