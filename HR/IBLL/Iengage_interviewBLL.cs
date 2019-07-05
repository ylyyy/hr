using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace IBLL
{
    public interface Iengage_interviewBLL
    {
        List<engage_interview> SelectAll();

        List<engage_interview> SelectWhere(Expression<Func<engage_interview, bool>> where);

        int Add(engage_interview t);

        int Delete(engage_interview t);

        int Update(engage_interview t);

        List<engage_interview> FenYe(Expression<Func<engage_interview, int>> order, Expression<Func<engage_interview, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
