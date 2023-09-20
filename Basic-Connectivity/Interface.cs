using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity
{
    public interface Interface<T, TId>
    {
        List<T> GetAll();
        T GetById(TId id);
        string Insert(T entity);
        string Update(T entity);
        string Delete(TId id);
    }
}
