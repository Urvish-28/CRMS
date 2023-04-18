using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRMS1.WebUI.Filters
{
    public class CRMSActionFilter : ActionFilterAttribute
    {
        public string FormAccessCode;
        public readonly CheckRolePermission.FormAccessCode _Action;

        public CRMSActionFilter(string _FormAccessCode, CheckRolePermission.FormAccessCode Action)
        {
            FormAccessCode = _FormAccessCode;
            _Action = Action;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool checkPermission = AccessPermission.CheckPermission(FormAccessCode, _Action.ToString());
            if (!checkPermission)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller" , "Account" },
                    {"action" , "AccessDenied"}
                });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}