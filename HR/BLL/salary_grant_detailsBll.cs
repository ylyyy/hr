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
    public class salary_grant_detailsBll : Isalary_grant_detailsBll
    {
        Isalary_grant_detailsDal grantdetail = IocContain.CreateAll<Isalary_grant_detailsDal>("yidao", "sgrantdetaildal");
        public int Add(salary_grant_details t)
        {
            return grantdetail.Add(t);
        }

        public int Delete(salary_grant_details t)
        {
            return grantdetail.Delete(t);
        }

        public List<salary_grant_details> FenYe(Expression<Func<salary_grant_details, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return grantdetail.FenYe<salary_grant_details,int>(e=>e.grd_id,where,out rows,currentPage,pageSize);
        }

        public List<salary_grant_details> SelectAll()
        {
            return grantdetail.SelectAll<salary_grant_details>();
        }

        public List<salary_grant_details> SelectWhere(Expression<Func<salary_grant_details, bool>> where)
        {
            return grantdetail.SelectWhere(where);
        }

        public int Update(salary_grant_details t)
        {
            return grantdetail.Update(t);
        }
    }
}
