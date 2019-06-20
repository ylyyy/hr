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

namespace BLL
{
    class config_majorBLL : Iconfig_majorBLL
    {
        Iconfig_majorDao ifm = IocContain.CreateAll<Iconfig_majorDao>("yidao", "config_majorDao");
        public int Add(config_major t)
        {
            return ifm.Add(t);
        }

        public int Delete(config_major t)
        {
            return ifm.Delete(t);
        }

        public List<config_major> SelectAll()
        {
            return ifm.SelectAll<config_major>();
        }

        public List<config_major> SelectWhere(Expression<Func<config_major, bool>> where)
        {
            return ifm.SelectWhere(where);
        }

        public int Update(config_major t)
        {
            return ifm.Update(t);
        }
    }
}
