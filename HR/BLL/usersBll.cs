using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;
using IDAL;
using IocContainer;

namespace BLL
{
    public class usersBll : IusersBll
    {
        IusersDal user = IocContain.CreateAll<IusersDal>("yidao", "userdal");
        public int Add(users t)
        {
            return user.Add(t);
        }

        public int Delete(users t)
        {
            return user.Delete(t);
        }

        public List<users> SelectAll()
        {
            return user.SelectAll<users>();
        }

        public List<users> SelectWhere(Expression<Func<users, bool>> where)
        {
            return user.SelectWhere(where);
        }

        public int Update(users t)
        {
            return user.Update(t);
        }
    }
}
