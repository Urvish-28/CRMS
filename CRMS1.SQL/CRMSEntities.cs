using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL
{
    public class CRMSEntities : DbContext
    {
        public CRMSEntities() : base("DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRole { get; set; }
        public DbSet<ConferenceRoom> ConferenceRoom { get; set; }
    }
}
