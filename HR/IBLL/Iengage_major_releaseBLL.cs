using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface Iengage_major_releaseBLL
    {
        List<engage_major_release> SelectAll();

        List<engage_major_release> SelectWhere(Expression<Func<engage_major_release, bool>> where);

        int Add(engage_major_release t);

        int Delete(engage_major_release t);

        int Update(engage_major_release t);

        List<engage_major_release> FenYe(Expression<Func<engage_major_release, int>> order, Expression<Func<engage_major_release, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
