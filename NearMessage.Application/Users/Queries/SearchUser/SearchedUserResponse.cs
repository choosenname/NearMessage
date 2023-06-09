﻿using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;

namespace NearMessage.Application.Users.Queries.SearchUser;

public sealed record SearchedUserResponse(Result<IEnumerable<Contact>> SearchedUsers);