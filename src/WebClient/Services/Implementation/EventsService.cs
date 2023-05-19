using IdentityModel.Client;
using Newtonsoft.Json;
using WebClient.Model;

namespace WebClient.Services.Implementation;

public class EventsService : IEventsService
{
  public async Task<Events[]> GetEvents()
  {
    var client = new HttpClient();
    var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
    if (disco.IsError)
      Console.WriteLine(disco.Error);
    
    
    // request token
    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    {
      Address = disco.TokenEndpoint,
      ClientId = "client",
      ClientSecret = "secret",
      Scope = "api1"
    });
    
    if (tokenResponse.IsError)
      Console.WriteLine(tokenResponse.Error);
    
    Console.WriteLine(tokenResponse.Json);
    
    // call api
    var apiClient = new HttpClient();
    apiClient.SetBearerToken(tokenResponse.AccessToken);
    
    var response = await apiClient.GetAsync("https://localhost:6001/events");
    if (!response.IsSuccessStatusCode)
    {
      Console.WriteLine(response.StatusCode);
    }
    
    var content = await response.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<Events[]>(content);
  }
}