using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Maybe;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Commands.UserAuthentication;

public sealed class UserAuthenticationAsyncCommandHandler
    : ICommandHandler<UserAuthenticationCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public UserAuthenticationAsyncCommandHandler(IUserRepository userRepository,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(UserAuthenticationCommand request,
        CancellationToken cancellationToken)
    {
        var maybeUser = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure<string>(new("The user with the specified user name was not found."));
        }

        User user = maybeUser.Value;

        if (!user.VerifyPassword(request.Password))
        {
            return Result.Failure<string>(new("Password wasn't match"));
        }

        string token = _jwtProvider.Generate(user);

        return Result.Success(token);
    }
}
