using Client.Stores;

namespace Client.ViewModels;

public class ChatViewModel : ViewModelBase
{
    private readonly UserStore _userStore;

    public ChatViewModel(UserStore userStore)
    {
        _userStore = userStore;
    }
    public string Text
    {
        get => _userStore.Token;
        set { }
    }
}
