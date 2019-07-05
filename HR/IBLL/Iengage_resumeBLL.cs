using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface Iengage_resumeBLL
    {
        List<engage_resume> SelectAll();

        List<engage_resume> SelectWhere(Expression<Func<engage_resume, bool>> where);

        int Add(engage_resume t);

        int Delete(engage_resume t);

        int Update(engage_resume t);

        List<engage_resume> FenYe(Expression<Func<engage_resume, int>> order, Expression<Func<engage_resume, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
