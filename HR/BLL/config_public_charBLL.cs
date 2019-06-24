using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocContainer;
using IDAL;
using IBLL;
using Entity;
using System.Linq.Expressions;

namespace BLL
{
    class config_public_charBLL : Iconfig_public_charBLL
    {
        Iconfig_public_charDao ipc = IocContain.CreateAll<Iconfig_public_charDao>("yidao", "config_public_charDao");
        public int Add(config_public_char t)
        {
            return ipc.Add(t);
        }

        public int Delete(config_public_char t)
        {
            return ipc.Delete(t);
        }

        public List<config_public_char> SelectAll()
        {
            return ipc.SelectAll<config_public_char>();
        }

        public List<config_public_char> SelectWhere(Expression<Func<config_public_char, bool>> where)
        {
            return ipc.SelectWhere(where);
        }

        public int Update(config_public_char t)
        {
            return ipc.Update(t);
        }
    }
}
