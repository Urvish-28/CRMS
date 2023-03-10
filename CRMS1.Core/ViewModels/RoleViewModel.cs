using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class RoleViewModel : BaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
