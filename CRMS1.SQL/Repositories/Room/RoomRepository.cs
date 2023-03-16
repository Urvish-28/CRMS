using CRMS1.Core.Models;
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
        IQueryable<ConferenceRoom> Collection();
        void Commit();
        void Insert(ConferenceRoom rooms);
        void Update(ConferenceRoom rooms);
        ConferenceRoom Find(Guid Id);
        void Delete(Guid Id);
    }
    public class RoomRepository : IRoomRepository
    {
        public CRMSEntities context;
        internal DbSet<ConferenceRoom> dbset;

        public RoomRepository(CRMSEntities Context)
        {
            this.context = Context;
            this.dbset = context.Set<ConferenceRoom>();
        }

        public IQueryable<ConferenceRoom> Collection()
        {
            return dbset;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Insert(ConferenceRoom rooms)
        {
            dbset.Add(rooms);
        }

        public void Update(ConferenceRoom rooms)
        {
            dbset.Attach(rooms);
            context.Entry(rooms).State = EntityState.Modified;
        }

        public ConferenceRoom Find(Guid Id)
        {
            return dbset.Find(Id);
        }

        public void Delete(Guid Id)
        {
            var rooms = Find(Id);
            if (context.Entry(rooms).State == EntityState.Detached)
                dbset.Attach(rooms);

            dbset.Remove(rooms);
        }
    }
}
