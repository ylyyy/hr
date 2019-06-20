using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace IBLL
{
    public interface Iconfig_majorBLL
    {
        List<config_major> SelectAll();

        List<config_major> SelectWhere(Expression<Func<config_major, bool>> where);

        int Add(config_major t);

        int Delete(config_major t);

        int Update(config_major t);
    }
}
