using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;

namespace NearMessage.Application.Groups.Commands.CreateGroup;

public sealed record CreateGroupCommand(
    String Name,
    HttpContext HttpContext) : ICommand<Result<Domain.Groups.Group>>;