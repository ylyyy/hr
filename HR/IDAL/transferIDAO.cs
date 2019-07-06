﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;
using System.Linq.Expressions;

namespace IDAL
{
   public interface transferIDAO
    {
        List<T> SelectAll<T>() where T : class;
        List<T> SelectWhere<T>(Expression<Func<T, bool>> where) where T : class;
        int Update<T>(T t) where T : class;
    }
}
