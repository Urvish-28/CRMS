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
        IEnumerable<Roles> gelAllRoles();
        Roles getRoleById(Guid id);
        void createRole(Roles model);
        void updateRole(Roles model);
        void deleteRole(Guid id);        
    }
    public class RoleService : IRoleService 
    {
        //SQLRepository<Roles> rolescontext;
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
            //this.rolescontext = Rolescontext;
        }

        public void createRole(Roles model)
        {
            //Roles obj = new Roles();
            //obj.Id = model.Id;
            //obj.Name = model.Name;
            _roleRepository.Insert(model);
            _roleRepository.Commit();
        }
        public void updateRole(Roles model)
        {
            _roleRepository.Update(model);
            _roleRepository.Commit();
        }
        public void deleteRole(Guid id)
        {
            Roles obj = _roleRepository.Find(id);
            obj.IsDelete = true;
            _roleRepository.Update(obj);
            _roleRepository.Commit();
        }
        
        public IEnumerable<Roles> gelAllRoles()
        {
            return _roleRepository.Collection().Where(x=>x.IsDelete == false);
        }
        public Roles getRoleById(Guid id)
        {
            return _roleRepository.Find(id);
        }        
    }
}
