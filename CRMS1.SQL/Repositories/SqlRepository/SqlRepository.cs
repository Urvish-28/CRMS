using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.SqlRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Commit();
        void Insert(T t);
        void Update(T t);
        void Delete(Guid Id);
        T Find(Guid Id);
        IQueryable<T> Collection();
        void InsertBulk(IEnumerable<T> t);
        void DeleteBulk(IEnumerable<T> t);
    }
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity 
    {
        internal CRMSEntities context;
        internal DbSet<T> dbset;

        public SqlRepository(CRMSEntities context)
        {
            this.context = context;
            this.dbset = context.Set<T>();
        }
        public void Commit()
        {
            context.SaveChanges();
        }
        public void Insert(T t)
        {
            dbset.Add(t);
            Commit();
        }
        public void InsertBulk(IEnumerable<T> t)
        {
            dbset.AddRange(t);
            Commit();
        }
        public void Update(T t)
        {
            dbset.Attach(t);
            context.Entry(t).State = EntityState.Modified;
            Commit();
        }
        public void Delete(Guid Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbset.Attach(t);
            dbset.Remove(t);
        }
        public T Find(Guid Id)
        {
            return dbset.Find(Id);
        }
        public IQueryable<T> Collection()
        {
            return dbset;
        }
        public void DeleteBulk(IEnumerable<T> t)
        {
            dbset.RemoveRange(t);
            Commit();
        }
    }
}
