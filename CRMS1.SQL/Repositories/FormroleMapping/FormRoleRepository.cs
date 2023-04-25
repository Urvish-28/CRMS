using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.FormroleMapping
{
    public interface IFormRoleRepository
    {
        IEnumerable<FormRoleMappingVM> GetFormRoleList(Guid Id);
    }
    public class FormRoleRepository : IFormRoleRepository
    {
        internal CRMSEntities context;
        internal DbSet<FormRoleMapping> dbset;
        public FormRoleRepository(CRMSEntities Context)
        {
            context = Context;
            this.dbset = context.Set<FormRoleMapping>();
        }
        public IEnumerable<FormRoleMappingVM> GetFormRoleList(Guid Id)
        {
            var formRoleMappingList = (from f in context.FormMst
                                       join fr in context.FormRoleMapping.Where(x=>x.RoleId == Id)
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
    }
}
