using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IDAL;

namespace DAL
{
    class config_file_second_kindDao : BaseDao, Iconfig_file_second_kindDao
    {
        public List<config_file_second_kind> SelectShuang()
        {
            return es.config_file_second_kind.Join(es.config_file_first_kind, p => p.first_kind_id, c =>c.first_kind_id, (p, c) => p).ToList();
        }
    }
}
