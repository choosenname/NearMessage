using Client.Stores;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public MainViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}