using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aj.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class  //Remeber: <T> where T : class - written to make interface generic. 
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T,bool>>filter); //Remeber: Expression<Func<T,bool>>filter which is written to accept arrow function
        void Add(T entity);
        void Remove(T entity);
        void RemoveRaneg(IEnumerable<T> entities);

    }
}
