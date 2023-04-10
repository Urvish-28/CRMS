using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class ChangePasswordViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Current password")]
        [DataType("Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password Do not match")]
        public string ConfirmPassword { get; set; }
    }
}
