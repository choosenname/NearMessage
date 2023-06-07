using NearMessage.Domain.Contacts;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

public sealed record UsersResponse(IEnumerable<Contact> Сontacts);