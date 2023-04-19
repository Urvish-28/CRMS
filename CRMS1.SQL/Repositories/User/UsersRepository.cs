using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;

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
        IEnumerable<IndexViewModel> UserList();

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
        public IEnumerable<IndexViewModel> UserList()
        {
            var userlist = from u in context.Users
                           join x in context.UserRole on u.Id equals x.UserId
                           join r in context.Roles on x.RoleId equals r.Id
                           select new IndexViewModel()
                           {
                               Id = u.Id,
                               Name = u.Name,
                               Email = u.Email,
                               UserName = u.UserName,
                               Gender = u.Gender,
                               MobileNo = u.MobileNo,
                               Role = r.Name
                           };
            return userlist;
        }
    }
}
