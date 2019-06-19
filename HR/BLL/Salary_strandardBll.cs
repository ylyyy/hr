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
    public class Salary_strandardBll : ISalary_strandardBll
    {
        ISalary_strandardDal issdal = IocContain.CreateAll<ISalary_strandardDal>("yidao", "ssarydal");
        public int Add(salary_standard t)
        {
            return issdal.Add<salary_standard>(t);
        }

        public int Delete(salary_standard t)
        {
            return issdal.Delete<salary_standard>(t);
        }

        public List<salary_standard> FenYe(Expression<Func<salary_standard, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return issdal.FenYe<salary_standard,int>(e=>e.ssd_id, where,out rows,currentPage,pageSize);
        }

        public List<salary_standard> SelectAll()
        {
            return issdal.SelectAll<salary_standard>();
        }

        public List<salary_standard> SelectWhere(Expression<Func<salary_standard, bool>> where)
        {
            return issdal.SelectWhere<salary_standard>(where);
        }

        public int Update(salary_standard t)
        {
            return issdal.Update<salary_standard>(t);
        }
    }
}
