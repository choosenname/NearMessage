using Client.Models;
using System.Diagnostics;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Client.ViewModels;

namespace Client.Commands.Messages;

public class SaveMediaCommand : CommandBase
{
    private readonly ChatViewModel _chatViewModel;

    public SaveMediaCommand(ChatViewModel chatViewModel)
    {
        _chatViewModel = chatViewModel;
    }

    public override async void Execute(object? parameter)
    {
        if (_chatViewModel.SelectedMessage?.FileData == null
            || _chatViewModel.SelectedMessage?.FileName == null)
            return;

        var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads";
        var filePath = Path.Combine(downloadsPath, _chatViewModel.SelectedMessage.FileName);

        if (!File.Exists(filePath))
        {
            await File.WriteAllBytesAsync(filePath, _chatViewModel.SelectedMessage.FileData, CancellationToken.None);
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = filePath,
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(startInfo);
    }
}