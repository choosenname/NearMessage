using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, UsersResponse>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<UsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserId(request.HttpContext.User);

        if (maybeSenderId.HasNoValue)
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>?>(new Error("Used don't recognized")));

        var maybeSender = await _userRepository.GetByIdAsync(maybeSenderId.Value, cancellationToken);

        if (maybeSender.HasNoValue)
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>?>(new Error("Used doesn't exist")));

        var contacts = maybeSender.Value.SentChats
            ?.Select(c =>
            {
                if (c.Receiver != null)
                    return new Contact(
                        c.Receiver.Id,
                        c.Receiver.Username,
                        c.ChatId);
                return null;
            })
            .ToList();

        var groupContacts = maybeSender.Value.UserGroups
            ?.Select(ug =>
            {
                if (ug.Group != null)
                    return new Contact(
                        ug.GroupId,
                        ug.Group.Name,
                        ug.GroupId);
                return null;
            })
            .ToList();

        if (groupContacts != null) contacts?.AddRange(groupContacts);

        return new UsersResponse(Result.Success<IEnumerable<Contact>?>(contacts));
    }
}