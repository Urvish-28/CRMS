using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public class ConferenceRoom : BaseEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public ConferenceRoom()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
        }
    }
}
