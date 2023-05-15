using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using CRMS1.WebUI.Filters;
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
    public class AuditLogController : Controller
    {
        private readonly IAuditLogService _auditLogService;
        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }
        // GET: AuditLog
        [CRMSActionFilter("ACTIVITY", CheckRolePermission.FormAccessCode.IsView)]
        public ActionResult ActivityLogIndex()
        {
            var list = _auditLogService.ActivityLogList();
            return PartialView("_ActivityLogIndex", list);
        }
        public ActionResult ActivityLogGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ActivityLogViewModel> list = _auditLogService.ActivityLogList().ToList();
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [CRMSActionFilter("ERROR", CheckRolePermission.FormAccessCode.IsView)]
        public ActionResult ErrorLogIndex()
        {
            var list = _auditLogService.ErrorLogList();
            return PartialView("_ErrorLogIndex", list);
        }
        public ActionResult ErrorLogGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ErrorLogViewModel> list = _auditLogService.ErrorLogList().ToList();
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ActivityLogDetails(Guid Id)
        {
            ActivityLogViewModel model = _auditLogService.ActivityLogList(Id.ToString()).FirstOrDefault();
            return View(model);
        }
        public ActionResult ErrorLogDetails(Guid Id)
        {
            ErrorLogViewModel model = _auditLogService.ErrorLogList(Id.ToString()).FirstOrDefault();
            return View(model);
        }
    }
}