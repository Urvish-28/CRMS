using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRMS1.Services
{
    public interface IFormRoleMappingService
    {
        IEnumerable<FormRoleMapping> GetAll();
        FormRoleMapping GetById(Guid id);
        FormRoleMapping BindFormRole(FormRoleMappingVM model);
        FormRoleMappingVM BindFormRole(FormRoleMapping model);
        void AddFormRole(FormRoleMappingVM model);
        void UpdateFormRole(FormRoleMappingVM model);
        void DeleteFormRole(Guid id);
    }
    public class FormRoleMappingService :Page, IFormRoleMappingService
    {
        private readonly IRepository<FormRoleMapping> _repository;
        public FormRoleMappingService(IRepository<FormRoleMapping> repository)
        {
            _repository = repository;
        }
        public IEnumerable<FormRoleMapping> GetAll()
        {
            return _repository.Collection().ToList();
        }
        public FormRoleMapping GetById(Guid id)
        {
            return _repository.Find(id);
        }
        public FormRoleMapping BindFormRole(FormRoleMappingVM model)
        {
            FormRoleMapping obj = GetById(model.Id);
            if(obj == null)
            {
                obj = new FormRoleMapping();
                obj.CreatedBy = (Guid)Session["UserId"];
            }
            else
            {
                obj.UpdatedOn = DateTime.Now;
                obj.UpdatedBy = (Guid)Session["UserId"];
            }
            obj.Id = model.Id;
            obj.AllowDelete = model.AllowDelete;
            obj.AllowInsert = model.AllowInsert;
            obj.AllowUpdate = model.AllowUpdate;
            obj.AllowView = model.AllowView;
            obj.FormId = model.FormId;
            obj.RoleId = model.RoleId;
            return obj;
        }
        public FormRoleMappingVM BindFormRole(FormRoleMapping model)
        {
            FormRoleMappingVM obj = new FormRoleMappingVM();
            obj.Id = model.Id;
            obj.AllowDelete = model.AllowDelete;
            obj.AllowInsert = model.AllowInsert;
            obj.AllowUpdate = model.AllowUpdate;
            obj.AllowView = model.AllowView;
            obj.FormId = model.FormId;
            obj.RoleId = model.RoleId;
            obj.CreatedOn = DateTime.Now;
            obj.UpdatedOn = DateTime.Now;
            return obj;
        }

        public void AddFormRole(FormRoleMappingVM model)
        {
            FormRoleMapping obj = new FormRoleMapping();
            obj = BindFormRole(model);
            _repository.Insert(obj);
            _repository.Commit();
        }

        public void UpdateFormRole(FormRoleMappingVM model)
        {
            FormRoleMapping obj = GetById(model.Id);
            obj = BindFormRole(model);
            _repository.Update(obj);
            _repository.Commit();
        }

        public void DeleteFormRole(Guid id)
        {
            FormRoleMapping obj = GetById(id);
            _repository.Delete(id);
            _repository.Commit();
        }

    }
}
