using CRMS1.Services;
using System.Web.Mvc;

namespace CRMS1.WebUI.Filters
{
    public class ActivityLogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _auditLogService = DependencyResolver.Current.GetService<AuditLogService>();
            _auditLogService.CreateActivity(null);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var _auditLogService = DependencyResolver.Current.GetService<AuditLogService>();
            var dfg = filterContext.HttpContext.Items["Error"] == null ? "" : "Authorized";
            filterContext.ExceptionHandled = true;
            if (filterContext.Exception != null)
            {
                _auditLogService.CreateActivity(filterContext.Exception.Message);
            }
        }
    }
}