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
    public class FormRoleMappingController : Controller
    {
        private readonly IFormRoleMappingService _formRoleMappingService;
        public FormRoleMappingController(IFormRoleMappingService formRoleMappingService)
        {
            _formRoleMappingService = formRoleMappingService;
        }
        // GET: FormRoleMapping
        public ActionResult Index(Guid id)
        {
            IEnumerable<FormRoleMappingVM> list = _formRoleMappingService.GetAllForm(id);
            return View(list);
        }
        [HttpPost]
        public ActionResult AddPermission(IEnumerable<FormRoleMapping> records)
        {
            if (!ModelState.IsValid)
            {
                return View(records);
            }
            _formRoleMappingService.AddFormRole(records);
            TempData["RoleAlert"] = "Permission Update Successful...!";
            return new RedirectResult(Url.Action("Dashboard", "Account", new { selectedTabId = 1 }));
        }
    }
}