using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace QLMyPham.DAL
{
 public   class Data
    {
     String strConn = @"Data Source=DESKTOP-TGJ3UQT;Initial Catalog = MYPHAM; User ID = sa; Password = 123";
        public SqlConnection getConnect()
        {
            String strConn = @"Data Source=DESKTOP-TGJ3UQT;Initial Catalog = MYPHAM; User ID = sa; Password = 123";
            return new SqlConnection(strConn);
        }
        protected SqlConnection conn;
        protected SqlCommand sqlCmd;
        protected SqlDataReader rs;
        public Data()
        {
            conn = new SqlConnection(strConn);
            sqlCmd = new SqlCommand();
        }
        public void Open()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }
        public void Close()
        {
            sqlCmd.Parameters.Clear();
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        public DataTable getTable(String sql)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = getConnect();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public void ExcuteNonQuery(String sql)
        {
            SqlConnection conn = getConnect();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.Clone();
            conn.Close();
        }
        public int ExcuteScalar(String sql)
        {
            SqlConnection conn = getConnect();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(sql, conn);
            int kq = int.Parse(cmd.ExecuteScalar().ToString());
            conn.Close();
            return kq;
        }
      
    }
}
