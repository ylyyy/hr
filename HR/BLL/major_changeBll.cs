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
        
        public DataTable FenYe(int currentPage, ref int page, ref int rows, string where)
        {
            return mc.FenYe(currentPage,ref page,ref rows,where);
        }
        //分页查询调动审核
        public DataTable FenYe2(int currentPage, ref int page, ref int rows, string where)
        {
            return mc.FenYe2(currentPage,ref page,ref rows,where);
        }

        public List<major_change> SelectWhere(Expression<Func<major_change, bool>> where)
        {
            return mc.SelectWhere(where);
        }
    }
}
