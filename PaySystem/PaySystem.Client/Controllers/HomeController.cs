using PaySystem.Client.Models;
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
        private readonly IBillService billService;
        private readonly IStatusBillService statusBillService;

        public HomeController(IUserService userService, IBillService billService, IStatusBillService statusBillService)
        {
            this.userService = userService;
            this.billService = billService;
            this.statusBillService = statusBillService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateBill()
        {
            return View("CreateBill");
        }

        [HttpPost]
        public ActionResult CreateBill(CreateBillModel model)
        {
            var user = userService.GetUsersByEmail(model.EmailOfUser);

            var newBill = new Bill()
            {
                Id = Guid.NewGuid(),
                Balance = 0,
                BillType = (BillType)Enum.Parse(typeof(BillType), model.BillType),
                IBank = model.IBank,
            };

            billService.CreateBillOnUser(user, newBill);
        
            return View(model);
        }

        [HttpGet]
        public ActionResult PutMoney()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PutMoney(PutMoneyModel model)
        {
            var user = userService.GetUsersByEmail(model.Email);
            var billOfUser = billService.GetBillOnUser(user);

            billService.PutMoneyInYourBill(user, model.Money);
            statusBillService.SetStatusBill(billOfUser, "succes");

            return View(model);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}