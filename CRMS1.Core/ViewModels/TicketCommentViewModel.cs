using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class TicketCommentViewModel
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public TicketCommentViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
