﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;

namespace IDAL
{
   public interface transferIDAO
    {
        List<T> SelectAll<T>() where T : class;
    }
}
