using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.FormroleMapping
{
    public interface IFormRoleRepository : IRepository<FormRoleMapping>
    {
        IEnumerable<FormRoleMappingVM> GetFormRoleList(Guid Id);
    }
    public class FormRoleRepository :SqlRepository<FormRoleMapping>, IFormRoleRepository
    {
        internal CRMSEntities _context;
        internal DbSet<FormRoleMapping> _dbset;
        public FormRoleRepository(CRMSEntities context) : base(context)
        {
            _context = this.context;
            _dbset = this.dbset;
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
