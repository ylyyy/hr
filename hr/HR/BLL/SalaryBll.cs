using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;
using IocContainer;
using IDAL;

namespace BLL
{
    public class SalaryBll : ISalaryBll
    {

        ISalaryDal idal = IocContain.CreateAll<ISalaryDal>("yidao", "sarydal");
        public int Add(salary t)
        {
            return idal.Add<salary>(t);
        }

        public int Delete(salary t)
        {
            return idal.Delete<salary>(t);
        }

        public List<salary> FenYe(Expression<Func<salary, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return idal.FenYe<salary,int>(e=>e.sid_id,where,out rows,currentPage,pageSize);
        }

        public List<salary> SelectAll()
        {
            return idal.SelectAll<salary>();
        }

        public List<salary> SelectWhere(Expression<Func<salary, bool>> where)
        {
            return idal.SelectWhere<salary>(where);
        }

        public int Update(salary t)
        {
            return idal.Update<salary>(t);
        }
    }
}
