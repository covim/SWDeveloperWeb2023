using Wifi.SD.Core.Entities;

namespace Wifi.SD.Core.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}