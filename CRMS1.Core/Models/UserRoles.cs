using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public class UserRoles : BaseEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("RoleId")]
        public Roles role { get; set; }
    }
}
