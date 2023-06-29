using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace BlazorServer.Controller;

public class ApiController : Microsoft.AspNetCore.Mvc.Controller
{
  [Route("callapi")]
  public async Task<IActionResult> CallApi()
  {
    var accessToken = await HttpContext.GetTokenAsync("access_token");

    var client = new HttpClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    var content = await client.GetStringAsync("https://localhost:6001/identity");

    ViewBag.Json = JArray.Parse(content).ToString();
    return View("api");
  }
}