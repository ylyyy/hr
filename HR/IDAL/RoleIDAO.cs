using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;
using System.Linq.Expressions;

namespace IDAL
{
   public interface RoleIDAO
    {
        List<T> SelectWhere<T>(Expression<Func<T, bool>> where) where T : class;
        List<T> SelectAll<T>() where T : class;
        int Add<T>(T t) where T : class;
        int Update<T>(T t) where T : class;
        int Delete<T>(T t) where T : class;
        List<T> FenYe<T, K>(Expression<Func<T, K>> order, Expression<Func<T, bool>> where, out int rows, int currentPage, int pageSize) where T : class;
    }
}
