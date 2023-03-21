using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Services
{
    public interface ICommonLookupService
    {
        void AddCommonLookup(CommonLookupsViewModel model);
        void UpdateCommonLookup(CommonLookupsViewModel model);
        void DeleteCommonLookup(Guid id);
        IEnumerable<CommonLookups> GetAll();
        CommonLookups GetById(Guid id);
    }
    public class CommonLookupService : ICommonLookupService
    {
        private readonly IRepository<CommonLookups> _repository;

        public CommonLookupService(IRepository<CommonLookups> repository)
        {
            _repository = repository;
        }

        public void AddCommonLookup(CommonLookupsViewModel model)
        {
            CommonLookups obj = new CommonLookups();
            obj.ConfigKey = model.ConfigKey;
            obj.ConfigName = model.ConfigName;
            obj.ConfigValue = model.ConfigValue;
            obj.DisplayOrder = model.DisplayOrder;
            obj.Description = model.Description;
            obj.IsActive = model.IsActive;
            _repository.Insert(obj);
            _repository.Commit();
        }
        public void UpdateCommonLookup(CommonLookupsViewModel model)
        {
            
            CommonLookups obj = GetById(model.Id);
            obj.ConfigKey = model.ConfigKey;
            obj.ConfigName = model.ConfigName;
            obj.ConfigValue = model.ConfigValue;
            obj.DisplayOrder = model.DisplayOrder;
            obj.Description = model.Description;
            obj.IsActive = model.IsActive;

            _repository.Update(obj);
            _repository.Commit();
        }
        public void DeleteCommonLookup(Guid id)
        {
            CommonLookups obj = _repository.Find(id);
            obj.IsDelete = true;
            _repository.Update(obj);
            _repository.Commit();
        }
        public IEnumerable<CommonLookups> GetAll()
        {
            return _repository.Collection().Where(x => x.IsDelete == false);
        }
        public CommonLookups GetById(Guid id)
        {
            return _repository.Find(id);
        }
    }
}
