using Microsoft.AspNetCore.Http;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Queries.SearchUser;

public sealed record SearchUserQuery(
    string Username,
    HttpContext Context) : IQuery<SearchedUserResponse>;