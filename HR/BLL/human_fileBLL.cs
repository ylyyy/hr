using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocContainer;
using IBLL;
using IDAL;
using Entity;
using System.Linq.Expressions;
using System.Data;


namespace BLL
{
    public class human_fileBll : human_fileIBLL
    {
        human_fileIDAO ri = IocContain.CreateAll<human_fileIDAO>("yidao", "human_fileDao");
        

        public List<config_file_first_kind> SelectAll<config_file_first_kind>() where config_file_first_kind : class
        {
            return ri.SelectAll<config_file_first_kind>();
        }

        public List<config_file_second_kind> SelectWhere(Expression<Func<config_file_second_kind, bool>> where)
        {
            return ri.SelectWhere<config_file_second_kind>(where);
        }
    }
}
