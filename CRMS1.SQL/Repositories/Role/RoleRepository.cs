using CRMS1.Core.Models;
using CRMS1.SQL.Repositories.SqlRepository;
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
        IEnumerable<Roles> GetRoleList();
        Roles GetById(Guid id);
        void AddRole(Roles obj);
        void UpdateRole(Roles obj);
        void DeleteRole(Guid id);
    }
    public class RoleRepository : IRoleRepository
    {
        private readonly IRepository<Roles> _Irepository;
        public RoleRepository(IRepository<Roles> Irepository)
        {
            _Irepository = Irepository;
        }
        public IEnumerable<Roles> GetRoleList()
        {
            return _Irepository.Collection().Where(x => x.IsDelete == false /*&& x.Code != "SADMIN"*/);
        }
        public Roles GetById(Guid id)
        {
            return _Irepository.Find(id);
        }
        public void AddRole(Roles obj)
        {
            _Irepository.Insert(obj);
            _Irepository.Commit();
        }
        public void UpdateRole(Roles obj)
        {
            _Irepository.Update(obj);
            _Irepository.Commit();
        }
        public void DeleteRole(Guid id)
        {
            Roles obj = GetById(id);
            obj.IsDelete = true;
            _Irepository.Update(obj);
            _Irepository.Commit();
        }


















    }
}
