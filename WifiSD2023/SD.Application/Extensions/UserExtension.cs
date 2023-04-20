using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Entities;

namespace SD.Application.Extensions
{
    public static class UserExtension
    {
        public static User UserWithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }

        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(s => s.UserWithoutPassword());
        }
    }
}
