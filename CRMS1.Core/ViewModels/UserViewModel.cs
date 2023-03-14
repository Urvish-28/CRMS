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
        public string Password { get; set; }
        [Required]
        [Display(Name ="Role")]
        public Guid RoleId { get; set; }
        public List<DropDown> RoleDropdown { get; set; }
    }
    public class DropDown
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class IndexViewModel : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
