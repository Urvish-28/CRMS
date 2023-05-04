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
    public interface ITicketRepository
    {
        IEnumerable<TicketIndexViewModel> TicketIndexList();
    }
    public class TicketRepository : ITicketRepository
    {
        public CRMSEntities _context;
        public TicketRepository(IRepository<Ticket> repository , CRMSEntities context)
        {
            _context = context;
        }
        public IEnumerable<TicketIndexViewModel> TicketIndexList()
        {
            var list = from t in _context.Ticket.Where(x => x.IsDelete == false)
                       join cType in _context.CommonLookups.Where(x => x.IsDelete == false) on t.TypeId equals cType.Id
                       join cPrio in _context.CommonLookups.Where(x => x.IsDelete == false) on t.PriorityId equals cPrio.Id
                       join cSta in _context.CommonLookups.Where(x => x.IsDelete == false) on t.StatusId equals cSta.Id
                       join user in _context.Users.Where(x => x.IsDelete == false) on t.AssignTo equals user.Id
                       /*                       join Ta in _context.TicketAttachment on t.Id equals Ta.TicketId into tab
                                              from fTab in tab.DefaultIfEmpty()*/
                       select new TicketIndexViewModel()
                       {
                           Id = t.Id,
                           Title = t.Title,
                           AssignTo = user.Name,
                           Priority = cPrio.ConfigValue,
                           Status = cSta.ConfigValue,
                           Type = cType.ConfigValue,
                           AttachmentCount = _context.TicketAttachment.Where(x => x.TicketId == t.Id && x.IsDelete == false).Count()
                           /*ImageName = fTab.FileName*/
                       };
            return list;
        }
    }
}
