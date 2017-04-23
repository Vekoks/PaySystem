using PaySystem.Client.Models;
using PaySystem.Client.Models.BillModels;
using PaySystem.Client.Models.StatusBillModels;
using PaySystem.Models;
using PaySystem.Services.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaySystem.Client.Controllers
{
    public class BillController : Controller
    {
        private readonly IUserService userService;
        private readonly IBillService billService;
        private readonly IStatusBillService statusBillService;

        public BillController(IUserService userService, IBillService billService, IStatusBillService statusBillService)
        {
            this.userService = userService;
            this.billService = billService;
            this.statusBillService = statusBillService;
        }

        // GET: Bill
        public ActionResult Index()
        {
            var user = userService.GetUsersByUserName(this.User.Identity.Name);

            var bills = billService.GetAllBillsOnUser(user);

            var listOfModelBills = new List<AllBillsOnUser>();

            foreach (var bill in bills)
            {
                listOfModelBills.Add(
                    new AllBillsOnUser()
                    {
                        Balance = bill.Balance.ToString(),
                        BillType = bill.BillType,
                        IBank = bill.IBank,
                        BillId = bill.Id.ToString()
                    }
                );
            }

            return View(listOfModelBills);
        }

        
        public ActionResult DetailsBill(string billId)
        {
            var bill = billService.GetBillWithId(billId);

            var allStatusOnBill = statusBillService.GetAllStatusOnBill(bill);

            var listWithStatus = new List<StatusBillModel>();

            foreach (var status in allStatusOnBill)
            {
                listWithStatus.Add(
                new StatusBillModel()
                {
                    Action = status.Action,
                    ActionDate = status.ActionDate,
                    ActionResoult = status.ActionResoult
                }
                );
            }

            return View(listWithStatus);
        }

        // GET: Bill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bill/Create
        [HttpPost]
        public ActionResult Create(CreateBillModel model)
        {
            try
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

                var billOfUser = billService.GetBillOnUser(user, newBill.IBank);

                statusBillService.SetStatusBill(billOfUser, "Create", "succes");

                //return View(model);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(model);
            }
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
            var billOfUser = billService.GetBillOnUser(user, model.IBankOnBillToSetMoney);

            var putMoneyToBillModel = new PutMoneyInBillModel()
            {
                Money = model.Money,
                IBankOnBillFromGetMoney = model.IBankOnBillFromGetMoney,
                IBankOnBillToSetMoney = model.IBankOnBillToSetMoney
            };

            var statusPutBill = billService.PutMoneyInYourBill(user, putMoneyToBillModel);
            statusBillService.SetStatusBill(billOfUser, "Put money", statusPutBill);

            return RedirectToAction("Index", "Home");

            //return View(model);
        }

        [HttpGet]
        public ActionResult GetMoney(string pictureName, string leva)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var pathOfPicture = "~/Content/images/" + pictureName + ".jpg";

            var model = new GetMoneyModel()
            {
                IBnak = "",
                Money = leva,
                PicturePath = pathOfPicture
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult GetMoney(GetMoneyModel model)
        {
            var user = userService.GetUsersByUserName(User.Identity.Name);
            var billOfUser = billService.GetBillOnUser(user, model.IBnak);

            var statusGetMoney = billService.GetMoneyInYourBill(user, model.Money);

            statusBillService.SetStatusBill(billOfUser, "Get money", statusGetMoney);

            return RedirectToAction("Index", "Home");

            //return View(model);
        }


        // GET: Bill/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bill/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bill/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bill/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
