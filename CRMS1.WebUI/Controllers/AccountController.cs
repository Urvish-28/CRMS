using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL;
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

        SQLRepository<User> Repository1;
        public AccountController()
        {
            Repository1 = new SQLRepository<User>();
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
            var user = Repository1.LoginRepository().Where(x => x.Email == model.Email && x.Password == model.Password).Count();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (user > 0)
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