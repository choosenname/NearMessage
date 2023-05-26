using System.Windows.Input;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _selectedViewModel;
    public ViewModelBase SelectedViewModel
    {
        get { return _selectedViewModel; }
        set
        {
            _selectedViewModel = value;
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }

    public ICommand UpdateViewCommand { get; set; }

    public MainViewModel()
    {
        UpdateViewCommand = new UpdateViewCommand(this);
    }
}
