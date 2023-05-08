using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using CRMS1.SQL.Repositories.TicketComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CRMS1.Services
{
    public interface ITicketCommentService
    {
        TicketComment GetById(Guid Id);
        IEnumerable<TicketCommentViewModel> GetAllComment(Guid TicketId);
        TicketComment BindModel(TicketCommentViewModel model);
        TicketCommentViewModel BindModel(TicketComment model);
        void AddComment(TicketCommentViewModel model);
        void EditComment(TicketCommentViewModel model);
        void DeleteComment(Guid Id);
    }
    public class TicketCommentService :Page, ITicketCommentService
    {
        private readonly IRepository<TicketComment> _repository;
        private readonly ITicketCommentRepository _ticketCommentRepository;
        public TicketCommentService(IRepository<TicketComment> repository, ITicketCommentRepository ticketCommentRepository)
        {
            _ticketCommentRepository = ticketCommentRepository;
            _repository = repository;
        }
        public TicketComment BindModel(TicketCommentViewModel model)
        {
            TicketComment obj = _repository.Find(model.Id);
            if(obj == null)
            {
                obj = new TicketComment();
                obj.CreatedBy = (Guid)Session["UserId"];
            }
            else
            {
                obj.UpdatedBy = (Guid)Session["UserId"];
                obj.UpdatedOn = DateTime.Now;
            }
            obj.Id = model.Id;
            obj.TicketId = model.TicketId;
            obj.Comment = model.Comment;
            return obj;
        }
        public TicketCommentViewModel BindModel(TicketComment model)
        {
            TicketCommentViewModel obj = new TicketCommentViewModel();
            obj.Id = model.Id;
            obj.TicketId = model.TicketId;
            obj.Comment = model.Comment;
            return obj;
        }
        public TicketComment GetById(Guid Id)
        {
            return _repository.Find(Id);
        }
        public IEnumerable<TicketCommentViewModel> GetAllComment(Guid TicketId)
        {
            return _ticketCommentRepository.GetAllComment(TicketId);
        }   
        public void AddComment(TicketCommentViewModel model)
        {
            TicketComment obj = new TicketComment();
            obj = BindModel(model);
            _repository.Insert(obj);
        }
        public void EditComment(TicketCommentViewModel model)
        {
            TicketComment obj = GetById(model.Id);
            obj = BindModel(model);
            _repository.Update(obj);
        }
        public void DeleteComment(Guid Id)
        {
            TicketComment obj = GetById(Id);
            obj.IsDelete = true;
            _repository.Commit();
        }
    }
}
