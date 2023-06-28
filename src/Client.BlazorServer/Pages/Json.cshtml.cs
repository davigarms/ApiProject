using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace BlazorServer.Pages;

public class Json : PageModel
{
  public async Task OnGet()
  {
    var accessToken = await HttpContext.GetTokenAsync("access_token");

    var client = new HttpClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    var content = await client.GetStringAsync("https://localhost:6001/identity");

    JsonContent = JArray.Parse(content).ToString();

  }
  
  public string JsonContent { get; set; }
}