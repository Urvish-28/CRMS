using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.Room
{
    public interface IRoomRepository
    {
        IEnumerable<ConferenceRoom> GetAllConferenceRoom();
        ConferenceRoom GetById(Guid id);
        void AddConferenceRoom(ConferenceRoom obj);
        void UpdateConferenceRoom(ConferenceRoom obj);
        void DeleteConferenceRoom(Guid id);
    }
    public class RoomRepository : IRoomRepository
    {
        private readonly IRepository<ConferenceRoom> _Irepository;

        public RoomRepository(IRepository<ConferenceRoom> Irepository)
        {
            _Irepository = Irepository;
        }
        public IEnumerable<ConferenceRoom> GetAllConferenceRoom()
        {
            return _Irepository.Collection().Where(x => x.IsDelete == false);
        }
        public ConferenceRoom GetById(Guid id)
        {
            return _Irepository.Find(id);
        }
        public void AddConferenceRoom(ConferenceRoom obj)
        { 
            _Irepository.Insert(obj);
            _Irepository.Commit();
        }
        public void UpdateConferenceRoom(ConferenceRoom obj)
        {
            obj = GetById(obj.Id);
            _Irepository.Update(obj);
            _Irepository.Commit();
        }
        public void DeleteConferenceRoom(Guid id)
        {
            ConferenceRoom obj = GetById(id);
            obj.IsDelete = true;
            _Irepository.Update(obj);
            _Irepository.Commit();
        }
    }
}
