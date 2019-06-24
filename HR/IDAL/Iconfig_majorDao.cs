using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface Iconfig_majorDao
    {
        List<T> SelectAll<T>() where T : class;

        List<T> SelectWhere<T>(Expression<Func<T, bool>> where) where T : class;

        int Add<T>(T t) where T : class;

        int Delete<T>(T t) where T : class;

        int Update<T>(T t) where T : class;
    }
}
