using Wifi.SD.Core.Entities;

namespace Wifi.SD.Core.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}