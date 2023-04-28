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
        User FindByEmail(string Email);
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
        public User FindByEmail(string Email)
        {
            return dbSet.Where(x=>x.Email == Email).FirstOrDefault();
        }
        public IEnumerable<IndexViewModel> UserList()
        {
            var userlist = from u in context.Users.Where(x => x.IsDelete == false)
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
