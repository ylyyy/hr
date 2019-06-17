using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;

namespace DAL
{
    public class BaseDao
    {
        protected HREntities1 es = CreateDBContent();
        public static HREntities1 CreateDBContent()
        {
            HREntities1 at = CallContext.GetData("s") as HREntities1;
            if (at == null)
            {
                at = new HREntities1();
                CallContext.SetData("s", at);
            }
            return at;
        }
        public List<T> SelectAll<T>() where T :class {
            return es.Set<T>().Select(e=>e).ToList();
        }
        public List<T> SelectWhere<T>(Expression<Func<T,bool>> where)where T :class {
            return es.Set<T>().Where(where).Select(e => e).ToList();
        }
        public int Add<T>(T t)where T :class {
            es.Entry<T>(t).State = System.Data.Entity.EntityState.Added;
            return es.SaveChanges();
        }
        public int Delete<T>(T t)where T :class
        {
            es.Entry<T>(t).State = System.Data.Entity.EntityState.Deleted;
            return es.SaveChanges();
        }
        public int Update<T>(T t)where T :class
        {
            es.Entry<T>(t).State = System.Data.Entity.EntityState.Modified;
            return es.SaveChanges();
        }
        public List<T> FenYe<T,K>(Expression<Func<T,K>> order,Expression<Func<T,bool>> where,out int rows,int currentPage,int pageSize)where T:class {
            var data=es.Set<T>().OrderBy(order).Where(where);
            rows = data.Count();
            return data.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}