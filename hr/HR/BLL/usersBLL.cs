using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocContainer;
using IDAL;
using IBLL;
using Entity;
using System.Linq.Expressions;

namespace BLL
{
    class usersBLL:IusersBLL
    {
        IusersDao ikf = IocContain.CreateAll<IusersDao>("yidao", "usersDao");

        public int Add(users t)
        {
            return ikf.Add(t);
        }

        public int Delete(users t)
        {
            return ikf.Delete(t);
        }

        public List<users> FenYe(Expression<Func<users, int>> order, Expression<Func<users, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return ikf.FenYe(order, where, out rows, currentPage, pageSize);
        }

        public List<users> SelectAll()
        {
            return ikf.SelectAll<users>();
        }

        public List<users> SelectWhere(Expression<Func<users, bool>> where)
        {
            return ikf.SelectWhere(where);
        }

        public int Update(users t)
        {
            return ikf.Update(t);
        }
    }
}
