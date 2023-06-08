using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMessage.Application.Messages.Queries.GetMessages;

public sealed record MessagesResponse(Result<IEnumerable<Message>> Messages);
