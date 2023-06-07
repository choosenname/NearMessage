using Client.Commands;
using Client.Models;
using Client.Stores;
using Client.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Client.Queries;

public class GetAllUsersQuery : CommandBase
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    public GetAllUsersQuery(HomeViewModel homeViewModel,
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

        var response = await _httpClient.GetAsync("/users/getall");

        if (response.IsSuccessStatusCode)
        {
            _homeViewModel.Contacts = await response.Content
                .ReadAsAsync<ObservableCollection<ContactModel>>();
        }
    }
}
