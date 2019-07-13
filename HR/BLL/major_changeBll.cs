using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocContainer;
using IBLL;
using IDAL;
using Entity;
using System.Linq.Expressions;
using System.Data;


namespace BLL
{
    public class major_changeBll : major_changeIBLL
    {
        major_changeIDAO mc = IocContain.CreateAll<major_changeIDAO>("yidao", "major_changeDao");

        public int Add(major_change t)
        {
            return mc.Add(t);
        }

        public int Delete(major_change t)
        {
            return mc.Delete(t);
        }

        public List<major_change> FenYe(Expression<Func<major_change, int>> order, Expression<Func<major_change, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return mc.FenYe(order,where,out rows,currentPage,pageSize);
        }

        //分页查询调动审核
        public DataTable FenYe2(int currentPage, ref int page, ref int rows, string where)
        {
            return mc.FenYe2(currentPage,ref page,ref rows,where);
        }

        public List<major_change> SelectAll()
        {
            return mc.SelectAll<major_change>();
        }

        public List<major_change> SelectWhere(Expression<Func<major_change, bool>> where)
        {
            return mc.SelectWhere(where);
        }

        public int Update(major_change t)
        {
            return mc.Update(t);
        }
    }
}
