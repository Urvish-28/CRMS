using CRMS1.SQL;
using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TMS.Data
{
    public class BaseProvider : IDisposable
    {
        public CRMSEntities _db;
        public BaseProvider()
        {
            _db = new CRMSEntities();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
           