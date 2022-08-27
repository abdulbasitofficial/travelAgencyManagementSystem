using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travals.Models;

namespace Travals.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(AccountModel c)
        {
                AccountModel cat = new AccountModel();
                cat.insert_Account(c);
                return RedirectToAction("Login");
          
        }
        public ActionResult LogIn()
        {
            ViewBag.Result = "";
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(AccountModel c)
        {
            AccountModel cat = new AccountModel();
            AccountModel user = cat.Check_Account(c.AccountEmail);
            if (user != null)
            {
                Session["Fname"] = user.AccountFName;
                Session["UserID"] = user.AccountID;
                Session["Lname"] = user.AccountLName;
                Session["Email"] = user.AccountEmail;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Result = "Invalid User";
                return View("Login");
            }
        }
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Account(int page = 1)
        {
            AccountModel cat = new AccountModel();
            List<AccountModel> files = cat.dispaly_Account();
            ViewBag.TotalPages = (files.Count() / 20) + 1;
            files = files.Skip((page - 1) * 20).Take(20).ToList();
            return View(files);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Delete(int id)
        {
            AccountModel cat = new AccountModel();
            cat.delete_Account(id);
            return RedirectToAction("Account");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            AccountModel cat = new AccountModel();
            AccountModel c = cat.upadate_Account(id);
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(AccountModel c)
        {
            AccountModel cat = new AccountModel();
            cat.upadate_Account(c);
            return RedirectToAction("Account");
        }
    }
}