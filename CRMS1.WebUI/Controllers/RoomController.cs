using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using CRMS1.SQL;
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
    public class RoomController : Controller
    {
        private readonly IRoomService _roomservice;
        public RoomController(IRoomService roomservice)
        {
            _roomservice = roomservice;
        }
        // GET: Room
        public ActionResult Index()
        {
            List<ConferenceRoom> room = _roomservice.GetAllRoom().ToList();
            return View(room);
        }
        public ActionResult Create()
        {
            RoomViewModel rooms = new RoomViewModel();
            return View(rooms);
        }
        [HttpPost]
        public ActionResult Create(RoomViewModel model)
        {
            bool IsExist = _roomservice.IsAlreadyExist(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (IsExist == true)
                {
                    TempData["RoomAlert"] = "ConferenceRoom Already Exists";
                    return View(model);
                }
                else
                {
                    model.CreatedBy = (Guid)Session["UserId"];
                    _roomservice.CreateRoom(model);
                    TempData["RoomAlert"] = "Conference Room added successfully...!";
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Edit(Guid id)
        {
            ConferenceRoom obj = _roomservice.GetRoomById(id);
            if(obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                RoomViewModel model = new RoomViewModel();
                model = _roomservice.BindConferenceRoomModel(obj);
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(RoomViewModel model)
        {
            bool IsExist = _roomservice.IsAlreadyExist(model);
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            else
            {
                if (IsExist)
                {
                    TempData["RoomAlert"] = "ConferenceRoom Already Exists";
                    return View(model);
                }
                else
                {
                    model.UpdatedBy = (Guid)Session["UserId"];
                    _roomservice.UpdateRoom(model);
                    TempData["RoomAlert"] = "Conference Room Edited successfully...!";
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Delete(Guid id)
        {
            ConferenceRoom userToDelete = _roomservice.GetRoomById(id);
            _roomservice.DeleteRoom(id);
            return RedirectToAction("Index");
        }
        public ActionResult ConferenceRoomGrid([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ConferenceRoom> list = _roomservice.GetAllRoom().ToList();
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}