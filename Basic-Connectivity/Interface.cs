using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity
{
    public interface Interface<T>
    {
        List<T> GetAll();
        T GetById(int id);
        string Insert(T entity);
        string Update(T entity);
        string Delete(T entity);
    }
}
