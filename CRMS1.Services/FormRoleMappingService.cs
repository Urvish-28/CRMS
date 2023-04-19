using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.FormroleMapping;
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
    public class FormRoleMappingService : IFormRoleMappingService
    {
        private readonly IRepository<FormRoleMapping> _repository;
        private readonly IFormMstService _formMstService;
        private readonly IFormRoleRepository _formRoleRepository;
        public FormRoleMappingService(IRepository<FormRoleMapping> repository, IFormMstService formMstService , IFormRoleRepository formRoleRepository)
        {
            _repository = repository;
            _formMstService = formMstService;
            _formRoleRepository = formRoleRepository;
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
            var formRoleMappingList = _formRoleRepository.GetAllForm(Id);
            return formRoleMappingList;
        }
        public List<FormRoleMapping> Permission(Guid RoleId)
        {
            List<FormRoleMapping> list = GetAll().Where(x => x.RoleId == RoleId).ToList();
            return list;
        }
    }
}
