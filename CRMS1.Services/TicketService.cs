using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using CRMS1.SQL.Repositories.TicketAttachments;
using CRMS1.SQL.Repositories.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRMS1.Services
{
    public interface ITicketService
    {
        Ticket GetById(Guid id);
        IEnumerable<Ticket> GetAllTicket();
        Ticket BindTicket(TicketViewModel model);
        TicketViewModel BindTicket(Ticket model);
        void CreateTicket(TicketViewModel model);
        void UpdateTicket(TicketViewModel model);
        void DeleteTicket(Guid Id);
        IEnumerable<TicketIndexViewModel> TicketListForIndex(string ticketId = "");
        HomeViewModel GetStatusCountForChart();
        HomeViewModel GetPriorityCount();
    }
    public class TicketService : Page, ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketAttachmentRepository _ticketAttachmentRepository;
        public TicketService(ITicketRepository ticketRepository, 
                             ITicketAttachmentRepository ticketAttachmentRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketAttachmentRepository = ticketAttachmentRepository;
        }
        public Ticket GetById(Guid id)
        {
            return _ticketRepository.Find(id);
        }
        public IEnumerable<Ticket> GetAllTicket()
        {
            return _ticketRepository.Collection();
        }
        public Ticket BindTicket(TicketViewModel model)
        {
            Ticket obj = GetById(model.Id);
            if (obj == null)
            {
                obj = new Ticket();
                obj.CreatedBy = (Guid)Session["UserId"];
            }
            else
            {
                obj.UpdatedOn = DateTime.Now;
                obj.UpdatedBy = (Guid)Session["UserId"];
            }
            obj.Id = model.Id;
            obj.Title = model.Title;
            obj.AssignTo = model.AssignTo;
            obj.Description = model.Description;
            obj.PriorityId = model.PriorityId;
            obj.StatusId = model.StatusId;
            obj.TypeId = model.TypeId;
            return obj;
        }
        public TicketViewModel BindTicket(Ticket model)
        {
            TicketViewModel obj = new TicketViewModel();
            obj.Id = model.Id;
            obj.Title = model.Title;
            obj.AssignTo = model.AssignTo;
            obj.Description = model.Description;
            obj.PriorityId = model.PriorityId;
            obj.StatusId = model.StatusId;
            obj.TypeId = model.TypeId;
            return obj;
        }
        public void CreateTicket(TicketViewModel model)
        {
            Ticket obj = new Ticket();
            obj = BindTicket(model);
            _ticketRepository.Insert(obj);
            if (model.Image != null)
            {
                model.Id = obj.Id;
                _ticketAttachmentRepository.CreateAttachment(model);
            }
        }
        public void UpdateTicket(TicketViewModel model)
        {
            Ticket obj = GetById(model.Id);
            obj = BindTicket(model);
            if (model.Image != null)
            {
                _ticketAttachmentRepository.CreateAttachment(model);
            }

            _ticketRepository.Update(obj);
        }
        public void DeleteTicket(Guid Id)
        {
            Ticket obj = GetById(Id);
            obj.IsDelete = true;
            _ticketRepository.Update(obj);
            
            _ticketAttachmentRepository.DeleteAttachmentByTicket(Id);
        }
        public IEnumerable<TicketIndexViewModel> TicketListForIndex(string ticketId)
        {
            return _ticketRepository.TicketIndexList(ticketId);
        }
        public HomeViewModel GetStatusCountForChart()
        {
            return _ticketRepository.StatusCountForChart();
        }
        public HomeViewModel GetPriorityCount()
        {
            return _ticketRepository.BarChart();
        }
    }
}
