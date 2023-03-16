using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class RoomViewModel : BaseEntity
    {
        [Required]
        [Display(Name ="Name")]
        public string RoomName { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
