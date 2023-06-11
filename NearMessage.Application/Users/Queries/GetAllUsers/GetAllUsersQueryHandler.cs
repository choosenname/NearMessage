using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Contacts;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, UsersResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<UsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var maybeSenderId = _jwtProvider.GetUserId(request.HttpContext.User);

        if (maybeSenderId.HasNoValue)
        {
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>?>(new("Used don't recognized")));
        }

        var maybeSender = await _userRepository.GetByIdAsync(maybeSenderId.Value, cancellationToken);

        if(maybeSender.HasNoValue)
        {
            return new UsersResponse(
                Result.Failure<IEnumerable<Contact>?>(new("Used doesn't exist")));
        }

        var contacts = maybeSender.Value.SentChats
            ?.Select(c => new Contact(
                c.Receiver.Id,
                c.Receiver.Username,
                c.ChatId))
            .ToList();

        return new UsersResponse(Result.Success<IEnumerable<Contact>?>(contacts));
    }
}