using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBHelper
    {
        public static string str = "Data Source=.;Initial Catalog=HR;User ID=sa;Password=123";
        public static DataTable Select(string sql, string fileName, params SqlParameter[] ps)
        {
            SqlConnection cn = new SqlConnection(str);

            SqlDataAdapter ad = new SqlDataAdapter(sql, cn);
            if (ps != null)
            {
                ad.SelectCommand.Parameters.AddRange(ps);
            }
            DataTable dt = new DataTable();
            try
            {
                ad.Fill(dt);
            }
            catch (Exception ex)
            {

                WRZ(fileName, ex);
            }
            return dt;
        }
        public static void WRZ(string fileName, Exception e)
        {
            using (StreamWriter sw = new StreamWriter("D:/错误日志.txt", true))
            {
                sw.WriteLine("报错信息：" + e.Message);
                sw.WriteLine("报错时间：" + DateTime.Now);
                sw.WriteLine("报错窗体：" + fileName);
                sw.WriteLine("-----------------------------------");
            }
        }
        //增删改
        public static int MyExecuteQuery(string sql, string fileName, params SqlParameter[] sp)
        {
            SqlConnection sqlcon =new SqlConnection(str);
            SqlCommand sqlcmd = new SqlCommand(sql, sqlcon);
            try
            {
                if (sp != null)
                {
                    sqlcmd.Parameters.AddRange(sp);
                }
                sqlcon.Open();
                return sqlcmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                WRZ(fileName, e);
                return -1;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        //datatable 查
        public static DataTable MyDataAdapter(string sql, string fileName, params SqlParameter[] sp)
        {
            SqlConnection sqlcon = new SqlConnection(str);
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlcon);
            DataTable dt = new DataTable();
            try
            {
                if (sp != null)
                {
                    sda.SelectCommand.Parameters.AddRange(sp);
                }
                sda.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                WRZ(fileName, e);
                return null;
            }
        }
        //返回单行单列
        public static object MyExecuteScalar(string sql, string fileName, params SqlParameter[] sp)
        {
            SqlConnection sqlcon = new SqlConnection(str);
            SqlCommand sqlcmd = new SqlCommand(sql, sqlcon);
            try
            {
                if (sp != null)
                {
                    sqlcmd.Parameters.AddRange(sp);
                }
                sqlcon.Open();
                return sqlcmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                WRZ(fileName, e);
                return null;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        //返回读取器
        public static SqlDataReader MyExecuteReader(string sql, string fileName, params SqlParameter[] sp)
        {
            SqlConnection sqlcon = new SqlConnection(str);
            SqlCommand sqlcmd = new SqlCommand(sql,sqlcon);
            try
            {
                if (sp != null)
                {
                    sqlcmd.Parameters.AddRange(sp);
                }
                sqlcon.Open();
                return sqlcmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                WRZ(fileName, e);
                return null;
            }
        }
        public static DataTable SelectProc(SqlParameter[] ps, string fileName)
        {
            SqlConnection cn = new SqlConnection(str);
            string sql = "proc_FenYe";

            SqlDataAdapter ad = new SqlDataAdapter(sql, cn);
            //执行的是存储过程
            ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            //命令对象添加参数对象
            ad.SelectCommand.Parameters.AddRange(ps);
            DataTable dt = new DataTable();
            try
            {
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                throw;

            }
            return dt;
        }

        public static object SelectSinger(string sql, string fileName, params SqlParameter[] ps)
        {
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand(sql, cn);
            if (ps != null)
            {
                cmd.Parameters.AddRange(ps);
            }
            object obj = null;
            try
            {
                cn.Open();
                obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                WRZ(fileName, ex);

            }
            finally
            {
                cn.Close();
            }

            return obj;
        }
        //分页查询
        public static DataTable MyFenYe(string fyname,SqlParameter[] ps, string fileName)
        {
            SqlConnection sqlcon = new SqlConnection(str);
            string sql = fyname;
            SqlDataAdapter ad = new SqlDataAdapter(sql,sqlcon);
            //执行的是存储过程
            ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            //命令对象添加参数对象
            ad.SelectCommand.Parameters.AddRange(ps);
            DataTable dt = new DataTable();
            try
            {
                ad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                WRZ(fileName, ex);
                return null;
            }
        }
        public static int InsertUpdateDelte(string sql, params SqlParameter[] ps)
        {
            SqlConnection cn = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand(sql, cn);
            if (ps != null)
            {
                cmd.Parameters.AddRange(ps);
            }
            int result = 0;
            try
            {
                cn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
            return result;
        }
    }
}