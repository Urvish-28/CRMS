using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class FormMstViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NavigateURL { get; set; }
        public Guid? ParentFormId { get; set; }
        public List<DropDown> ParentFormDropDown { get; set; }
        [Required]
        [RegularExpression(@"[A-Z]*$",
         ErrorMessage = "Only uppercase Characters are allowed.")]
        public string FormAccessCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string ParentFormName { get; set; }
        public bool AllowView { get; set; }
        public bool IsMenu { get; set; }
        public bool HasChild { get; set; }
        public DateTime CreatedOn { get; set; }
        public FormMstViewModel()
        {
            this.Id = Guid.NewGuid();
        }
    }
}  
