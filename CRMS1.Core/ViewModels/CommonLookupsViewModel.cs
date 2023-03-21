using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class CommonLookupsViewModel : BaseEntity
    {
        [Required]
        public string ConfigName { get; set; }
        [Required]
        [Compare("ConfigName" , ErrorMessage ="Can not be same")]
        public string ConfigKey { get; set; }
        [Required]
        public string ConfigValue { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
    }
}
