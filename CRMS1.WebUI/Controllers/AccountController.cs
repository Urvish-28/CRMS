using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using CRMS1.SQL;
using CRMS1.SQL.Repositories.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRMS1.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        public AccountController(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
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
            var user = _loginService.Login(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (user != null)
                {
                    Session["Name"] = _userService.GetAllUsers().Where(b => b.Email == model.Email).Select(x => x.UserName).FirstOrDefault();
                    Session["UserId"] = _userService.GetAllUsers().Where(b => b.Email == model.Email).Select(x => x.Id).FirstOrDefault();
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is invalid");
                    return View("Login");
                }
            }
        }

        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Login", "Account");
        }
    }
}