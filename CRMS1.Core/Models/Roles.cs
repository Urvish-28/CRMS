using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public class Roles : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Roles()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
