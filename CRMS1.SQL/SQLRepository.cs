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
        public DataContext context;
        internal DbSet<T> dbset;
        public SQLRepository()
        {
            context = new DataContext();
        }

        public IQueryable<User> LoginRepository()
        {
            var user = context.Users.Where(a => a.Role == "SuperAdmin");
            return user;
        }
    }
}
