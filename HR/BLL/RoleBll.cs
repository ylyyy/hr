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
   public class RoleBll:RoleIBLL
    {
        RoleIDAO rd = IocContain.CreateAll<RoleIDAO>("yidao", "RoleDao");
        public List<Role> SelectAll() {
            return rd.SelectAll<Role>();
        }
        public int Add(Role t)
        {
            return rd.Add(t);
        }
        public int Update(Role t)
        {
            return rd.Update(t);
        }
        public int Delete(Role t)
        {
            return rd.Delete(t);
        }
        public List<Role> SelectWhere(Expression<Func<Role, bool>> where)
        {
            return rd.SelectWhere<Role>(where);
        }
        public List<Role> FenYestandard(Expression<Func<Role, int>> order, Expression<Func<Role, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return rd.FenYe(order, where, out rows, currentPage, pageSize);
        }
    }
    
}
