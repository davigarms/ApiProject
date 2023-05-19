using WebClient.Model;

namespace WebClient.Services;

public interface IEventsService
{
  public Task<Events[]> GetEvents();
}