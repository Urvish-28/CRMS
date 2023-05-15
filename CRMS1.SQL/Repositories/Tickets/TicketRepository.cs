using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMS1.SQL.Repositories.Tickets
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        IEnumerable<TicketIndexViewModel> TicketIndexList(string ticketId);
    }
    public class TicketRepository :SqlRepository<Ticket>, ITicketRepository
    {
        public CRMSEntities _context;
        public TicketRepository(CRMSEntities context) : base(context)
        {
            _context = this.context;
        }
        public IEnumerable<TicketIndexViewModel> TicketIndexList(string ticketId)
        {
            bool CheckRoleCode = (bool)HttpContext.Current.Session["RoleCode"];
            Guid userId = (Guid)HttpContext.Current.Session["UserId"];
            if (CheckRoleCode == false)
            {
                var list = from t in _context.Ticket.Where(x => x.IsDelete == false && x.AssignTo == userId)
                           join cType in _context.CommonLookups.Where(x => x.IsDelete == false) on t.TypeId equals cType.Id
                           join cPrio in _context.CommonLookups.Where(x => x.IsDelete == false) on t.PriorityId equals cPrio.Id
                           join cSta in _context.CommonLookups.Where(x => x.IsDelete == false) on t.StatusId equals cSta.Id
                           join user in _context.Users.Where(x => x.IsDelete == false) on t.AssignTo equals user.Id
                           where (ticketId == "" || ticketId == t.Id.ToString())
                           select new TicketIndexViewModel()
                           {
                               Id = t.Id,
                               Title = t.Title,
                               AssignTo = user.Name,
                               Priority = cPrio.ConfigValue,
                               Status = cSta.ConfigValue,
                               Type = cType.ConfigValue,
                               AttachmentCount = _context.TicketAttachment.Where(x => x.TicketId == t.Id && x.IsDelete == false).Count()
                           };
                return list;
            }
            else
            {
                var list = from t in _context.Ticket.Where(x => x.IsDelete == false)
                           join cType in _context.CommonLookups.Where(x => x.IsDelete == false) on t.TypeId equals cType.Id
                           join cPrio in _context.CommonLookups.Where(x => x.IsDelete == false) on t.PriorityId equals cPrio.Id
                           join cSta in _context.CommonLookups.Where(x => x.IsDelete == false) on t.StatusId equals cSta.Id
                           join user in _context.Users.Where(x => x.IsDelete == false) on t.AssignTo equals user.Id
                           where (ticketId == "" || ticketId == t.Id.ToString())
                           select new TicketIndexViewModel()
                           {
                               Id = t.Id,
                               Title = t.Title,
                               AssignTo = user.Name,
                               Priority = cPrio.ConfigValue,
                               Status = cSta.ConfigValue,
                               Type = cType.ConfigValue,
                               AttachmentCount = _context.TicketAttachment.Where(x => x.TicketId == t.Id && x.IsDelete == false).Count()
                           };
                return list;
            }
        }
    }
}
