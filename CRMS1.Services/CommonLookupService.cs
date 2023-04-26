﻿using CRMS1.Core.Models;
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
    public interface ICommonLookupService
    {
        void AddCommonLookup(CommonLookupsViewModel model);
        void UpdateCommonLookup(CommonLookupsViewModel model);
        void DeleteCommonLookup(Guid id);
        List<CommonLookupsViewModel> GetAll();
        CommonLookups GetById(Guid id);

        CommonLookups BindCommonLookupModel(CommonLookupsViewModel model);
        CommonLookupsViewModel BindCommonLookupModel(CommonLookups model);
        bool IsAlreadyExist(CommonLookupsViewModel model, bool IsCreated = false);
    }
    public class CommonLookupService  :Page, ICommonLookupService 
    {
        private readonly IRepository<CommonLookups> _repository;

        public CommonLookupService(IRepository<CommonLookups> repository)
        {
            _repository = repository;
        }
        public void AddCommonLookup(CommonLookupsViewModel model)
        {
            CommonLookups obj = new CommonLookups();
            obj = BindCommonLookupModel(model);
            _repository.Insert(obj);
            _repository.Commit();
        }
        public void UpdateCommonLookup(CommonLookupsViewModel model)
        {
            CommonLookups obj = GetById(model.Id);
            obj = BindCommonLookupModel(model);
            _repository.Update(obj);
        }
        public void DeleteCommonLookup(Guid id)
        {
            CommonLookups obj = _repository.Find(id);
            obj.IsDelete = true;
            _repository.Update(obj);
            _repository.Commit();
        }
        public List<CommonLookupsViewModel> GetAll()
        {
            IEnumerable<CommonLookups> model= _repository.Collection().Where(x => x.IsDelete == false);
            List<CommonLookupsViewModel> obj = new List<CommonLookupsViewModel>();
            foreach(var item in model)
            {
                CommonLookupsViewModel viewModel = new CommonLookupsViewModel();
                viewModel.Id = item.Id;
                viewModel.ConfigName = item.ConfigName;
                viewModel.ConfigKey = item.ConfigKey;
                viewModel.ConfigValue = item.ConfigValue;
                viewModel.DisplayOrder = item.DisplayOrder;
                viewModel.IsActive = item.IsActive;
                viewModel.Description = item.Description;
                obj.Add(viewModel);
            }
            return obj;
        }
        public CommonLookups GetById(Guid id)
        {
            return _repository.Find(id);
        }
        public CommonLookups BindCommonLookupModel(CommonLookupsViewModel model)
        {
            CommonLookups obj = GetById(model.Id);
            if (obj == null)
            {
                obj = new CommonLookups();
                obj.CreatedOn = DateTime.Now;
                obj.CreatedBy = (Guid)Session["UserId"];
            }
            else
            {
                obj.UpdatedOn = DateTime.Now;
                obj.UpdatedBy = (Guid)Session["UserId"];
            }
            obj.Id = model.Id;
            obj.ConfigKey = model.ConfigKey;
            obj.ConfigName = model.ConfigName;
            obj.ConfigValue = model.ConfigValue;
            obj.DisplayOrder = model.DisplayOrder;
            obj.Description = model.Description;
            obj.IsActive = model.IsActive;
            return obj;
        }
        public CommonLookupsViewModel BindCommonLookupModel(CommonLookups model)
        {
            CommonLookupsViewModel obj = new CommonLookupsViewModel();
            obj.Id = model.Id;
            obj.ConfigKey = model.ConfigKey;
            obj.ConfigName = model.ConfigName;
            obj.ConfigValue = model.ConfigValue;
            obj.DisplayOrder = model.DisplayOrder;
            obj.Description = model.Description;
            obj.IsActive = model.IsActive;
            return obj;
        }
        public bool IsAlreadyExist(CommonLookupsViewModel model , bool IsCreated = false)
        {

            var records = GetAll().Where(x => (x.ConfigName.ToLower() == model.ConfigName.ToLower() &&
                                               x.ConfigKey.ToLower() == model.ConfigKey.ToLower()) &&
                                               (IsCreated || x.Id !=model.Id)
                                              ).ToList();                          
            if(records.Count() > 0)                                                 
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
