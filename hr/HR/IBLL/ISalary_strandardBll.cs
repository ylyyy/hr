using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ISalary_strandardBll
    {
        List<salary_standard> SelectAll();
        List<salary_standard> SelectWhere(Expression<Func<salary_standard, bool>> where);
        int Add(salary_standard t);
        int Delete(salary_standard t);
        int Update(salary_standard t);
        List<salary_standard> FenYe(Expression<Func<salary_standard, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
