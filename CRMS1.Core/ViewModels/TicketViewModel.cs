using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMS1.Core.ViewModels
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid AssignTo { get; set; }
        public IEnumerable<DropDown> AssignToDropDown { get; set; }
        [Required]
        [Display(Name = "Type")]
        public Guid TypeId { get; set; }
        public IEnumerable<DropDown> TypeDropDown { get; set; }
        [Required]
        [Display(Name = "Priority")]
        public Guid PriorityId { get; set; }
        public IEnumerable<DropDown> PriorityDropDown { get; set; }
        [Required]
        [Display(Name = "Status")]
        public Guid StatusId { get; set; }
        public IEnumerable<DropDown> StatusDropDown { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public IEnumerable<TicketAttachment> Attachments { get; set; }
        public string AttachmentListFromView { get; set; }
        public TicketViewModel()
        {
            this.Id = Guid.NewGuid();
        }
    }
    public class TicketIndexViewModel : BaseEntity
    {
        public string Title { get; set; }
        public string AssignTo { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int AttachmentCount  { get; set; }
    }
}
