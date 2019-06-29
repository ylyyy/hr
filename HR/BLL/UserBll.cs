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
    public class UserBll:UserIBLL
    {
        UserIDAO ui = IocContain.CreateAll<UserIDAO>("yidao", "UserDao");
       public List<users> SelectWhere(Expression<Func<users, bool>> where){
            return ui.SelectWhere<users>(where);
        }
        public DataTable QuanUserSel() {
            return ui.QuanUserSel();
        }
        public int Add(users t){
            return ui.Add(t);
        }
        public int Update(users t) {
            return ui.Update(t);
        }
        public int Delete(users t) {
            return ui.Delete(t);
        }
        public List<users> FenYestandard(Expression<Func<users, int>> order, Expression<Func<users, bool>> where, out int rows, int currentPage, int pageSize)
        { 
            return ui.FenYe(order, where, out rows, currentPage, pageSize);
        }
    }
}
