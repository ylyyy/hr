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
    class engage_major_releaseBLL:Iengage_major_releaseBLL
    {
        Iengage_major_releaseDao ikf = IocContain.CreateAll<Iengage_major_releaseDao>("yidao", "engage_major_releaseDao");

        public int Add(engage_major_release t)
        {
            return ikf.Add(t);
        }

        public int Delete(engage_major_release t)
        {
            return ikf.Delete(t);
        }

        public List<engage_major_release> FenYe(Expression<Func<engage_major_release, int>> order, Expression<Func<engage_major_release, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return ikf.FenYe(order, where, out rows, currentPage, pageSize);
        }

        public List<engage_major_release> SelectAll()
        {
            return ikf.SelectAll<engage_major_release>();
        }

        public List<engage_major_release> SelectWhere(Expression<Func<engage_major_release, bool>> where)
        {
            return ikf.SelectWhere(where);
        }

        public int Update(engage_major_release t)
        {
            return ikf.Update(t);
        }
    }
}
