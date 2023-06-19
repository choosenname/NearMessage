using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;

namespace NearMessage.Domain.UsersInformation;

public interface IUserInformationRepository
{
    Task<Result> SaveUserInformationAsync(UserInformation userInformation,Guid id,
        CancellationToken cancellationToken);
}