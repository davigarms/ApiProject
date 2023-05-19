using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("identity")]
[Authorize]
public class IdentityController : ControllerBase
{
  [HttpGet]
  public IActionResult Get()
  {
    Console.WriteLine("Getting User claims...");
    return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
  }
}