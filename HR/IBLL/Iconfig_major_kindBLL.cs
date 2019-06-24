using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace IBLL
{
    public interface Iconfig_major_kindBLL
    {
        List<config_major_kind> SelectAll();

        List<config_major_kind> SelectWhere(Expression<Func<config_major_kind, bool>> where);

        int Add(config_major_kind t);

        int Delete(config_major_kind t);

        int Update(config_major_kind t);
    }
}
