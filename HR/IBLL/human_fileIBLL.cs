using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public interface human_fileIBLL
    {
        //查询全部
        List<config_file_first_kind> SelectAll<config_file_first_kind>() where config_file_first_kind : class;

        //条件查询
        List<config_file_second_kind> SelectWhere(Expression<Func<config_file_second_kind, bool>> where);
    }
}
