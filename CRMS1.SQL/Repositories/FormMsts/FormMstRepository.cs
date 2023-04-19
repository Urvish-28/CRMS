using CRMS1.Core.Models;
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
    public interface IFormMstRepository
    {
        IEnumerable<FormMstViewModel> FormListIndex();
        IEnumerable<FormMstViewModel> FormMstMenuList();
    }
    public class FormMstRepository : IFormMstRepository
    {
        public CRMSEntities context;
        internal DbSet<FormMst> dbset;
        public FormMstRepository(CRMSEntities Context)
        {
            this.context = Context;
            this.dbset = context.Set<FormMst>();
        }
        public IEnumerable<FormMstViewModel> FormListIndex()
        {
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
                            ParentFormId = f.ParentFormId
                        }).ToList();
            return list;
        }
        public IEnumerable<FormMstViewModel> FormMstMenuList()
        {
            var formRoleList = HttpContext.Current.Session["Permission"] as List<FormRoleMapping>;
            var list = (from f in context.FormMst.ToList()
                        join formRole in formRoleList
                        on f.Id equals formRole.FormId
                        where formRole.AllowView == true
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
                            ParentFormId = f.ParentFormId
                        }).ToList();
            return list;
        }
    }
}
