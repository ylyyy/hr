using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace IBLL
{
    public interface Iconfig_file_first_kindBLL
    {
        List<config_file_first_kind> SelectAll();

        List<config_file_first_kind> SelectWhere(Expression<Func<config_file_first_kind, bool>> where);

        int Add(config_file_first_kind t);

        int Delete(config_file_first_kind t);

        int Update(config_file_first_kind t);

        List<config_file_first_kind> FenYe(Expression<Func<config_file_first_kind, int>> order, Expression<Func<config_file_first_kind, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
