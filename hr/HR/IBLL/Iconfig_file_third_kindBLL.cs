using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace IBLL
{
    public interface Iconfig_file_third_kindBLL
    {
        List<config_file_third_kind> SelectAll();

        List<config_file_third_kind> SelectWhere(Expression<Func<config_file_third_kind, bool>> where);

        int Add(config_file_third_kind t);

        int Delete(config_file_third_kind t);

        int Update(config_file_third_kind t);
    }
}
