using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using CRMS1.SQL;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS1.WebUI.Controllers
{
    [Authorize]
    public class RoleManagementController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleManagementController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public RoleManagementController()
        {

        }

        // GET: RoleManagement
        public ActionResult Index()
        {
            List<Roles> roles = _roleService.GetAllRoles().ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            RoleViewModel roles = new RoleViewModel();
            return View(roles);
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            bool IsExist = _roleService.IsAlreadyExist(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (IsExist)
                {
                    TempData["AlertMsg"] = "Role Already Exists";
                    return View(model);
                }
                else
                {
                    model.CreatedBy = (Guid)Session["UserId"];
                    _roleService.CreateRole(model);
                    TempData["AlertMsg"] = "Role is added successfully...!";
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Edit(Guid Id)
        {

            Roles obj = _roleService.GetRoleById(Id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = _roleService.BindRoleModel(obj);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            bool IsExist = _roleService.IsAlreadyExist(model, false);
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
                    _roleService.UpdateRole(model);
                    TempData["AlertMsg"] = "Role Edited successfully...!";
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(Guid id)
        {
            Roles rolesToDelete = _roleService.GetRoleById(id);
            _roleService.DeleteRole(id);
            return RedirectToAction("Index");
        }
        public ActionResult RoleGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Roles> list = _roleService.GetAllRoles().ToList();
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}