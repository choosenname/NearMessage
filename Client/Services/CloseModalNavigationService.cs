using Client.Interfaces;
using Client.Stores;
using Client.ViewModels;

namespace Client.Services;

public class CloseModalNavigationService : INavigationService
{
    private readonly ModalNavigationStore _navigationStore;

    public CloseModalNavigationService(ModalNavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void Navigate()
    {
        _navigationStore.Close();
    }
}