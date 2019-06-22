using Entity;
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
    public class salary_standard_detailsDal : BaseDao, Isalary_standard_detailsDal
    {
        //用存储过程获取薪酬标准编号
        public string bianHao() {
            SqlParameter[] sqlp = new SqlParameter[1];
            sqlp[0] = new SqlParameter("@zhi", SqlDbType.VarChar, 14);
            sqlp[0].Direction = ParameterDirection.Output;
            DataTable dt = DBHelper.MyFenYe("[bhao]", sqlp, "salary_standard_detailsDal");
            string zhi = "";
            if (sqlp[0].Value != null)
            {
                zhi = sqlp[0].Value.ToString();
            }
            return zhi;
        }
        public int DeleteWhere(string id) {

            //SqlParameter sqlp = new SqlParameter() {
            //    ParameterName="@standard_id",Value=id
            //};
            //int aa= es.salary_standard_details.SqlQuery("delete  dbo.salary_standard_details where standard_id='@standard_id'", id);

            return 0;
        }
    }
}
