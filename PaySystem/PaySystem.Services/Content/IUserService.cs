using PaySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Services.Content
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        User GetUsersByEmail(string email);
    }
}
