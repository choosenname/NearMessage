using System.Threading;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.ViewModels;
using static System.String;
using static Client.Services.SaveEntityModelService;

namespace Client.Commands.Navigation;

public class ExitCommand : CommandBase
{
    private readonly SettingsViewModel _viewModel;
    private readonly INavigationService _navigationService;


    public ExitCommand(SettingsViewModel viewModel, INavigationService navigationService)
    {
        _viewModel = viewModel;
        _navigationService = navigationService;
    }

    public override async void Execute(object? parameter)
    {
        var userInformation = new UserInformationModel(
            _viewModel.UserStore.User.Id,
            _viewModel.About ?? Empty);

        await SaveEntityAsync(userInformation, CancellationToken.None);

        _navigationService.Navigate();
    }
}