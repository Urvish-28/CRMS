using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.AuditRepository;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace CRMS1.Services
{
    public interface IAuditLogService
    {
        void CreateActivity(string exception);
        IEnumerable<ActivityLogViewModel> ActivityLogList(string Id = "");
        IEnumerable<ErrorLogViewModel> ErrorLogList(string Id = "");
    }
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditRepository _auditRepository;
        public AuditLogService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }
        public static void StopWatch()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Thread.Sleep(5000);
            stopwatch.Stop();

            var ExecutionTime = stopwatch.ElapsedMilliseconds;
        }
        public void CreateActivity(string exception)
        {
            AuditLogs model = new AuditLogs();
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            RouteData routeData = urlHelper.RouteCollection.GetRouteData(currentContext);
            model.UserId = (Guid?)HttpContext.Current.Session["UserId"];
            model.HttpMethod = HttpContext.Current.Request.HttpMethod;
            model.Comments = routeData.Values["controller"].ToString() + " || Action = " + routeData.Values["action"].ToString();
            model.Parameters = routeData.Values["Id"].ToString();
            model.BrowserInfo = HttpContext.Current.Request.Browser.Browser + "" + HttpContext.Current.Request.Browser.Version;
            model.Url = HttpContext.Current.Request.Url.ToString();
            model.ClientIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            model.HttpStatusCode = HttpContext.Current.Response.StatusCode;
            model.ExecutionDuration = DateTime.Now.Millisecond;
            model.ExecutionTime = DateTime.UtcNow;
            model.Exception = exception;
            _auditRepository.Insert(model);
        }
        public IEnumerable<ActivityLogViewModel> ActivityLogList(string Id)
        {
            return _auditRepository.ActivityLogList(Id);
        }
        public IEnumerable<ErrorLogViewModel> ErrorLogList(string Id)
        {
            return _auditRepository.ErrorLogList(Id);
        }
    }
}
