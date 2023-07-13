using CRMS1.Core.ViewModels;
using CRMS1.Services;
using CRMS1.SQL.Repositories.Tickets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS1.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITicketService _ticketService;

        public HomeController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public ActionResult Index()
        {
            HomeViewModel model = _ticketService.GetStatusCountForChart();
            var statusCount = model.ChartData.ToArray();
            model = _ticketService.GetPriorityCount();
            var priorityCount = model.BarChartData.ToArray();
            model.DataArray = statusCount;
            model.BarArray = priorityCount;
            return View(model);
        }
    }
}