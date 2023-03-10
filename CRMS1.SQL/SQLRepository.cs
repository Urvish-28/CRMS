using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL
{
    public class SQLRepository<T> where T :BaseEntity
    {
        public CRMSEntities context;
        internal DbSet<T> dbset;
        public SQLRepository()
        {
            context = new CRMSEntities();
            this.dbset = context.Set<T>();
        }

        public IQueryable<User> LoginRepository()
        {
            var user = context.Users.Where(a => a.Role == "SuperAdmin");
            return user;
        }

        public IQueryable<T> Collection()
        {
            return dbset;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Insert(T t)
        {
            dbset.Add(t);
        }

        public void Update(T t)
        {
            dbset.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }

        public T Find(Guid Id)
        {
            return dbset.Find(Id);
        }

        public void Delete(Guid Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbset.Attach(t);

            dbset.Remove(t);
        }
        public virtual T GetById(Guid id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }
    }
}
