using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;

namespace NearMessage.Application.Users.Queries.SearchUser;

public sealed record SearchUserQuery(
    string Username,
    HttpContext HttpContext) : IQuery<SearchedUserResponse>;