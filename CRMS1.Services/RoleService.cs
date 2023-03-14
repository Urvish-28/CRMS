using CRMS1.Core.Models;
using CRMS1.SQL;
using CRMS1.SQL.Repositories.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMS1.Services
{
    public interface IRoleService
    {
        IEnumerable<Roles> GetAllRoles();
        Roles GetRoleById(Guid id);
        void CreateRole(Roles model);
        void UpdateRole(Roles model);
        void DeleteRole(Guid id);        
    }
    public class RoleService : IRoleService 
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public void CreateRole(Roles model)
        {
            //Roles obj = new Roles();
            //obj.Id = model.Id;
            //obj.Name = model.Name;
            _roleRepository.Insert(model);
            _roleRepository.Commit();
        }
        public void UpdateRole(Roles model)
        {
            _roleRepository.Update(model);
            _roleRepository.Commit();
        }
        public void DeleteRole(Guid id)
        {
            Roles obj = _roleRepository.Find(id);
            obj.IsDelete = true;
            _roleRepository.Update(obj);
            _roleRepository.Commit();   
        }
        
        public IEnumerable<Roles> GetAllRoles()
        {
            return _roleRepository.Collection().Where(x=>x.IsDelete == false);
        }
        public Roles GetRoleById(Guid id)
        {
            return _roleRepository.Find(id);
        }        
    }
}
