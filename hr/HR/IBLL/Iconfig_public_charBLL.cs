using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace IBLL
{
    public interface Iconfig_public_charBLL
    {
        List<config_public_char> SelectAll();

        List<config_public_char> SelectWhere(Expression<Func<config_public_char, bool>> where);

        int Add(config_public_char t);

        int Delete(config_public_char t);

        int Update(config_public_char t);
    }
}
