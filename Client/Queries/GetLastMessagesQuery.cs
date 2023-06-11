using System.Net.Http;
using Client.Commands;

namespace Client.Queries;

public class GetLastMessagesQuery : CommandBase
{
    private readonly HttpClient _httpClient;
    public override void Execute(object? parameter)
    {
        throw new System.NotImplementedException();
    }
}