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
    class engage_resumeBLL:Iengage_resumeBLL
    {
        Iengage_resumeDao ikf = IocContain.CreateAll<Iengage_resumeDao>("yidao", "engage_resumeDao");

        public int Add(engage_resume t)
        {
            return ikf.Add(t);
        }

        public int Delete(engage_resume t)
        {
            return ikf.Delete(t);
        }

        public List<engage_resume> FenYe(Expression<Func<engage_resume, int>> order, Expression<Func<engage_resume, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return ikf.FenYe(order, where, out rows, currentPage, pageSize);
        }

        public List<engage_resume> SelectAll()
        {
            return ikf.SelectAll<engage_resume>();
        }

        public List<engage_resume> SelectWhere(Expression<Func<engage_resume, bool>> where)
        {
            return ikf.SelectWhere(where);
        }

        public int Update(engage_resume t)
        {
            return ikf.Update(t);
        }
    }
}
