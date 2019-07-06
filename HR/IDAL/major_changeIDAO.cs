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
        //分页查询调动登记
        DataTable FenYe(int currentPage, ref int page, ref int rows,string where);
        //分页查询调动审核
        DataTable FenYe2(int currentPage,ref int page,ref int rows,string where);
        //条件查询
        List<T> SelectWhere<T>(Expression<Func<T, bool>> where) where T : class;
    }
}
