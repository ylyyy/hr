using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface Isalary_grantBll
    {
        List<salary_grant> SelectAll();
        List<salary_grant> SelectWhere(Expression<Func<salary_grant, bool>> where);
        int Add(salary_grant t);
        int Delete(salary_grant t);
        int Update(salary_grant t);
        List<salary_grant> FenYe(Expression<Func<salary_grant, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
