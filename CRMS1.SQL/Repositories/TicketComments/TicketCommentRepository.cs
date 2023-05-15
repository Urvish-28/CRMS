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
    public interface ITicketCommentRepository : IRepository<TicketComment>
    {
        IEnumerable<TicketCommentViewModel> GetAllComment(Guid TicketId);
    }
    public class TicketCommentRepository :SqlRepository<TicketComment> , ITicketCommentRepository
    {
        private CRMSEntities _context;
        public TicketCommentRepository(CRMSEntities context) : base(context)
        {
            _context = this.context;
        }
        public IEnumerable<TicketCommentViewModel> GetAllComment(Guid TicketId)
        {
            var list = (from u in _context.Users.Where(x => x.IsDelete == false)
                        join tc in _context.TicketComment.Where(x => x.IsDelete == false && x.TicketId == TicketId) on u.Id equals tc.CreatedBy
                        select new TicketCommentViewModel()
                        {
                            Id = tc.Id,
                            TicketId = tc.TicketId,
                            Comment = tc.Comment,
                            UserName = u.Name,
                            CreatedOn = (DateTime)(tc.UpdatedOn.HasValue ? tc.UpdatedOn : tc.CreatedOn),
                            CreatedBy = u.Id
                        }).ToList().OrderByDescending(x=>x.CreatedOn);
            return list;
        }
    }
}
