using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.Role
{
    public interface IRoleRepository 
    {
        IQueryable<Roles> Collection();
        void Commit();
        void Insert(Roles roles);
        void Update(Roles roles);
        Roles Find(Guid Id);
        void Delete(Guid Id);
    }
    public class RoleRepository : IRoleRepository
    {
        public CRMSEntities context;
        internal DbSet<Roles> dbset;
        public RoleRepository(CRMSEntities Context)
        {
            this.context = Context;
            this.dbset = context.Set<Roles>();
        }
        public IQueryable<Roles> Collection()
        {
            return dbset;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Insert(Roles roles)
        {
            dbset.Add(roles);
        }

        public void Update(Roles roles)
        {
            dbset.Attach(roles);
            context.Entry(roles).State = EntityState.Modified;
        }

        public Roles Find(Guid Id)
        {
            return dbset.Find(Id);
        }

        public void Delete(Guid Id)
        {
            var roles = Find(Id);
            if (context.Entry(roles).State == EntityState.Detached)
                dbset.Attach(roles);

            dbset.Remove(roles);
        }
    }
}
