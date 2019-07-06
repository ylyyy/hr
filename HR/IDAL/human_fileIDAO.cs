using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface  human_fileIDAO
    {
        //查询全部
        List<T> SelectAll<T>() where T : class;

        //条件查询
        List<T> SelectWhere<T>(Expression<Func<T, bool>> where) where T : class;
    }
}
