using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

namespace BlazorServer.Pages;

public class _HostAuthModel : PageModel
{
  public IActionResult OnGetLogin2()
  {
    return Challenge(AuthProps(), "oidc");
  }

  public async Task OnGetLogout2()
  {
    await HttpContext.SignOutAsync("Cookies");
    await HttpContext.SignOutAsync("oidc", AuthProps());
  }

  private AuthenticationProperties AuthProps()  => new AuthenticationProperties
    {
      RedirectUri = Url.Content("~/")
    };
}