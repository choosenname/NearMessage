using NearMessage.Application.Abstraction;
using NearMessage.Common.Abstractions.Messaging;
using NearMessage.Common.Primitives.Errors;
using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.UsersInformation;

namespace NearMessage.Application.UsersInformation.Command.SaveUserInformation;

public sealed class SaveUserInformationCommandHandler 
    : ICommandHandler<SaveUserInformationCommand, Result>
{
private readonly IJwtProvider _jwtProvider;
private readonly IUserInformationRepository _userInformationRepository;

public SaveUserInformationCommandHandler(IJwtProvider jwtProvider,
    IUserInformationRepository userInformationRepository)
{
    _jwtProvider = jwtProvider;
    _userInformationRepository = userInformationRepository;
}

public async Task<Result> Handle(SaveUserInformationCommand request, CancellationToken cancellationToken)
    {
        var maybeUserId = _jwtProvider.GetUserId(request.HttpContext.User);
        if (maybeUserId.HasNoValue)
            return Result.Failure(
                new Error("Can't find sender identifier"));

        var result = await _userInformationRepository
            .SaveUserInformationAsync(request.UsersInformation, maybeUserId.Value, cancellationToken);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        return result;
    }
}