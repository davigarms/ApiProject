using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("events")]
public class EventController : ControllerBase
{
  [HttpGet]
  [Authorize]
  public IActionResult Get()
  {
    return new JsonResult(new []
    {
      new Event{ Id = "3839ASCCKKGVBJSDLNBNCDRPQLPRTKSHC", Name = "Cabaret Style", Description = "One man's search for truth."},
      new Event{ Id = "3802APMBTBBCNDTJTCHSQVMMMQRJJPQQG", Name = "Boolean Boolean Boolean (Has priority booking)", Description = "One man's search for truth. (Priority Booking to members)"},
      new Event{ Id = "3811AGPSBTQVJQGHLJJDCSRLMLHGNTNGJ", Name = "Macbeth", Description = "One of Shakespeare's most famous plays. Has Ticket Donations of £1 per ticket"},
    });
  }
}

public class Event
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
}