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
            var existingmodel = _commonLookupService.GetAll().Where(x => x.ConfigKey == model.ConfigKey && x.ConfigName == model.ConfigName).Count();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (existingmodel > 0 || model.ConfigKey == model.ConfigName)
                {
                    TempData["Alert"] = "Already Exists";
                }
                else
                {
                    _commonLookupService.AddCommonLookup(model);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
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
                CommonLookupsViewModel model = new CommonLookupsViewModel();
                model.Id = obj.Id;
                model.ConfigName = obj.ConfigName;
                model.ConfigKey = obj.ConfigKey;
                model.ConfigValue = obj.ConfigValue;
                model.DisplayOrder = obj.DisplayOrder;
                model.IsActive = obj.IsActive;
                return PartialView("_EditPartial", model);
            }
        }
        [HttpPost]
        public ActionResult Edit(CommonLookupsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (model.ConfigKey == model.ConfigName)
                {
                    TempData["Alert"] = "Already Exists";
                }
                else
                {
                    _commonLookupService.UpdateCommonLookup(model);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(Guid id)
        {
            CommonLookups commonLookupsdelete = _commonLookupService.GetById(id);
            _commonLookupService.DeleteCommonLookup(id);
            return RedirectToAction("Index");
        }
    }
}