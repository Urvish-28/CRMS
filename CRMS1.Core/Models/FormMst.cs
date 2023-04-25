using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public class FormMst : BaseEntity
    {
        public string Name { get; set; }
        public string NavigateURL { get; set; }
        public Guid? ParentFormId { get; set; }
        public string FormAccessCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMenu { get; set; }
    }
}
