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
            List<Roles> Roles = _roleService.gelAllRoles().ToList();
            return View(Roles);
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
                obj.CreatedBy = DateTime.Now.ToString();
                //obj.CreatedBy = SessionHelper.UserId;
                _roleService.createRole(obj);

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(Guid Id)
        {
            Roles obj = _roleService.getRoleById(Id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                RoleViewModel model = new RoleViewModel();
                model.Id = obj.Id;
                model.Name = obj.Name;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            Roles roleToEdit = _roleService.getRoleById(model.Id);

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
                roleToEdit.UpdatedOn = DateTime.Now;
                _roleService.updateRole(roleToEdit);
                
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Guid id)
        {
            _roleService.deleteRole(id);

            /*            if (rolesToDelete == null)
                        {
                            return HttpNotFound();
                        }
                        else
                        {
                            return View(rolesToDelete);
                        }
            */
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid Id)
        {
            Roles roleToDelete = _roleService.getRoleById(Id);

            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            else
            {
                _roleService.deleteRole(Id);
                return RedirectToAction("Index");
            }
        }
    }
}