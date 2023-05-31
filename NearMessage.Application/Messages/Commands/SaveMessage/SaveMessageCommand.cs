using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Application.Messages.Commands.SaveMessage;

public sealed record class SaveMessageCommand(
    string Content) : ICommand<Result>;
