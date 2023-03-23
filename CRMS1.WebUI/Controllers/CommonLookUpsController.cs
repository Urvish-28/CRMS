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
            List<CommonLookups> commonLookups = _commonLookupService.GetAll().ToList();
            return View(commonLookups);
        }
        public ActionResult Create()
        {
            CommonLookupsViewModel commonLookups = new CommonLookupsViewModel();
            return PartialView("_CreatePartial", commonLookups);
        }
        [HttpPost]
        public ActionResult Create(CommonLookupsViewModel model)
        {
            bool isExist = _commonLookupService.IsAlreadyExist(model, false);
            if (!ModelState.IsValid)
            {
                return Content("false");
            }
            else
            {
                if (isExist)
                {
                    return Content("Exists");
                }
                else
                {
                    _commonLookupService.AddCommonLookup(model);
                    TempData["Alert"] = "Create Successfully!!";
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
                    TempData["Alert"] = "Edit Successfully!!";
                    return Content("true");
                }
            }
        }
        public ActionResult Delete(Guid id)
        {
            CommonLookups commonLookupsdelete = _commonLookupService.GetById(id);
            _commonLookupService.DeleteCommonLookup(id);
            TempData["Alert"] = "Delete Successfully!!";
            return RedirectToAction("Index");
        }
    }
}