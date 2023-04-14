using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class FormRoleMappingVM
    {
        public Guid Id { get; set; }
        public Guid? FormId { get; set; }
        public Guid? RoleId { get; set; }
        public bool AllowInsert { get; set; }
        public bool AllowView { get; set; }
        public bool AllowUpdate { get; set; }
        public bool AllowDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string FormName { get; set; }
        public bool AllRights { get; set; }
        public FormRoleMappingVM()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
