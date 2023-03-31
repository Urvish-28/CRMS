using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public class CommonLookups : BaseEntity
    {
        public string ConfigName { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }

        public CommonLookups()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
        }
    }
}
