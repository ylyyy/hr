using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface Ihuman_fileBll
    {
        List<human_file> SelectAll();
        List<human_file> SelectWhere(Expression<Func<human_file, bool>> where);
        int Add(human_file t);
        int Delete(human_file t);
        int Update(human_file t);
        List<human_file> FenYe(Expression<Func<human_file, bool>> where, out int rows, int currentPage, int pageSize);
        string bianHao();
        string hmbianHao();
    }
}
