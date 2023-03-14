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
        IEnumerable<Rooms> GetAllRoom();
        Rooms GetRoomById(Guid id);
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
            Rooms obj = new Rooms();
            obj.RoomName = model.RoomName;
            obj.RoomNo = model.RoomNo;
            _roomRepository.Insert(obj);
            _roomRepository.Commit();
        }
        public void UpdateRoom(RoomViewModel model)
        {
            Rooms obj = GetRoomById(model.Id);
            obj.RoomName = model.RoomName;
            obj.RoomNo = model.RoomNo;
            _roomRepository.Update(obj);
            _roomRepository.Commit();
        }
        public void DeleteRoom(Guid id)
        {
            Rooms obj = _roomRepository.Find(id);
            obj.IsDelete = true;
            _roomRepository.Update(obj);
            _roomRepository.Commit();
        }
        public IEnumerable<Rooms> GetAllRoom()
        {
            return _roomRepository.Collection().Where(x => x.IsDelete == false);
        }
        public Rooms GetRoomById(Guid id)
        {
            return _roomRepository.Find(id);
        }
    }
}
