using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace IBLL
{
    public interface Iconfig_file_second_kindBLL
    {
        List<config_file_second_kind> SelectAll();

        List<config_file_second_kind> SelectWhere(Expression<Func<config_file_second_kind, bool>> where);

        int Add(config_file_second_kind t);

        int Delete(config_file_second_kind t);

        int Update(config_file_second_kind t);

        List<config_file_second_kind> SelectShuang();
    }
}
