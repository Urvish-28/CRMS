using CRMS1.Core.Models;
using CRMS1.SQL.Repositories.SqlRepository;
using CRMS1.SQL.Repositories.TicketAttachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Services
{
    public interface ITicketAttachmentService
    {
        string GetImageName(Guid Id);
        IEnumerable<TicketAttachment> AttachmentList(Guid TicketId);
        IEnumerable<TicketAttachment> GetAllAttachment();
        void DeleteAttachment(Guid Id);
    }
    public class TicketAttachmentService : ITicketAttachmentService
    {
        private readonly IRepository<TicketAttachment> _repository;
        private readonly ITicketAttachmentRepository _ticketAttachmentRepository;
        public TicketAttachmentService(ITicketAttachmentRepository ticketAttachmentRepository , IRepository<TicketAttachment> repository)
        {
            _ticketAttachmentRepository = ticketAttachmentRepository;
            _repository = repository;
        }
        public string GetImageName(Guid Id)
        {
            return _ticketAttachmentRepository.GetImageName(Id);
        }
        public IEnumerable<TicketAttachment> AttachmentList(Guid TicketId)
        {
            var List = _ticketAttachmentRepository.AttachmentList(TicketId);
            foreach (var item in List)
            {
                string name = item.FileName.Split('_')[0];
                item.FileName = item.FileName.Replace(item.FileName, name);
            }
            return List;
        }
        public IEnumerable<TicketAttachment> GetAllAttachment()
        {
            return _repository.Collection();
        }
        public void DeleteAttachment(Guid Id)
        {
            _ticketAttachmentRepository.DeleteAttachment(Id);
        }
    }
}
