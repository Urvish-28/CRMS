using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using CRMS1.SQL.Repositories.CommonLookUps;
using CRMS1.WebUI.Filters;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS1.WebUI.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketAttachmentService _ticketAttachmentService;
        private readonly IUserService _userService;
        private readonly ICommonLookupService _commonLookUpsService;
        public TicketController(ITicketService ticketService,
                                IUserService userService,
                                ICommonLookupService commonLookUpsService,
                                 ITicketAttachmentService ticketAttachmentService)
        {
            _ticketService = ticketService;
            _userService = userService;
            _commonLookUpsService = commonLookUpsService;
            _ticketAttachmentService = ticketAttachmentService;
        }
        // GET: Ticket
        [CRMSActionFilter("TICKET", CheckRolePermission.FormAccessCode.IsView)]
        public ActionResult Index()
        {
            List<TicketIndexViewModel> list = _ticketService.TicketListForIndex().ToList();
            return View(list);
        }
        public ActionResult TicketGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<TicketIndexViewModel> list = _ticketService.TicketListForIndex().ToList();
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [CRMSActionFilter("TICKET", CheckRolePermission.FormAccessCode.IsInsert)]
        public ActionResult Create()
        {
            TicketViewModel model = new TicketViewModel();
            model.AssignToDropDown = _userService.GetAllUsers().Select(x => new DropDown() {Id = x.Id , Name = x.Name});
            model.TypeDropDown = _commonLookUpsService.DropDownList("TicketType").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            model.PriorityDropDown = _commonLookUpsService.DropDownList("Priority").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            model.StatusDropDown = _commonLookUpsService.DropDownList("Status").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TicketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AssignToDropDown = _userService.GetAllUsers().Select(x => new DropDown() { Id = x.Id, Name = x.Name });
                model.TypeDropDown = _commonLookUpsService.DropDownList("TicketType").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
                model.PriorityDropDown = _commonLookUpsService.DropDownList("Priority").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
                model.StatusDropDown = _commonLookUpsService.DropDownList("Status").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
                return View(model);
            }
            else
            {
                _ticketService.CreateTicket(model);
                TempData["Ticket"] = "Ticket Added Successfully...!";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(Guid Id)
        {
            Ticket obj = _ticketService.GetById(Id);
            if(obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = _ticketService.BindTicket(obj);
                model.AssignToDropDown = _userService.GetAllUsers().Select(x => new DropDown() { Id = x.Id, Name = x.Name });
                model.TypeDropDown = _commonLookUpsService.DropDownList("TicketType").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
                model.PriorityDropDown = _commonLookUpsService.DropDownList("Priority").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
                model.StatusDropDown = _commonLookUpsService.DropDownList("Status").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
                model.Attachments = _ticketAttachmentService.AttachmentList(Id);
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(TicketViewModel model , HttpPostedFileBase file)
        {
            model.AssignToDropDown = _userService.GetAllUsers().Select(x => new DropDown() { Id = x.Id, Name = x.Name });
            model.TypeDropDown = _commonLookUpsService.DropDownList("TicketType").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            model.PriorityDropDown = _commonLookUpsService.DropDownList("Priority").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            model.StatusDropDown = _commonLookUpsService.DropDownList("Status").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue });
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if(model.AttachmentListFromView != null)
                {
                    List<string> listOfTicket = JsonConvert.DeserializeObject<List<string>>(model.AttachmentListFromView);
                    foreach(var item in listOfTicket)
                    {
                        Guid Id = Guid.Parse(item);
                        _ticketAttachmentService.DeleteAttachment(Id);
                    }
                }
                model.Image = file;
                _ticketService.UpdateTicket(model);
                TempData["Ticket"] = "Ticket Edit Successfully...!";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(Guid Id)
        {
            _ticketService.DeleteTicket(Id);
            return RedirectToAction("Index");
        }
        public FileResult DownloadImage(Guid Id)
        {
            var getImage = _ticketAttachmentService.GetImageName(Id);
            string contentType = string.Empty;
            if (getImage != null)
            {
                contentType = "application/force-download";
                string fullPath = Path.Combine(Server.MapPath("~/Content/TicketImage/") + getImage);
                return File(fullPath, contentType, getImage);
            }
            return null;
        }
        public ActionResult AttachmentList(Guid TicketId)
        {
            IEnumerable<TicketAttachment> list = _ticketAttachmentService.AttachmentList(TicketId);
            return PartialView("_AttachmentList", list);
        }
        public JsonResult StatusFilter()
        {
            var statusList = _commonLookUpsService.DropDownList("Status").Select(x => new DropDown() { Id = x.Id, Name = x.ConfigValue }).ToList();
            return Json(statusList, JsonRequestBehavior.AllowGet);
        }
    }
}