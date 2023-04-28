using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using CRMS1.SQL;
using CRMS1.SQL.Repositories.Login;
using CRMS1.WebUI.Filters;
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
        private readonly IUserRoleService _userroleService;
        private readonly IFormMstService _formService;
        private readonly IFormRoleMappingService _formRoleService;
        private readonly IRoleService _roleService;
        public AccountController(ILoginService loginService, 
                                 IUserService userService, 
                                 IFormMstService formMstService , 
                                 IUserRoleService userroleService , 
                                 IFormRoleMappingService formRoleService,
                                 IRoleService roleService)
        {
            _loginService = loginService;
            _userService = userService;
            _formService = formMstService;
            _userroleService = userroleService;
            _formRoleService = formRoleService;
            _roleService = roleService;
        }

          [AllowAnonymous]
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
                    Session["Name"] = _userService.GetUserByEmail(model.Email).UserName;
                    Session["UserId"] = _userService.GetUserByEmail(model.Email).Id;
                    var userRoleId = _userroleService.getByUserId(user.Id).RoleId;
                    Session["RoleCode"] = (_roleService.GetRoleById(userRoleId).Code == "SADMIN");
                    Session["Permission"] = _formRoleService.Permission(userRoleId).ToList();
                    Session["UserRoleId"] = userRoleId;
                    Session["FormListForMenu"] = _formService.FormMstList(true);
                    Session["FormListForTab"] = _formService.FormMstList();
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
        [CRMSActionFilter("SYS", CheckRolePermission.FormAccessCode.IsView)]
        public ActionResult DashBoard()
        {
            ViewBag.DashBoard = TempData["FormName"] as string;
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}