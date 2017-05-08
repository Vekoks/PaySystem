using PaySystem.Client.Models;
using PaySystem.Client.Models.UsersModels;
using PaySystem.Models;
using PaySystem.Services.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaySystem.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserDetails()
        {
            var user = userService.GetUsersByUserName(this.User.Identity.Name);

            var model = new UserDetailsViewModel
            {
                FirstName = user.FIsrtName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(model);
        }
    }
}