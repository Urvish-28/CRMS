using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public BaseEntity()
        {
            this.CreatedOn = DateTime.Now;
        }
    }
}
