using Services.Client.Model;

namespace Services.Client.Interface;

public interface IEventsService
{
  public Task<Events[]> GetEvents();
}