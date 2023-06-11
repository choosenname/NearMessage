using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

public sealed record UsersResponse(Result<IEnumerable<Contact>?> Contacts);