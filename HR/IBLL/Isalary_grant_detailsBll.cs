using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface Isalary_grant_detailsBll
    {
        List<salary_grant_details> SelectAll();
        List<salary_grant_details> SelectWhere(Expression<Func<salary_grant_details, bool>> where);
        int Add(salary_grant_details t);
        int Delete(salary_grant_details t);
        int Update(salary_grant_details t);
        List<salary_grant_details> FenYe(Expression<Func<salary_grant_details, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
