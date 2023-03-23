using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
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
        void CreateRole(RoleViewModel model);
        void UpdateRole(RoleViewModel model);
        void DeleteRole(Guid id);
        Roles BindRoleModel(RoleViewModel model);
        RoleViewModel BindRoleModel(Roles model);
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public void CreateRole(RoleViewModel model)
        {
            Roles obj = new Roles();
            obj = BindRoleModel(model);
            _roleRepository.Insert(obj);
            _roleRepository.Commit();
        }
        public void UpdateRole(RoleViewModel model)
        {
            Roles obj = new Roles();
            obj = BindRoleModel(model);
            _roleRepository.Update(obj);
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
            return _roleRepository.Collection().Where(x => x.IsDelete == false);
        }
        public Roles GetRoleById(Guid id)
        {
            return _roleRepository.Find(id);
        }
        public Roles BindRoleModel(RoleViewModel model)
        {
            Roles obj = GetRoleById(model.Id);
            if(obj == null)
            {
                obj = new Roles();
            }
            obj.Id = model.Id;
            obj.Name = model.Name;
            obj.Code = model.Code;
            return obj;
        }
        public RoleViewModel BindRoleModel(Roles model)
        {
            RoleViewModel obj = new RoleViewModel();
            obj.Id = model.Id;
            obj.Name = model.Name;
            obj.Code = model.Code;
            return obj;
        }
    }
}
