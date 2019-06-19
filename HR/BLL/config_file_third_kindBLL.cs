using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IBLL;
using IocContainer;
using IDAL;

namespace BLL
{
    class config_file_third_kindBLL : Iconfig_file_third_kindBLL
    {
        Iconfig_file_third_kindDao iftk = IocContain.CreateAll<Iconfig_file_third_kindDao>("yidao", "config_file_third_kindDao");
        public int Add(config_file_third_kind t)
        {
            return iftk.Add(t);
        }

        public int Delete(config_file_third_kind t)
        {
            return iftk.Delete(t);
        }

        public List<config_file_third_kind> SelectAll()
        {
            return iftk.SelectAll<config_file_third_kind>();
        }

        public List<config_file_third_kind> SelectWhere(Expression<Func<config_file_third_kind, bool>> where)
        {
            return iftk.SelectWhere(where);
        }
        public int Update(config_file_third_kind t)
        {
            return iftk.Update(t);
        }
    }
}
