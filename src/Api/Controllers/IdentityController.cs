using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("identity")]
public class IdentityController : ControllerBase
{
  [HttpGet]
  [Authorize]
  public IActionResult Get()
  {
    Console.WriteLine("Getting User claims...");
    return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
  }
}
