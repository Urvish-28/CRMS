using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.AuditRepository
{
    public interface IAuditRepository : IRepository<AuditLogs>
    {
        IEnumerable<ActivityLogViewModel> ActivityLogList(string Id);
        IEnumerable<ErrorLogViewModel> ErrorLogList(string Id);
    }
    public class AuditRepository : SqlRepository<AuditLogs>, IAuditRepository
    {
        public CRMSEntities _context;
        internal DbSet<AuditLogs> _dbset;
        public AuditRepository(CRMSEntities context) : base(context)
        {
            _dbset = this.dbset;
            _context = this.context;
        }
        public IEnumerable<ActivityLogViewModel> ActivityLogList(string Id)
        {
            var list = (from u in _context.Users.Where(x => x.IsDelete == false)
                        join a in _context.AuditLog on u.Id equals a.UserId
                        where (Id == "" || Id == a.Id.ToString())
                        select new ActivityLogViewModel()
                        {
                            Id = a.Id,
                            UserName = u.UserName,
                            BrowserInfo = a.BrowserInfo,
                            ExecutionDuration = a.ExecutionDuration,
                            ExecutionTime = a.ExecutionTime.ToString(),
                            ClientIpAddress = a.ClientIpAddress,
                            Comments = a.Comments,
                            Headers = a.Headers,
                            HttpMethod = a.HttpMethod,
                            HttpStatusCode = a.HttpStatusCode,
                            Parameters = a.Parameters,
                            Url = a.Url,
                            UserId = a.UserId,
                            CreatedOn = a.CreatedOn
                        }).ToList().OrderByDescending(x => x.CreatedOn);
            return list;
        }
        public IEnumerable<ErrorLogViewModel> ErrorLogList(string Id)
        {
            var list = (from u in _context.Users.Where(x => x.IsDelete == false)
                        join e in _context.AuditLog on u.Id equals e.UserId
                        where e.Exception != null &&
                        (Id == "" || Id == e.Id.ToString())
                        select new ErrorLogViewModel()
                        {
                            Id = e.Id,
                            UserName = u.UserName,
                            BrowserInfo = e.BrowserInfo,
                            ExecutionDuration = e.ExecutionDuration,
                            ExecutionTime = e.ExecutionTime.ToString(),
                            ClientIpAddress = e.ClientIpAddress,
                            Comments = e.Comments,
                            Headers = e.Headers,
                            HttpMethod = e.HttpMethod,
                            HttpStatusCode = e.HttpStatusCode,
                            Parameters = e.Parameters,
                            Url = e.Url,
                            UserId = e.UserId,
                            Exception = e.Exception,
                            CreatedOn = e.CreatedOn
                        }).ToList().OrderByDescending(x => x.CreatedOn);
            return list;
        }
    }
}
