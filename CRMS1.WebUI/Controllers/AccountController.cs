using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL;
using CRMS1.SQL.Repositories.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CRMS1.WebUI.Controllers
{
    public class AccountController : Controller
    {
        LoginRepository repository;
        public AccountController()
        {
            repository = new LoginRepository();
        }

        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var user = repository.Login(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (user != null)
                {
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
        }


        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}