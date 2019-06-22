using IBLL;
using IDAL;
using IocContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace BLL
{
    public class salary_standard_detailsBll: Isalary_standard_detailsBll
    {
        Isalary_standard_detailsDal isd = IocContain.CreateAll<Isalary_standard_detailsDal>("yidao", "ssddal");

        public int Add(salary_standard_details t)
        {
            return isd.Add(t);
        }

        public string bianHao()
        {
            return isd.bianHao();
        }

        public int Delete(salary_standard_details t)
        {
            return isd.Delete(t);
        }

        public int DeleteWhere(string id)
        {
            return isd.DeleteWhere(id);
        }

        public List<salary_standard_details> FenYe(Expression<Func<salary_standard_details, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return isd.FenYe<salary_standard_details,int>(e=>e.sdt_id, where,out rows,currentPage,pageSize);
        }

        public List<salary_standard_details> SelectAll()
        {
            return isd.SelectAll<salary_standard_details>();
        }

        public List<salary_standard_details> SelectWhere(Expression<Func<salary_standard_details, bool>> where)
        {
            return isd.SelectWhere(where);
        }

        public int Update(salary_standard_details t)
        {
            return isd.Update(t);
        }
    }
}
