using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;
using IDAL;
using IocContainer;

namespace BLL
{
    public class human_fileBll : Ihuman_fileBll
    {
        Ihuman_fileDal human = IocContain.CreateAll<Ihuman_fileDal>("yidao", "humandal");
        public int Add(human_file t)
        {
            return human.Add<human_file>(t);
        }

        public string bianHao()
        {
            return human.bianHao();
        }

        public int Delete(human_file t)
        {
            return human.Delete<human_file>(t);
        }

        public List<human_file> FenYe(Expression<Func<human_file, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return human.FenYe<human_file,int>(e=>e.huf_id,where,out rows,currentPage,pageSize);
        }

        public string hmbianHao()
        {
            return human.hmbianHao();
        }

        public List<human_file> SelectAll()
        {
            return human.SelectAll<human_file>();
        }

        public List<human_file> SelectWhere(Expression<Func<human_file, bool>> where)
        {
            return human.SelectWhere<human_file>(where);
        }

        public int Update(human_file t)
        {
            return human.Update<human_file>(t);
        }
    }
}
