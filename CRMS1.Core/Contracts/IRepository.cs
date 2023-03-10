using CRMS1.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Insert(T t);
        void Update(T t);
        void Delete(Guid Id);
        
        T Find(Guid Id); 
        IQueryable<T> Collection();

        //T GetById(Guid Id);
        //IEnumerable<T> GetAll();
        void Commit();
        
    }
}
