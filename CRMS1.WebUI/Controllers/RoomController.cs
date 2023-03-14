using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMS1.WebUI.Controllers
{
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
            List<Rooms> room = _roomservice.GetAllRoom().ToList();
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                _roomservice.CreateRoom(model);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(Guid id)
        {
            Rooms obj = _roomservice.GetRoomById(id);
            if(obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                RoomViewModel model = new RoomViewModel();
                model.RoomName = obj.RoomName;
                model.RoomNo = obj.RoomNo;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(RoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            else
            {
                _roomservice.UpdateRoom(model);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(Guid id)
        {
            Rooms userToDelete = _roomservice.GetRoomById(id);
            _roomservice.DeleteRoom(id);
            return RedirectToAction("Index");
        }
    }
}