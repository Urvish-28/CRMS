using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public class FormRoleMapping : BaseEntity
    {
        public Guid? FormId { get; set; }
        public Guid? RoleId { get; set; }
        public bool AllowInsert { get; set; }
        public bool AllowView { get; set; }
        public bool AllowUpdate { get; set; }
        public bool AllowDelete { get; set; }
    }
}
