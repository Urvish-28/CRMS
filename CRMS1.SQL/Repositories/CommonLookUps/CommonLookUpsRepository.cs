using CRMS1.Core.Models;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.SQL.Repositories.CommonLookUps
{
    public interface ICommonLookUpsRepository
    {
        IEnumerable<CommonLookups> DropDownList(string configName);
    }
    public class CommonLookUpsRepository : ICommonLookUpsRepository
    {
        private readonly IRepository<CommonLookups> _repository;
        public CommonLookUpsRepository(IRepository<CommonLookups> repository)
        {
            _repository = repository;
        }
        public IEnumerable<CommonLookups> DropDownList(string configName)
        {
            var list = _repository.Collection().Where(x => x.ConfigName == configName && x.IsDelete == false).ToList();
            return list;
        }
    }
}
