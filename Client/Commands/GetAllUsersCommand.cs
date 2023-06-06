using Client.Models;
using Client.Stores;
using Client.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Client.Commands;

public class GetAllUsersCommand : CommandBase
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    public GetAllUsersCommand(HomeViewModel homeViewModel,
        HttpClient httpClient, UserStore userStore)
    {
        _homeViewModel = homeViewModel;
        _httpClient = httpClient;
        _userStore = userStore;

        Execute(null);
    }

    public override async void Execute(object? parameter)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _userStore.Token);

        var response = await _httpClient.GetAsync("https://localhost:7196/users/getall");

        if (response.IsSuccessStatusCode)
        {
            _homeViewModel.Users = await response.Content.ReadAsAsync<List<UserModel>>();
        }
    }
}
