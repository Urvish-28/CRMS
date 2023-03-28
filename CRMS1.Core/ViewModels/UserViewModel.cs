using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class UserViewModel : BaseEntity
    {
        [Required]
        [Display(Name ="Employee Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType("Password")]
        [StringLength(8)]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Role")]
        public Guid RoleId { get; set; }
        public List<DropDown> RoleDropdown { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please Enter Valid Mobile Number")]
        public string MobileNo { get; set; }
        [Required]
        public string Gender { get; set; }
    }
    public class DropDown
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class IndexViewModel : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
    }
}
