using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Services
{
    public interface IRoomService
    {
        void CreateRoom(RoomViewModel model);
        void UpdateRoom(RoomViewModel model);
        void DeleteRoom(Guid id);
        IEnumerable<ConferenceRoom> GetAllRoom();
        ConferenceRoom GetRoomById(Guid id);
        ConferenceRoom BindConferenceRoomModel(RoomViewModel model);
        RoomViewModel BindConferenceRoomModel(ConferenceRoom model);
        bool IsAlreadyExist(RoomViewModel model, bool IsCreated = false);
    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public void CreateRoom(RoomViewModel model)
        {
            ConferenceRoom obj = new ConferenceRoom();
            obj.Capacity = model.Capacity;
            obj.Name = model.RoomName;
            _roomRepository.Insert(obj);
            _roomRepository.Commit();
        }
        public void UpdateRoom(RoomViewModel model)
        {
            ConferenceRoom obj = GetRoomById(model.Id);
            obj.Capacity = model.Capacity;
            obj.Name = model.RoomName;
            _roomRepository.Update(obj);
            _roomRepository.Commit();
        }
        public void DeleteRoom(Guid id)
        {
            ConferenceRoom obj = _roomRepository.Find(id);
            obj.IsDelete = true;
            _roomRepository.Update(obj);
            _roomRepository.Commit();
        }
        public IEnumerable<ConferenceRoom> GetAllRoom()
        {
            return _roomRepository.Collection().Where(x => x.IsDelete == false);
        }
        public ConferenceRoom GetRoomById(Guid id)
        {
            return _roomRepository.Find(id);
        }
        public ConferenceRoom BindConferenceRoomModel(RoomViewModel model)
        {
            ConferenceRoom obj = GetRoomById(model.Id);
            if(obj == null)
            {
                obj = new ConferenceRoom();
            }
            obj.Capacity = model.Capacity;
            obj.Name = model.RoomName;
            return obj;
        }
        public RoomViewModel BindConferenceRoomModel(ConferenceRoom model)
        {
            RoomViewModel obj = new RoomViewModel();
            obj.Capacity = model.Capacity;
            obj.RoomName = model.Name;
            return obj;
        }

        public bool IsAlreadyExist(RoomViewModel model, bool IsCreated = false)
        {
            var records = GetAllRoom().Where(x => (x.Name.ToLower() == model.RoomName.ToLower()) && (IsCreated || x.Id != model.Id)).ToList();

            if (records.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
