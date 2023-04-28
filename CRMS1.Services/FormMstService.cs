using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.FormMsts;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRMS1.Services
{
    public interface IFormMstService
    {
        void AddFormMst(FormMstViewModel model);
        void UpdateFormMst(FormMstViewModel model);
        void DeleteFormMst(Guid id);
        IEnumerable<FormMst> GetAllFormMst();
        FormMst GetById(Guid id);
        FormMst BindFormMst(FormMstViewModel model);
        FormMstViewModel BindFormMst(FormMst model);
        bool IsAlreadyExist(FormMstViewModel model, bool IsCreated = false);
        IEnumerable<FormMstViewModel> FormMstList(bool isMenu = false);
        IEnumerable<FormMstViewModel> FormMstListIndex();
    }
    public class FormMstService : Page, IFormMstService
    {
        private readonly IRepository<FormMst> _repository;
        private readonly IFormMstRepository _formMstRepository;
        private readonly IFormRoleMappingService _formRoleMappingService;
        public FormMstService(IRepository<FormMst> repository , IFormMstRepository formMstRepository , IFormRoleMappingService formRoleMappingService)
        {
            _formMstRepository = formMstRepository;
            _repository = repository;
            _formRoleMappingService = formRoleMappingService;
        }
        public void AddFormMst(FormMstViewModel model)
        {
            FormMst obj = new FormMst();
            obj = BindFormMst(model);
            _repository.Insert(obj);
        }
        public void UpdateFormMst(FormMstViewModel model)
        {
            FormMst obj = GetById(model.Id);
            obj = BindFormMst(model);
            _repository.Update(obj);
        }
        public void DeleteFormMst(Guid id)
        {
            FormMst obj = GetById(id);
            _repository.Delete(id);
            _repository.Commit();

            var formRoleList = _formRoleMappingService.GetAll().Where(x => x.FormId == id);
            _formRoleMappingService.DeleteBulk(formRoleList);
        }
        public IEnumerable<FormMst> GetAllFormMst()
        {
            return _repository.Collection().ToList();
        }
        public FormMst GetById(Guid id)
        {
            return _repository.Find(id);
        }
        public FormMst BindFormMst(FormMstViewModel model)
        {
            FormMst obj = GetById(model.Id);
            if (obj == null)
            {
                obj = new FormMst();
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
            obj.NavigateURL = model.NavigateURL;
            obj.ParentFormId = model.ParentFormId;
            obj.FormAccessCode = model.FormAccessCode;
            obj.DisplayOrder = model.DisplayOrder;
            obj.IsActive = model.IsActive;
            obj.IsMenu = model.IsMenu;
            return obj;

        }
        public FormMstViewModel BindFormMst(FormMst model)
        {
            FormMstViewModel obj = new FormMstViewModel();
            obj.Id = model.Id;
            obj.Name = model.Name;
            obj.NavigateURL = model.NavigateURL;
            obj.ParentFormId = model.ParentFormId;
            obj.FormAccessCode = model.FormAccessCode;
            obj.DisplayOrder = model.DisplayOrder;
            obj.IsActive = model.IsActive;
            obj.IsMenu = model.IsMenu;
            return obj;
        }
        public bool IsAlreadyExist(FormMstViewModel model, bool IsCreated = false)
        {
            var forms = GetAllFormMst().Where(x => (x.Name.ToLower() == model.Name.ToLower() ||
                                                  x.FormAccessCode.ToLower() == model.FormAccessCode.ToLower()) &&
                                                  (IsCreated || x.Id != model.Id)).ToList();

            if (forms.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public IEnumerable<FormMstViewModel> FormMstList(bool isMenu)
        {
            var list = _formMstRepository.FormMstMenuList(isMenu);
            return list;
        }
        public IEnumerable<FormMstViewModel> FormMstListIndex()
        {
            var list = _formMstRepository.FormListIndex();
            return list;
        }
    }
}
