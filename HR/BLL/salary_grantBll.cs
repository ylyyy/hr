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
    public class salary_grantBll : Isalary_grantBll
    {
        Isalary_grantDal grant = IocContain.CreateAll<Isalary_grantDal>("yidao", "sgrantdal");
        public int Add(salary_grant t)
        {
            return grant.Add<salary_grant>(t);
        }

        public int Delete(salary_grant t)
        {
            return grant.Delete<salary_grant>(t);
        }

        public List<salary_grant> FenYe(Expression<Func<salary_grant, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return grant.FenYe<salary_grant,int>(e=>e.sgr_id,where,out rows,currentPage,pageSize);
        }

        public List<salary_grant> SelectAll()
        {
            return grant.SelectAll<salary_grant>();
        }

        public List<salary_grant> SelectWhere(Expression<Func<salary_grant, bool>> where)
        {
            return grant.SelectWhere(where);
        }

        public int Update(salary_grant t)
        {
            return grant.Update<salary_grant>(t);
        }
    }
}
