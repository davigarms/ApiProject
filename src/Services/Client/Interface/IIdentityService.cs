using Services.Client.Model;

namespace Services.Client.Interface;

public interface IIdentityService
{
  public Task<Identity[]> GetIdentity();
}