using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Entity;
using System.Linq.Expressions;
using System.Data;

namespace IBLL
{
    public interface transferIBLL
    {
        List<major_change> SelectAll();
        List<major_change> SelectWhere(Expression<Func<major_change, bool>> where);
        int Update(major_change t);
    }
}
