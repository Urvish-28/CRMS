using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using CRMS1.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS1.WebUI.Controllers
{
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                //context.Insert(roles);
                //context.Commit();
                Roles obj = new Roles();        // mapping of View model to data model
                obj.Name = model.Name;
                obj.Code = model.Code;
                obj.CreatedOn = DateTime.Now;
                //obj.CreatedBy = SessionHelper.UserId;
                _roleService.CreateRole(obj);
                TempData["AlertMsg"] = "Role added successfully...!";
                return RedirectToAction("Index");
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
                RoleViewModel model = new RoleViewModel();
                model.Id = obj.Id;
                model.Name = obj.Name;
                model.Code = obj.Code;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            Roles roleToEdit = _roleService.GetRoleById(model.Id);
            if (roleToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                roleToEdit.Name = model.Name;
                roleToEdit.Code = model.Code;
                roleToEdit.UpdatedOn = DateTime.Now;
                _roleService.UpdateRole(roleToEdit);
                TempData["AlertMsg"] = "Role Edited successfully...!";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Guid id)
        {
            Roles rolesToDelete = _roleService.GetRoleById(id);
            _roleService.DeleteRole(id);
            /*     if (rolesToDelete == null)
                 {
                     return HttpNotFound();
                 }
                 else
                 {
                     return View(rolesToDelete);
                 }      */
            return RedirectToAction("Index");
        }
    }
}