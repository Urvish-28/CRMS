using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.UserRole
{
    public interface IUserRoleRepository
    {
        IQueryable<UserRoles> Collection();
        void Commit();
        void Insert(UserRoles userroles);
        void Update(UserRoles userroles);
        UserRoles Find(Guid Id);
        void Delete(Guid Id);
    }
    public class UserRoleRepository : IUserRoleRepository
    {
        public CRMSEntities context;
        internal DbSet<UserRoles> dbset;

        public UserRoleRepository(CRMSEntities Context)
        {
            this.context = Context;
            this.dbset = context.Set<UserRoles>();
        }

        public IQueryable<UserRoles> Collection()
        {
            return dbset;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Insert(UserRoles userroles)
        {
            dbset.Add(userroles);
        }

        public void Update(UserRoles userroles)
        {

            /*     dbset.Attach(userroles);
                 context.Entry(userroles).State = EntityState.Modified;*/
            UserRoles userUpdate = context.UserRole.Where(b => b.UserId == userroles.UserId).FirstOrDefault();
            userUpdate.RoleId = userroles.RoleId;
            Commit();
        }

        public UserRoles Find(Guid Id)
        {
            return dbset.Where(x => x.UserId == Id).FirstOrDefault();
        }

        public void Delete(Guid Id)
        {
            var userroles = Find(Id);
            if (context.Entry(userroles).State == EntityState.Detached)
                dbset.Attach(userroles);

            dbset.Remove(userroles);
        }
    }
}
