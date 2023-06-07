using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Users;

namespace NearMessage.Application.Users.Commands.CreateUser;

public sealed class UserRegistrationAsyncCommandHandler
    : ICommandHandler<UserRegistrationCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public UserRegistrationAsyncCommandHandler(IUserRepository userRepository,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(UserRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var user = new User(
            id,
            request.Username,
            request.Password);

        var result = await _userRepository.CreateUserAsync(user, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<string>(result.Error);
        }

        var token = _jwtProvider.Generate(user);

        return Result.Success(token);
    }
}
