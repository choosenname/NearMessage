using System;
using Client.Properties;
using Client.Stores;
using Client.ViewModels;
using Microsoft.Win32;
using System.IO;

namespace Client.Commands.Users;

public class ChangeSelectedAvatarCommand : CommandBase
{
    private readonly HomeViewModel _viewModel;

    public ChangeSelectedAvatarCommand(HomeViewModel viewModel)
    {
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
        if (_viewModel.SelectedContact != null)
        {
            var userId = _viewModel.SelectedContact.Id.ToString();

            if (!Directory.Exists(destinationFolderPath))
            {
                Directory.CreateDirectory(destinationFolderPath);
            }

            var destinationFilePath = Path.Combine(destinationFolderPath, userId + ".png");

            File.Copy(selectedFilePath, destinationFilePath, true);
        }

        _viewModel.Avatar = String.Empty;
    }
}