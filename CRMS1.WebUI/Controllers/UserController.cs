using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS1.WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _usersevice;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public UserController(IUserService usersevice,
                              IRoleService roleService,
                              IUserRoleService userRoleService)
        {
            _usersevice = usersevice;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }
        public UserController()
        {
        }
        // GET: User
        public ActionResult Index()
        {
            var list = _usersevice.GetUserList();
            return View(list);
        }
        public ActionResult Create()
        {
            UserViewModel user = new UserViewModel();
            user.RoleDropdown = _roleService.GetAllRoles()
                .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
            return View(user);
        }
        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            bool IsExist = _usersevice.IsAlreadyExist(model);
            if (!ModelState.IsValid)
            {
                model.RoleDropdown = _roleService.GetAllRoles()
                .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
                return View(model);
            }
            else
            {
                if (IsExist)
                {
                    model.RoleDropdown = _roleService.GetAllRoles()
                    .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
                    TempData["AlertMsg"] = "Employee Already Exists";
                    return View(model);
                }
                else
                {
                     model.CreatedBy = (Guid)Session["UserId"];
                    _usersevice.AddUser(model);
                    TempData["AlertMsg"] = "Employee added successfully...!";
                    return RedirectToAction("Index");
                }

            }
        }
        public ActionResult Edit(Guid id)
        {
            UserViewModel model = new UserViewModel();
            User obj = _usersevice.GetUserById(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                model = _usersevice.BindUserModel(obj);

                model.RoleDropdown = _roleService.GetAllRoles().Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
                model.RoleId = _userRoleService.getByUserId(id).RoleId;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            bool IsExist = _usersevice.IsAlreadyExist(model,false);
            model.RoleDropdown = _roleService.GetAllRoles().Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (IsExist)
                {
                    TempData["AlertMsg"] = "Employee Already Exists";
                    return View(model);
                }
                else
                {
                    model.UpdatedBy = (Guid)Session["UserId"];
                    _usersevice.UpdateUser(model);
                    TempData["AlertMsg"] = "Employee Details Edited successfully...!";
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Delete(Guid id)
        {
            User userToDelete = _usersevice.GetUserById(id);
            _usersevice.DeleteUser(id);
            TempData["AlertMsg"] = "Employee Deleted successfully...!";
            return RedirectToAction("Index");
        }
    }
}