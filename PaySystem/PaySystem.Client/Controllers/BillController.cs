﻿using Newtonsoft.Json;
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
using System.Web.Script.Serialization;

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

        // GET: Bill/DetailsBill/billId
        [HttpGet]
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

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Bill/PutMoney
        [HttpGet]
        public ActionResult PutMoney()
        {
            var model = new PutMoneyModel()
            {
                Email = "",
                ExistBills = false,
                IBankOnBillFromGetMoney = "",
                IBankOnBillToSetMoney = "",
                Money = "",
            };

            return View(model);
        }

        // POST: Bill/PutMoney
        [HttpPost]
        public ActionResult PutMoney(PutMoneyModel model)
        {
            var user = userService.GetUsersByEmail(model.Email);
            var billOfUser = billService.GetBillOnUser(user, model.IBankOnBillToSetMoney);

            var putMoneyToBillModel = new PutMoneyInBillModel()
            {
                Money = model.Money,
                IBankOnBillFromGetMoney = model.IBankOnBillFromGetMoney,
                billToSetMoney = billOfUser
            };

            var statusPutBill = billService.PutMoneyInYourBill(putMoneyToBillModel);

            switch (statusPutBill)
            {
                case "no exist bill":
                    model.ExistBills = true;
                    return View(model);

                case "no exist really bill":
                    model.ExistBills = true;
                    return View(model);

                default:
                    //statusBillService.SetStatusBill(billOfUser, "Put money", statusPutBill);
                    return RedirectToAction("Index", "Home");
            }         
        }

        // GET: Bill/PutMoneyFromManyBill
        [HttpGet]
        public ActionResult PutMoneyFromManyBill()
        {
            var model = new ModelForPutMoneyFromManyBills()
            {
                ExistBills = false,
            };

            return View(model);
        }

        // POST: Bill/PutMoneyFromManyBill
        [HttpPost]
        public ActionResult PutMoneyFromManyBill(string model)
        {
            var putMoney = new JavaScriptSerializer().Deserialize<ModelForPutMoneyFromManyBills>(model);

            var user = userService.GetUsersByEmail(putMoney.Email);
            var billOfUser = billService.GetBillOnUser(user, putMoney.IBankOnBillSetMoney);

            var statusPutBill = billService.PutMoneyInYourBillFromManyBills(putMoney);
            
            switch (statusPutBill)
            {
                case "no exist bill":
                    //putMoney.ExistBills = true;
                    //return View(model);
                    return Json(new { status = "no exist bill", message = "no exist bill" });

                case "no exist really bill":
                    //putMoney.ExistBills = true;
                    //return View(model);
                    return Json(new { status = "no exist really bill", message = "no exist really bill" });

                case "no balance in really bill":
                    //putMoney.ExistBills = true;
                    //return View(model);
                    return Json(new { status = "no balance in really bill", message = "no balance in really bill" });

                default:
                    //return RedirectToAction("Index", "Home");
                    return Json(new { status = "Success", message = "Success" });
            }
        }

        // GET: Bill/TransferringMoney
        [HttpGet]
        public ActionResult TransferringMoney()
        {
            var model = new PutMoneyModel()
            {
                Email = "",
                ExistBills = false,
                IBankOnBillFromGetMoney = "",
                IBankOnBillToSetMoney = "",
                Money = "",
            };

            return View(model);
        }

        // POST: Bill/TransferringMoney
        [HttpPost]
        public ActionResult TransferringMoney(PutMoneyModel model)
        {
            var user = userService.GetUsersByEmail(model.Email);
            var billOfUser = billService.GetBillOnUser(user, model.IBankOnBillToSetMoney);

            var putMoneyToBillModel = new PutMoneyInBillModel()
            {
                Money = model.Money,
                IBankOnBillFromGetMoney = model.IBankOnBillFromGetMoney,
                billToSetMoney = billOfUser
            };

            var statusPutBill = billService.TransferringFormBillToBill(putMoneyToBillModel, user);

            switch (statusPutBill)
            {
                case "no exist one of bills":
                    model.ExistBills = true;
                    return View(model);

                default:

                    var billFromGetMoney = billService.GetBillOnUser(user, model.IBankOnBillFromGetMoney);

                    return RedirectToAction("Index", "Home");
            }
        }

        // GET: Bill/GetMoney
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
                IBank = "",
                Money = leva,
                PicturePath = pathOfPicture,
                IsValetIBank = false
            };

            return View(model);
        }

        // POST: Bill/GetMoney
        [HttpPost]
        public ActionResult GetMoney(GetMoneyModel model)
        {
            var user = userService.GetUsersByUserName(User.Identity.Name);
            var billOfUser = billService.GetBillOnUser(user, model.IBank);

            if (billOfUser == null)
            {
                model.IsValetIBank = true;
                return View(model);
            }

            billService.GetMoneyInYourBill(billOfUser, model.Money);

            return RedirectToAction("Index", "Home");
        }

        // GET: Bill/Delete/billId
        public ActionResult Delete(string billId)
        {
            var user = userService.GetUsersByUserName(User.Identity.Name);
            var billOfUser = billService.GetBillWithId(billId);

            statusBillService.DeleteStatusOnBill(billOfUser);
            billService.DeleteBill(billOfUser);

            return RedirectToAction("Index", "Home");

            //return View();
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
