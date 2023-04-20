using SD.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Entities;
using Wifi.SD.Core.Services;

namespace SD.Application.Services
{
    public class UserService : IUserService
    {
        //Mockups

        List<User> users = new List<User>
        {
            new User {Id = new Guid("8e675ab6-678d-4a46-abd5-c2cd2d164eac"), 
                FirstName = string.Empty, LastName = string.Empty, UserName = "Admin", 
                Password= new NetworkCredential("Admin", "1234").SecurePassword},

            new User {Id = new Guid("2d3cfb27-d6bd-4a90-9eaa-3a2ca9c0840d"), 
                FirstName = "Max", LastName = "Mustermann", UserName = "U1", 
                Password = new NetworkCredential("U1", "1111").SecurePassword},
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = users.SingleOrDefault(w => string.Compare(w.UserName, username, true) == 0
                                                 && new NetworkCredential(w.UserName, w.Password).Password == password);

            if (user == null)
            {
                return user;
            }

            return await Task.FromResult(user.UserWithoutPassword());
        }
    }
}
