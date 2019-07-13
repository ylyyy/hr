using IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class major_changeDao:BaseDao, major_changeIDAO
    {
        public DataTable FenYe2(int currentPage, ref int page, ref int rows, string where)
        {
            SqlParameter[] sp =
            {
                            new SqlParameter(){ParameterName="@pageSize",Value=2},
                            new SqlParameter(){ParameterName="@KeyName",Value="mch_id"},
                            new SqlParameter(){ParameterName="@TableName",Value="dbo.major_change"},
                            new SqlParameter(){ParameterName="@CurrentPage",Value=currentPage},
                            new SqlParameter(){ParameterName="@where",Value=where},
                            new SqlParameter(){ParameterName="@pages",SqlDbType= SqlDbType.Int, Direction= ParameterDirection.Output},
                            new SqlParameter(){ParameterName="@rows",SqlDbType= SqlDbType.Int, Direction= ParameterDirection.Output}
            };
            DataTable dt = DBHelper.SelectProc(sp,"");
            page = (int)sp[5].Value;
            rows = (int)sp[6].Value;
            return dt;
        }
    }
}
