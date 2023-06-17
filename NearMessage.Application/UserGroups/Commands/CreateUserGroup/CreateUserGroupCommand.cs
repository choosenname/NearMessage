using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.UserGroups;

namespace NearMessage.Application.UserGroups.Commands.CreateUserGroup;

public sealed record CreateUserGroupCommand(
    Guid GroupId,
    HttpContext HttpContext) : ICommand<Result<Guid>>;