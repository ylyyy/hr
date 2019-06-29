using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Entity;
using System.Linq.Expressions;
using System.Data;

namespace IBLL
{
    public interface UserIBLL 
    {
        List<users> SelectWhere(Expression<Func<users, bool>> where);
        DataTable QuanUserSel();
        int Add(users t);
        int Update(users t);
        int Delete(users id);
        //List<T> FenYe<T, K>(Expression<Func<T, K>> order, Expression<Func<T, bool>> where, out int rows, int currentPage, int pageSize);
        List<users> FenYestandard(Expression<Func<users, int>> order, Expression<Func<users, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
