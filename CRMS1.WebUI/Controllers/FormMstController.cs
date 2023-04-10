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
    public class FormMstController : Controller
    {
        private readonly IFormMstService _formService;
        public FormMstController(IFormMstService formService)
        {
            _formService = formService;
        }
        // GET: FormMst
        public ActionResult Index()
        {
            var list = _formService.FormMstList();
            return View(list);
        }
        public ActionResult Create()
        {
            FormMstViewModel model = new FormMstViewModel();
            model.ParentFormDropDown = _formService.GetAllFormMst().Where(x=>x.ParentFormId == null)
                                       .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(FormMstViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ParentFormDropDown = _formService.GetAllFormMst().Where(x => x.ParentFormId == null)
                           .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
                return View(model);
            }
            else
            {
                bool isExist = _formService.IsAlreadyExist(model, false);
                if (isExist)
                {
                    model.ParentFormDropDown = _formService.GetAllFormMst().Where(x => x.ParentFormId == null)
                           .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
                    TempData["formMst"] = "Data Already Exists!!";
                    return View(model);
                }
                else
                {
                    _formService.AddFormMst(model);
                    TempData["formMst"] = "Added Successfully...!";
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Edit(Guid id)
        {
            FormMst obj = _formService.GetById(id);
            if(obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = _formService.BindFormMst(obj);
                model.ParentFormDropDown = _formService.GetAllFormMst().Where(x => x.ParentFormId == null && x.Id != model.Id)
                           .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(FormMstViewModel model)
        {
            model.ParentFormDropDown = _formService.GetAllFormMst().Where(x => x.ParentFormId == null && x.Id != model.Id)
                           .Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                bool isExist = _formService.IsAlreadyExist(model);
                if (isExist)
                {
                    TempData["formMst"] = "Data Already Exists!!";
                    return View(model);
                }
                else
                {
                    _formService.UpdateFormMst(model);
                    TempData["formMst"] = "Added Successfully...!";
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Delete(Guid id)
        {
            FormMst obj = _formService.GetById(id);
            _formService.DeleteFormMst(id);
            return RedirectToAction("Index");
        }
        public ActionResult FormMstGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<FormMstViewModel> list = _formService.FormMstList().ToList();
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}