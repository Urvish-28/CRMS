using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

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
    public class RoleService :Page, IRoleService
    {
        private readonly IRepository<Roles> _roleRepository;
        private readonly IFormRoleMappingService _formRoleMappingService;
        public RoleService(IRepository<Roles> roleRepository, IFormRoleMappingService formRoleMappingService)
        {
            _formRoleMappingService = formRoleMappingService;
            _roleRepository = roleRepository;
        }
        public void CreateRole(RoleViewModel model)
        {
            Roles obj = new Roles();
            obj = BindRoleModel(model);
            _roleRepository.Insert(obj);
        }
        public void UpdateRole(RoleViewModel model)
        {
            Roles obj = new Roles();
            obj = BindRoleModel(model);
            _roleRepository.Update(obj);
        }
        public void DeleteRole(Guid id)
        {
            _roleRepository.SoftDelete(id);

            var formRoleList = _formRoleMappingService.GetAll().Where(x => x.RoleId == id);
            _formRoleMappingService.DeleteBulk(formRoleList);
        }
        public IEnumerable<Roles> GetAllRoles()
        {
            return _roleRepository.Collection().Where(x=>x.IsDelete == false && x.Code != "SADMIN");
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
                obj.CreatedOn = DateTime.Now;
                obj.CreatedBy = (Guid)Session["UserId"];
            }
            else
            {
                obj.UpdatedOn = DateTime.Now;
                obj.UpdatedBy = (Guid)Session["UserId"];
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
