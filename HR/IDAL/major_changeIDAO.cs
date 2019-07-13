using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
   public interface major_changeIDAO
    {
        //分页查询调动审核
        DataTable FenYe2(int currentPage,ref int page,ref int rows,string where);
        //条件查询
        List<T> SelectWhere<T>(Expression<Func<T, bool>> where) where T : class;
        //List<T> SelectWhere1<T>(Expression<Func<T, bool>> where) where T : class;

        List<T> SelectAll<T>() where T : class;

        int Add<T>(T t) where T : class;

        int Delete<T>(T t) where T : class;

        int Update<T>(T t) where T : class;

        List<T> FenYe<T, K>(Expression<Func<T, K>> order, Expression<Func<T, bool>> where, out int rows, int currentPage, int pageSize) where T : class;

    }
}
