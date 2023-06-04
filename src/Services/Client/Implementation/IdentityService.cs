using IdentityModel.Client;
using Newtonsoft.Json;
using Services.Client.Interface;
using Services.Client.Model;

namespace Services.Client.Implementation;

public class IdentityService : IIdentityService
{
  public async Task<Identity[]> GetIdentity()
  {
    var client = new HttpClient();
    var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
    if (disco.IsError)
      Console.WriteLine((string?)disco.Error);
    
    
    // request token
    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    {
      Address = disco.TokenEndpoint,
      ClientId = "client",
      ClientSecret = "secret",
      Scope = "api1"
    });
    
    if (tokenResponse.IsError)
      Console.WriteLine((string?)tokenResponse.Error);
    
    Console.WriteLine((object?)tokenResponse.Json);
    
    // call api
    var apiClient = new HttpClient();
    apiClient.SetBearerToken(tokenResponse.AccessToken);
    
    var response = await apiClient.GetAsync("https://localhost:6001/identity");
    if (!response.IsSuccessStatusCode)
    {
      Console.WriteLine(response.StatusCode);
    }
    
    var content = await response.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<Identity[]>(content);
  }
}