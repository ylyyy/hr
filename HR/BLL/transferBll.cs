using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using IocContainer;
using IBLL;
using IDAL;
using Entity;
using System.Linq.Expressions;
using System.Data;

namespace BLL
{
    public class transferBll:transferIBLL
    {
        transferIDAO rd = IocContain.CreateAll<transferIDAO>("yidao", "transferDao");
        public List<major_change> SelectAll() {
            return rd.SelectAll<major_change>();
        }
        public List<major_change> SelectWhere(Expression<Func<major_change, bool>> where)
        {
            return rd.SelectWhere<major_change>(where);
        }
        public int Update(major_change t)
        {
            return rd.Update(t);
        }
    }
}
