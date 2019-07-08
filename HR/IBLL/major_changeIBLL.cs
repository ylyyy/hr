using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public interface major_changeIBLL
    {
        //分页查询调动登记
        DataTable FenYe(int currentPage, ref int page, ref int rows,string where);
        //分页查询调动审核
        DataTable FenYe2(int currentPage,ref int page,ref int rows,string where);
        //查询单行
        List<major_change> SelectWhere(Expression<Func<major_change, bool>> where);
        //List<major_change> SelectWhere1(Expression<Func<major_change, bool>> where);
    }
}
