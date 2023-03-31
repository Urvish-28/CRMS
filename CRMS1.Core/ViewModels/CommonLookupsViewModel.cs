using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class CommonLookupsViewModel 
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="please enter ConfigName")]
        public string ConfigName { get; set; }
        [Required(ErrorMessage = "please enter ConfigKey")]
        public string ConfigKey { get; set; }
        [Required(ErrorMessage = "please enter ConfigValue")]
        public string ConfigValue { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public CommonLookupsViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
