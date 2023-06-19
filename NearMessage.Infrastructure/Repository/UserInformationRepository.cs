using NearMessage.Common.Primitives.Result;
using NearMessage.Domain.Messages;
using NearMessage.Domain.UsersInformation;
using Newtonsoft.Json;
using System.Text;

namespace NearMessage.Infrastructure.Repository;

public class UserInformationRepository : IUserInformationRepository
{
    private readonly string _filePath;

    public UserInformationRepository(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<Result> SaveUserInformationAsync(UserInformation userInformation, Guid id,
        CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(_filePath);

        var json = JsonConvert.SerializeObject(userInformation);
        var encoding = Encoding.UTF8;

        await File.WriteAllTextAsync(Path.Combine(_filePath, $"{id}.json"), json,
            encoding, cancellationToken);

        return Result.Success();
    }
}