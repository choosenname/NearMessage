using System;
using Client.Models;
using Client.Properties;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;

namespace Client.Services;

public static class LoadEntityModelService
{
    public static async Task<UserInformationModel> LoadEntityAsync(Guid userId, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserInformationPath);

        Directory.CreateDirectory(directoryPath);

        var fileName = Path.Combine(directoryPath, userId.ToString());

        if (!File.Exists(fileName)) return new UserInformationModel(userId, null);

        var json = await File.ReadAllTextAsync(fileName, cancellationToken);

        var userInformation = JsonConvert.DeserializeObject<UserInformationModel>(json);

        return userInformation ?? new UserInformationModel(userId, null);
    }

    public static UserInformationModel LoadEntity(Guid userId)
    {
        var directoryPath = Path.Combine(
            Settings.Default.UserInformationPath);

        Directory.CreateDirectory(directoryPath);

        var fileName = Path.Combine(directoryPath, $"{userId.ToString()}.json");

        if (!File.Exists(fileName)) return new UserInformationModel(userId, null);

        var json = File.ReadAllText(fileName);

        var userInformation = JsonConvert.DeserializeObject<UserInformationModel>(json);

        return userInformation ?? new UserInformationModel(userId, null);
    }
}