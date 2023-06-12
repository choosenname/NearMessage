using Client.Commands;
using Client.Models;
using Client.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Client.Queries;

public class SearchUserQuery : CommandBase
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    public SearchUserQuery(HomeViewModel homeViewModel,
        HttpClient httpClient)
    {
        _homeViewModel = homeViewModel;
        _httpClient = httpClient;
    }

    public override async void Execute(object? parameter)
    {
        var response = await _httpClient.GetAsync("/users/search"
                                                  + $"?Username={_homeViewModel.SearchText}");

        if (!response.IsSuccessStatusCode) return;

        _homeViewModel.Contacts = await response.Content
            .ReadAsAsync<ObservableCollection<ContactModel>>();
    }
}