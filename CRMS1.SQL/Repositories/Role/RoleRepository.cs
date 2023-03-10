using CRMS1.Core.Contracts;
using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.Role
{
    public interface IRoleRepository : IRepository<Roles>
    {

    }
    public class RoleRepository : SQLRepository<Roles>, IRoleRepository
    {
        public CRMSEntities context;
        internal DbSet dbset;
        public RoleRepository(CRMSEntities context)
        {
            this.context = context;
            this.dbset = context.Set<Roles>();
        }
    }
}
