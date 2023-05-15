using CRMS1.WebUI.Filters;
using System.Web;
using System.Web.Mvc;

namespace CRMS1.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ActivityLogFilter());
        }
    }
}
