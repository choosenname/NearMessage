using Client.Models;
using Client.Services;
using Client.ViewModels;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using System;
using System.IO;
using Client.Properties;
using Client.Stores;

namespace Client.Commands.Users;

public class ChangeAvatarCommand : CommandBase
{
    private readonly UserStore _userStore;
    private readonly HomeViewModel _viewModel;

    public ChangeAvatarCommand(UserStore userStore, HomeViewModel viewModel)
    {
        _userStore = userStore;
        _viewModel = viewModel;
    }

    public override void Execute(object? parameter)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Файлы PNG (*.png)|*.png"
        };

        var result = openFileDialog.ShowDialog();

        if (result != true) return;


        var selectedFilePath = openFileDialog.FileName;
        var destinationFolderPath = Settings.Default.UserPhotoPath;
        var userId = _userStore.User.Id.ToString();

        if (!Directory.Exists(destinationFolderPath))
        {
            Directory.CreateDirectory(destinationFolderPath);
        }

        var destinationFilePath = Path.Combine(destinationFolderPath, userId + ".png");

        File.Copy(selectedFilePath, destinationFilePath, true);

        _viewModel.Avatar = String.Empty;
    }
}