using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorServer.Pages;

public class LogoutModel : PageModel
{
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task OnGetAsync()
  {
    var authProps = new AuthenticationProperties
    {
      RedirectUri = Url.Content("~/")
    };
    
    await HttpContext.SignOutAsync("Cookies");
    await HttpContext.SignOutAsync("oidc", authProps);
    
    
  }
}
