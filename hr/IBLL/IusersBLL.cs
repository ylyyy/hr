using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IusersBLL
    {
        List<users> SelectAll();

        List<users> SelectWhere(Expression<Func<users, bool>> where);

        int Add(users t);

        int Delete(users t);

        int Update(users t);

        List<users> FenYe(Expression<Func<users, int>> order, Expression<Func<users, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
