using Client.Models;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace Client.Commands.Users;

public class GetUsersCommand : CommandBase
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    public GetUsersCommand(HomeViewModel homeViewModel,
        HttpClient httpClient, UserStore userStore)
    {
        _homeViewModel = homeViewModel;
        _httpClient = httpClient;
        _userStore = userStore;
    }

    public override async void Execute(object? parameter)
    {
        if(_homeViewModel.IsSearching) return;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _userStore.Token);

        var response = await _httpClient.GetAsync("/users/getall");

        if (!response.IsSuccessStatusCode) return;

        var contacts = await response.Content
            .ReadAsAsync<ObservableCollection<ContactModel>>();

        _homeViewModel.Contacts = contacts;
        _homeViewModel.IsLoading = false;

        foreach (var contact in contacts) SaveEntityModelService.SaveEntity(contact);
    }
}