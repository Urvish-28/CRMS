using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public class Rooms : BaseEntity
    {
        public string RoomName { get; set; }
        public string RoomNo { get; set; }

        public Rooms()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
        }
    }
}
