using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using IBLL;
using IocContainer;
using Entity;
using System.Linq.Expressions;

namespace BLL
{
    class config_file_second_kindBLL : Iconfig_file_second_kindBLL
    {
        Iconfig_file_second_kindDao ifkd = IocContain.CreateAll<Iconfig_file_second_kindDao>("yidao", "config_file_second_kindDao");
        public int Add(config_file_second_kind t)
        {
            return ifkd.Add(t);
        }

        public int Delete(config_file_second_kind t)
        {
            return ifkd.Delete(t);
        }

        public List<config_file_second_kind> SelectAll()
        {
            return ifkd.SelectAll<config_file_second_kind>();
        }

        public List<config_file_second_kind> SelectShuang()
        {
            return ifkd.SelectShuang();
        }

        public List<config_file_second_kind> SelectWhere(Expression<Func<config_file_second_kind, bool>> where)
        {
            return ifkd.SelectWhere(where);
        }

        public int Update(config_file_second_kind t)
        {
            return ifkd.Update(t);
        }
    }
}
