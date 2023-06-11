using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Client.Commands;
using Client.Models;
using Client.Stores;
using Client.ViewModels;

namespace Client.Queries;

public class SearchUserQuery : CommandBase
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    public SearchUserQuery(HomeViewModel homeViewModel,
        HttpClient httpClient, UserStore userStore)
    {
        _homeViewModel = homeViewModel;
        _httpClient = httpClient;
        _userStore = userStore;
    }

    public override async void Execute(object? parameter)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _userStore.Token);

        var response = await _httpClient.GetAsync("/users/search"
                                                  + $"?Username={_homeViewModel.SearchText}");

        if (!response.IsSuccessStatusCode) return;

        var contacts = await response.Content
            .ReadAsAsync<ObservableCollection<ContactModel>>();

        _homeViewModel.Contacts = contacts;
    }
}