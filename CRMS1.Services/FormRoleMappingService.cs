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
        void AddFormRole(IEnumerable<FormRoleMapping> model);
        void DeleteFormRole(Guid id);
        IEnumerable<FormRoleMappingVM> GetAllForm(Guid id);
        List<FormRoleMapping> Permission(Guid RoleId);
    }
    public class FormRoleMappingService : Page, IFormRoleMappingService
    {
        private readonly IRepository<FormRoleMapping> _repository;
        private readonly IFormMstService _formMstService;
        public FormRoleMappingService(IRepository<FormRoleMapping> repository, IFormMstService formMstService)
        {
            _repository = repository;
            _formMstService = formMstService;
        }
        public IEnumerable<FormRoleMapping> GetAll()
        {
            return _repository.Collection().ToList();
        }
        public FormRoleMapping GetById(Guid id)
        {
            return _repository.Find(id);
        }
        public void AddFormRole(IEnumerable<FormRoleMapping> records)
        {
            Guid Id = (Guid)records.FirstOrDefault().RoleId;
            var recordsByRoleId = GetAll().Where(x => x.Id == Id).Select(x => x.RoleId);
            if (recordsByRoleId == null)
            {
                _repository.InsertBulk(records);
                _repository.Commit();
            }
            else
            {
                var DeleteList = GetAll().Where(x => x.RoleId == Id);
                _repository.DeleteBulk(DeleteList);
                _repository.InsertBulk(records);
                _repository.Commit();
            }
        }

        public void DeleteFormRole(Guid id)
        {
            FormRoleMapping obj = GetById(id);
            _repository.Delete(id);
            _repository.Commit();
        }

        public IEnumerable<FormRoleMappingVM> GetAllForm(Guid Id)
        {
            var db_forms = _formMstService.GetAllFormMst();
            var db_formrolemappings = GetAll().Where(x=>x.RoleId == Id);
            var formRoleMappingList = (from f in db_forms
                                       join fr in db_formrolemappings
                                       on f.Id equals fr.FormId into formrole
                                       from frm in formrole.DefaultIfEmpty()
                                       select new FormRoleMappingVM()
                                       {
                                           FormName = f.Name,
                                           FormId = f.Id,
                                           RoleId = Id,
                                           AllowView = frm == null ? false : frm.AllowView,
                                           AllowInsert = frm == null ? false : frm.AllowInsert,
                                           AllowUpdate = frm == null ? false : frm.AllowUpdate,
                                           AllowDelete = frm == null ? false : frm.AllowDelete
                                       }).ToList();
            return formRoleMappingList;
        }
        public List<FormRoleMapping> Permission(Guid RoleId)
        {
            List<FormRoleMapping> list = GetAll().Where(x => x.RoleId == RoleId).ToList();
            return list;
        }
    }
}
