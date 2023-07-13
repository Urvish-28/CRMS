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
        HomeViewModel StatusCountForChart();
        HomeViewModel BarChart();
    }
    public class TicketRepository : SqlRepository<Ticket>, ITicketRepository
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
        public HomeViewModel StatusCountForChart()
        {
            var homeModel = new HomeViewModel();
            List<ChartViewModel> chartModel = (from t in _context.Ticket.Where(x => x.IsDelete == false)
                                               join c in _context.CommonLookups on t.StatusId equals c.Id
                                               select new ChartViewModel
                                               {
                                                   category = c.ConfigValue
                                               }).ToList();
            homeModel.ChartData = new List<ChartViewModel>();
            var NewCount = chartModel.Where(x => x.category == "New").Count();
            if (NewCount != 0)
            {
                homeModel.ChartData.Add(new ChartViewModel
                {
                    value = NewCount,
                    category = "New"
                });
            }
            var returnedCount = chartModel.Where(x => x.category == "Returned").Count();
            if (returnedCount != 0)
            {
                homeModel.ChartData.Add(new ChartViewModel
                {
                    value = returnedCount,
                    category = "Returned"
                });
            }
            var pendingCount = chartModel.Where(x => x.category == "Pending").Count();
            if (pendingCount != 0)
            {
                homeModel.ChartData.Add(new ChartViewModel
                {
                    value = chartModel.Where(x => x.category == "Pending").Count(),
                    category = "Pending"
                });
            }
            var assignCount = chartModel.Where(x => x.category == "Assigned").Count();
            if (assignCount != 0)
            {
                homeModel.ChartData.Add(new ChartViewModel
                {
                    value = assignCount,
                    category = "Assigned"
                });
            }
            var resolvedCount = chartModel.Where(x => x.category == "Resolved").Count();
            if (resolvedCount != 0)
            {
                homeModel.ChartData.Add(new ChartViewModel
                {
                    value = resolvedCount,
                    category = "Resolved"
                });
            }
            var inprogressCount = chartModel.Where(x => x.category == "InProgress").Count();
            if (inprogressCount != 0)
            {
                homeModel.ChartData.Add(new ChartViewModel
                {
                    value = inprogressCount,
                    category = "InProgress"
                });
            }
            var approvedCount = chartModel.Where(x => x.category == "Approved").Count();
            if (approvedCount != 0)
            {
                homeModel.ChartData.Add(new ChartViewModel
                {
                    value = approvedCount,
                    category = "Approved"
                });
            }
            var rejectedCount = chartModel.Where(x => x.category == "Rejected").Count();
            if (rejectedCount != 0)
            {
                homeModel.ChartData.Add(new ChartViewModel
                {
                    value = rejectedCount,
                    category = "Rejected"
                });
            }
            var closedCount = chartModel.Where(x => x.category == "Closed").Count();
            if (closedCount != 0)
            {
                homeModel.ChartData.Add(new ChartViewModel
                {
                    value = closedCount,
                    category = "Closed"
                });
            }
            return homeModel;
        }
        public HomeViewModel BarChart()
        {
            var homeModel = new HomeViewModel();
            List<ChartViewModel> model = (from t in _context.Ticket.Where(x => x.IsDelete == false)
                                          join c in _context.CommonLookups on t.PriorityId equals c.Id
                                          select new ChartViewModel
                                          {
                                              category = c.ConfigValue
                                          }).ToList();
            homeModel.BarChartData = new List<ChartViewModel>();
            var Immediate = model.Where(x => x.category == "Immediate").Count();
            homeModel.BarChartData.Add(new ChartViewModel
            {
                category = "Immediate",
                value = Immediate
            });
            var high = model.Where(x => x.category == "High").Count();
            homeModel.BarChartData.Add(new ChartViewModel
            {
                category = "High",
                value = high
            });
            var Medium = model.Where(x => x.category == "Medium").Count();
            homeModel.BarChartData.Add(new ChartViewModel
            {
                category = "Medium",
                value = Medium
            });
            var Low = model.Where(x => x.category == "Low").Count();
            homeModel.BarChartData.Add(new ChartViewModel
            {
                category = "Low",
                value = Low
            });
            return homeModel;
        }
    }
}
