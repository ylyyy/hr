using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocContainer;
using IDAL;
using Entity;
using IBLL;
using System.Linq.Expressions;

namespace BLL
{
    class config_file_first_kindBLL : Iconfig_file_first_kindBLL
    {
        Iconfig_file_first_kindDao ikf = IocContain.CreateAll<Iconfig_file_first_kindDao>("yidao", "config_file_first_kindDao");
        public int Add(config_file_first_kind t)
        {
            return ikf.Add(t);
        }

        public int Delete(config_file_first_kind t)
        {
            return ikf.Delete(t);
        }

        public List<config_file_first_kind> FenYe(Expression<Func<config_file_first_kind, int>> order, Expression<Func<config_file_first_kind, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return ikf.FenYe(order, where,out rows, currentPage, pageSize);
        }

        public List<config_file_first_kind> SelectAll()
        {
            return ikf.SelectAll<config_file_first_kind>();
        }

        public List<config_file_first_kind> SelectWhere(Expression<Func<config_file_first_kind, bool>> where)
        {
            return ikf.SelectWhere<config_file_first_kind>(where);
        }

        public int Update(config_file_first_kind t)
        {
            return ikf.Update(t);
        }
    }
}
