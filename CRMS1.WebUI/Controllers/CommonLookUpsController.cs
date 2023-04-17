using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
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
    public class CommonLookUpsController : Controller
    {
        private readonly ICommonLookupService _commonLookupService;

        public CommonLookUpsController(ICommonLookupService commonLookupService)
        {
            _commonLookupService = commonLookupService;
        }
        // GET: CommonLookUps
        public ActionResult Index()
        {
            List<CommonLookupsViewModel> commonLookups = _commonLookupService.GetAll();
            return PartialView("_IndexPartial",commonLookups);
        }
        public ActionResult Create()
        {
            CommonLookupsViewModel commonLookups = new CommonLookupsViewModel();
            return PartialView("_CreatePartial", commonLookups);
        }
        [HttpPost]
        public ActionResult Create(CommonLookupsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content("false");
            }
            else
            {
                bool isExist = _commonLookupService.IsAlreadyExist(model, false);
                if (isExist)
                {
                    return Content("Exists"); 
                }
                else
                {
                    _commonLookupService.AddCommonLookup(model);
                    TempData["CMAlert"] = "Create Successfully!!";
                    return Content("true");
                }
            }
        }
        public ActionResult Edit(Guid id)
        {
            CommonLookups obj = _commonLookupService.GetById(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = _commonLookupService.BindCommonLookupModel(obj);
                return PartialView("_EditPartial", model);
            }
        }
        [HttpPost]
        public ActionResult Edit(CommonLookupsViewModel model)
            {
            var existingmodel = _commonLookupService.IsAlreadyExist(model);
            if (!ModelState.IsValid)
            {
                return Content("false");
            }
            else
            {
                if (existingmodel)
                {
                    return Content("Exists");
                }
                else
                {
                    _commonLookupService.UpdateCommonLookup(model);
                    TempData["CMAlert"] = "Edit Successfully!!";
                    return Content("true");
                }
            }
        }
        public ActionResult Delete(Guid id)
        {
            CommonLookups commonLookupsdelete = _commonLookupService.GetById(id);
            _commonLookupService.DeleteCommonLookup(id);
            TempData["Alert"] = "Delete Successfully!!";
            return new RedirectResult(Url.Action("Dashboard", "Account", new { selectedTabId = 2 }));
        }
        public ActionResult CommonLookupGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<CommonLookupsViewModel> list = _commonLookupService.GetAll();
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}