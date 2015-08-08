using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bank;
using Bank.Models;
using Bank.ViewModel;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        //private BankEntities db = new BankEntities();
        BankRepository bankRep = new BankRepository();

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]       
        public ActionResult ListAccounts(string accountType)
        {
            if (accountType == null)
            {
                //ViewBag.Message = "Account Type: " + accountType + "!";
                TempData["Message"] = "Account Type: " + accountType + "!";
                return RedirectToAction("Index");
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var query = bankRep.ListAccounts(accountType);
            ViewBag.Title = accountType + " Bank Accounts";
            return View(query);
        }


        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientBankAccount bankAccount = bankRep.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }

            return View(bankAccount);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankRepository bankRep = new BankRepository();
            ClientBankAccount bankAccount = bankRep.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountNumber, AccountType, ClientID, LastName, FirstName,Balance")] ClientBankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                bool result = bankRep.Update(bankAccount);
                if (!result)
                {
                    TempData["errorMessage"] = "Update failed.";
                    return RedirectToAction("Edit", new { id = bankAccount.AccountNumber });
                }
                TempData["successMessage"] = "Update successful.";
                return RedirectToAction("Details", new {id = bankAccount.AccountNumber});
            }
            return View(bankAccount);
        }

    }
}
