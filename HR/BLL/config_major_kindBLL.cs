using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocContainer;
using Entity;
using IBLL;
using IDAL;
using System.Linq.Expressions;

namespace BLL
{
    class config_major_kindBLL : Iconfig_major_kindBLL
    {
        Iconfig_major_kindDao ifmk = IocContain.CreateAll<Iconfig_major_kindDao>("yidao", "config_major_kindDao");
        public int Add(config_major_kind t)
        {
            return ifmk.Add(t);
        }

        public int Delete(config_major_kind t)
        {
            return ifmk.Delete(t);
        }

        public List<config_major_kind> SelectAll()
        {
            return ifmk.SelectAll<config_major_kind>();
        }

        public List<config_major_kind> SelectWhere(Expression<Func<config_major_kind, bool>> where)
        {
            return ifmk.SelectWhere(where);
        }

        public int Update(config_major_kind t)
        {
            return ifmk.Update(t);
        }
    }
}
