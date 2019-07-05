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
    public class engage_interviewBLL:Iengage_interviewBLL
    {
        Iengage_interviewDao ikf = IocContain.CreateAll<Iengage_interviewDao>("yidao", "engage_interviewDao");

        public int Add(engage_interview t)
        {
            return ikf.Add(t);
        }

        public int Delete(engage_interview t)
        {
            return ikf.Delete(t);
        }

        public List<engage_interview> FenYe(Expression<Func<engage_interview, int>> order, Expression<Func<engage_interview, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return ikf.FenYe(order,where,out rows,currentPage,pageSize);
        }

        public List<engage_interview> SelectAll()
        {
            return ikf.SelectAll<engage_interview>();
        }

        public List<engage_interview> SelectWhere(Expression<Func<engage_interview, bool>> where)
        {
            return ikf.SelectWhere(where);
        }

        public int Update(engage_interview t)
        {
            return ikf.Update(t);
        }
    }
}
