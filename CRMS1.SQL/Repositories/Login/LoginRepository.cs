using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.Login
{
    public interface ILoginRepository
    {
        IQueryable<User> Collection();
    }
    public class LoginRepository : ILoginRepository
    {
        public CRMSEntities context;
        internal DbSet<User> dbSet;

        public LoginRepository()
        {
            context = new CRMSEntities();
            this.dbSet = context.Set<User>();
        }

        public IQueryable<User> Collection()
        {
            return dbSet;
        }
    }
}
