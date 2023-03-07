using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS1.WebUI.Controllers
{
    public class RoleManagementController : Controller
    {
        // GET: RoleManagement
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int av)
        {
            return View();
        }

    }
}