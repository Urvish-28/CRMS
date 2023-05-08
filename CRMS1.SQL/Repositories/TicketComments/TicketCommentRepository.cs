using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.TicketComments
{
    public interface ITicketCommentRepository
    {
        IEnumerable<TicketCommentViewModel> GetAllComment(Guid TicketId);
    }
    public class TicketCommentRepository : ITicketCommentRepository
    {
        private readonly IRepository<TicketComment> _repository;
        private CRMSEntities _context;
        public TicketCommentRepository(IRepository<TicketComment> repository , CRMSEntities context)
        {
            _repository = repository;
            _context = context;
        }
        public IEnumerable<TicketCommentViewModel> GetAllComment(Guid TicketId)
        {
            var list = from u in _context.Users.Where(x => x.IsDelete == false)
                       join tc in _context.TicketComment.Where(x => x.IsDelete == false && x.TicketId == TicketId) on u.Id equals tc.CreatedBy 
                       select new TicketCommentViewModel()
                       {
                           Id = tc.Id,
                           TicketId = tc.TicketId,
                           Comment = tc.Comment,
                           UserName = u.Name
                       };
            return list;
        }
    }
}
