using CRMS1.Core.Models;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.CommonLookUps
{
    public interface ICommonLookUpsRepository : IRepository<CommonLookups>
    {
        IEnumerable<CommonLookups> DropDownList(string configName);
    }
    public class CommonLookUpsRepository :SqlRepository<CommonLookups>, ICommonLookUpsRepository
    {
        public CRMSEntities _context;
        internal DbSet<CommonLookups> _dbset;
        public CommonLookUpsRepository(CRMSEntities context) : base(context)
        {
            _dbset = this.dbset;
            _context = this.context;
        }
        public IEnumerable<CommonLookups> DropDownList(string configName)
        {
            var list = Collection().Where(x => x.ConfigName == configName && x.IsDelete == false).ToList();
            return list;
        }
    }
}
