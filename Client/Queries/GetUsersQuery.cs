using Client.Commands;
using Client.Models;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace Client.Queries;

public class GetUsersQuery : QueryBase
{
    private readonly HomeViewModel _homeViewModel;

    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    public GetUsersQuery(HomeViewModel homeViewModel,
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

        if (!response.IsSuccessStatusCode) return;

        var contacts = await response.Content
            .ReadAsAsync<ObservableCollection<ContactModel>>();

        _homeViewModel.Contacts = contacts;

        foreach (var contact in contacts) await SaveEntityModelService.SaveEntityAsync(contact, CancellationToken.None);
    }
}