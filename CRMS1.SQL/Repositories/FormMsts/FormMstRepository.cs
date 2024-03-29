﻿using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMS1.SQL.Repositories.FormMsts
{
    public interface IFormMstRepository : IRepository<FormMst>
    {
        IEnumerable<FormMstViewModel> FormListIndex();
        IEnumerable<FormMstViewModel> FormMstMenuList(bool isMenu);
    }
    public class FormMstRepository :SqlRepository<FormMst>, IFormMstRepository
    {
        public CRMSEntities _context;
        internal DbSet<FormMst> _dbset;
        public FormMstRepository(CRMSEntities context) :base(context)
        {
            _context = this.context;
            _dbset = this.dbset;
        }
        public IEnumerable<FormMstViewModel> FormListIndex()
        {
            var allForms = context.FormMst;
            var list = (from f in context.FormMst
                        select new FormMstViewModel()
                        {
                            Id = f.Id,
                            Name = f.Name,
                            NavigateURL = f.NavigateURL,
                            ParentFormName = (from fParent in context.FormMst
                                              where fParent.Id == f.ParentFormId
                                              select fParent.Name).FirstOrDefault(),
                            FormAccessCode = f.FormAccessCode,
                            DisplayOrder = f.DisplayOrder,
                            IsActive = f.IsActive,
                            IsMenu = f.IsMenu,
                            ParentFormId = f.ParentFormId,
                            CreatedOn = f.CreatedOn,
                            HasChild = (allForms.Where(x => x.ParentFormId == f.Id && x.IsMenu).Select(x => x.Id).Any())
                        }).OrderBy(x => x.CreatedOn).ToList();
            return list;
        }
        public IEnumerable<FormMstViewModel> FormMstMenuList(bool isMenu)
        {
            bool CheckRoleCode = (bool)HttpContext.Current.Session["RoleCode"];
            if (CheckRoleCode == false)
            {
                var allForms = FormListbyRoleId();
                var formRoleList = HttpContext.Current.Session["Permission"] as List<FormRoleMapping>;
                var list = (from f in context.FormMst.ToList()
                            join formRole in formRoleList
                            on f.Id equals formRole.FormId
                            where formRole.AllowView == true && f.IsMenu == isMenu
                            select new FormMstViewModel()
                            {
                                Id = f.Id,
                                Name = f.Name,
                                NavigateURL = f.NavigateURL,
                                ParentFormName = (from fParent in context.FormMst
                                                  where fParent.Id == f.ParentFormId
                                                  select fParent.Name).FirstOrDefault(),
                                FormAccessCode = f.FormAccessCode,
                                DisplayOrder = f.DisplayOrder,
                                IsActive = f.IsActive,
                                IsMenu = f.IsMenu,
                                ParentFormId = f.ParentFormId,
                                CreatedOn = f.CreatedOn,
                                AllowView = formRole.AllowView,
                                HasChild = (allForms.Where(x => x.ParentFormId == f.Id && x.IsMenu).Select(x => x.Id).Any())
                            }).OrderBy(x => x.CreatedOn).ToList();
                return list;
            }
            else
            {
                return FormListIndex();
            }
        }
        public IEnumerable<FormMstViewModel> FormListbyRoleId()
        {
            Guid roleId = (Guid)HttpContext.Current.Session["UserRoleId"];
            var list = (from fm in context.FormMst
                        join formrole in context.FormRoleMapping
                        on fm.Id equals formrole.FormId
                        where formrole.RoleId == roleId && formrole.AllowView
                        select new FormMstViewModel()
                        {
                            Id = (Guid)formrole.FormId,
                            Name = fm.Name,
                            ParentFormId = fm.ParentFormId,
                            NavigateURL = fm.NavigateURL,
                            FormAccessCode = fm.FormAccessCode,
                            DisplayOrder = fm.DisplayOrder,
                            IsActive = fm.IsActive,
                            AllowView = formrole.AllowView,
                            IsMenu = fm.IsMenu,
                            CreatedOn = fm.CreatedOn
                        }).OrderBy(x=>x.CreatedOn).ToList();
            return list;
        }
    }
}
