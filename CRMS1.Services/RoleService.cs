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
        bool IsAlreadyExist(RoleViewModel model, bool IsCreated = false);
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
            _roleRepository.AddRole(obj);
        }
        public void UpdateRole(RoleViewModel model)
        {
            Roles obj = new Roles();
            obj = BindRoleModel(model);
            _roleRepository.UpdateRole(obj);
        }
        public void DeleteRole(Guid id)
        {
            _roleRepository.DeleteRole(id);
        }

        public IEnumerable<Roles> GetAllRoles()
        {
            return _roleRepository.GetRoleList().Where(x=>x.Code != "SADMIN");
        }
        public Roles GetRoleById(Guid id)
        {
            return _roleRepository.GetById(id);
        }
        public Roles BindRoleModel(RoleViewModel model)
        {
            Roles obj = GetRoleById(model.Id);
            if(obj == null)
            {
                obj = new Roles();
                obj.CreatedOn = DateTime.Now;
                obj.CreatedBy = model.CreatedBy;
            }
            else
            {
                obj.UpdatedOn = DateTime.Now;
                obj.UpdatedBy = model.UpdatedBy;
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
        public bool IsAlreadyExist(RoleViewModel model, bool IsCreated = false)
        {
            var records = GetAllRoles().Where(x => (x.Name.ToLower() == model.Name.ToLower()) && (IsCreated || x.Id != model.Id)).ToList();

            if (records.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
