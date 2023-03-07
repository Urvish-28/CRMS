using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL
{
    public class DataContext : DbContext
    {
        public DataContext()
    : base("DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Role { get; set; }
        public DbSet<UserRoles> User_Role { get; set; }
    }
}
