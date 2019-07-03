using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ISalaryBll
    {
        List<salary> SelectAll();
        List<salary> SelectWhere(Expression<Func<salary, bool>> where);
        int Add(salary t);
        int Delete(salary t);
        int Update(salary t);
        List<salary> FenYe(Expression<Func<salary, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
