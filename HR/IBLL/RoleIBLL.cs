using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;
using System.Data;

namespace IBLL
{
    public interface RoleIBLL
    {
        List<Role> SelectAll();
        int Add(Role t);
        int Delete(Role id);
        List<Role> SelectWhere(Expression<Func<Role, bool>> where);
        int Update(Role t);
        List<Role> FenYestandard(Expression<Func<Role, int>> order, Expression<Func<Role, bool>> where, out int rows, int currentPage, int pageSize);

    }
}
