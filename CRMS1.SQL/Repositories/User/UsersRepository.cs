using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMS1.Core.Models;

namespace CRMS1.SQL.Repositories.Users
{
    public interface IUsersRepository
    {
        IQueryable<User> Collection();
        void Commit();
        void Insert(User user);
        void Update(User user);
        User Find(Guid Id);
        void Delete(Guid Id);

    }
    public class UsersRepository : IUsersRepository
    {
        public CRMSEntities context;
        internal DbSet<User> dbSet;

        public UsersRepository(CRMSEntities Context)
        {
            this.context = Context;
            this.dbSet = context.Set<User>();
        }

        public IQueryable<User> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Insert(User user)
        {
            dbSet.Add(user);
        }

        public void Update(User user)
        {
            dbSet.Attach(user);
            context.Entry(user).State = EntityState.Modified;
        }

        public User Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Delete(Guid Id)
        {
            var user = Find(Id);
            if (context.Entry(user).State == EntityState.Detached)
                dbSet.Attach(user);

            dbSet.Remove(user);
        }
    }
}
