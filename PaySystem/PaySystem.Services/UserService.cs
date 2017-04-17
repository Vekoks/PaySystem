using PaySystem.Data.Repository;
using PaySystem.Models;
using PaySystem.Services.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Services
{
    public class UserService : IUserService
    {
        private readonly IPaySystemRepository<User> _userRepo;

        public UserService(IPaySystemRepository<User> userRepository)
        {
            this._userRepo = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepo.All();
        }

        public User GetUsersByEmail(string email)
        {
            return _userRepo.All().Where(x =>x.Email.Contains(email)).FirstOrDefault();
        }

        public User GetUsersByUserName(string userName)
        {
            return _userRepo.All().Where(x => x.UserName.Contains(userName)).FirstOrDefault();
        }
    }
}
