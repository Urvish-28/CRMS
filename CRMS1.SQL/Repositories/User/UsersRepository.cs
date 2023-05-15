using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;

namespace CRMS1.SQL.Repositories.Users
{
    public interface IUsersRepository : IRepository<User>
    {
        User FindByEmail(string Email);
        IEnumerable<IndexViewModel> UserList();
    }
    public class UsersRepository :SqlRepository<User> ,IUsersRepository
    {
        public CRMSEntities _context;
        internal DbSet<User> _dbSet;

        public UsersRepository(CRMSEntities context) : base(context)
        {
            _context = this.context;
            _dbSet = this.dbset;
        }
        public User FindByEmail(string Email)
        {
            return _dbSet.Where(x=>x.Email == Email).FirstOrDefault();
        }
        public IEnumerable<IndexViewModel> UserList()
        {
            var userlist = from u in context.Users.Where(x => x.IsDelete == false)
                           join x in context.UserRole on u.Id equals x.UserId
                           join r in context.Roles.Where(x=>x.Code != "SADMIN") on x.RoleId equals r.Id
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
