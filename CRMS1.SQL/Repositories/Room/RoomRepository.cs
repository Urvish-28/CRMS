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
        IQueryable<Rooms> Collection();
        void Commit();
        void Insert(Rooms rooms);

    }
    public class RoomRepository
    {
        public CRMSEntities context;
        internal DbSet<Rooms> dbset;

        public RoomRepository(CRMSEntities Context)
        {
            this.context = Context;
            this.dbset = context.Set<Rooms>();
        }

        public IQueryable<Rooms> Collection()
        {
            return dbset;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Insert(Rooms rooms)
        {
            dbset.Add(rooms);
        }

        public void Update(Rooms rooms)
        {
            dbset.Attach(rooms);
            context.Entry(rooms).State = EntityState.Modified;
        }

        public Rooms Find(Guid Id)
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
