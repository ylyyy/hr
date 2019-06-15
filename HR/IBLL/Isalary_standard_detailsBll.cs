using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface Isalary_standard_detailsBll
    {
        List<salary_standard_details> SelectAll();
        List<salary_standard_details> SelectWhere(Expression<Func<salary_standard_details, bool>> where);
        int Add(salary_standard_details t);
        int Delete(salary_standard_details t);
        int Update(salary_standard_details t);
        List<salary_standard_details> FenYe(Expression<Func<salary_standard_details, bool>> where, out int rows, int currentPage, int pageSize);
        
    }
}
