using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using QLMyPham.DAL;
using QLMyPham.DTO;
using System.Windows;
namespace QLMyPham.BUS
{
 public   class SANPHAM_BUS : Data
    {
        SANPHAM_DTO sp=new SANPHAM_DTO();
        Data da = new Data();
        public DataTable getSP()
        {
            DataTable dt = null;
            String sql = "SELECT MASP, TENSP,DONGIA FROM SANPHAM";
            dt = da.getTable(sql);
            return dt;
        }


        public bool kiemtraKC(int MASP)
        {
            string sql = "SELECT  count (*) from  SANPHAM WHERE MASP = '" + MASP + "'";

            try
            {
                int kq = (int)da.ExcuteScalar(sql);//lay gia tri do mang ve chuuong trinh
                if (kq > 0)
                {
                    return false;
                }
                else return true;

            }
            catch (SqlException ex)
            {
                return false;
                MessageBox.Show(ex.Message);
            }
        }
        public bool kiemtraKN(int MASP)
        {
            string sql = "SELECT  count (*) from  HOADON WHERE MASP = '" + MASP + "'";

            try
            {
                int kq = (int)da.ExcuteScalar(sql);//lay gia tri do mang ve chuuong trinh
                if (kq > 0)
                {
                    return false;
                }
                else return true;

            }
            catch (SqlException ex)
            {
                return false;
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable getSANPHAMhethang()
        {
            DataTable dt = null;
            String sql = "SELECT MASP,TENSP,SOLUONG FROM SANPHAM WHERE SOLUONG < 10";
            dt = da.getTable(sql);
            return dt;
        }
        public DataTable getSANPHAM()
        {
            DataTable dt = null;
            String sql = "SELECT MASP,TENSP,MOTA,SOLUONG,DONGIA,NSX,MALSP FROM SANPHAM";
            dt = da.getTable(sql);
            return dt;
        }
        public DataTable Search(String condi)
        {
            DataTable dt = null;
            String sql = "Select MASP, TENSP,DONGIA from SANPHAM where TENSP like N'%" + condi + "%'";
            dt = da.getTable(sql);
            return dt;
        }
        public DataTable SearchSP(String condi)
        {
            DataTable dt = null;
            String sql = "Select * from SANPHAM where TENSP like N'%" + condi + "%'";
            dt = da.getTable(sql);
            return dt;
        }
        public void insertSP(string TENSP, string MOTA, int SOLUONG, float DONGIA, string NSX, string MALSP)
        {
            string sql = "INSERT INTO SANPHAM VALUES(N'" + TENSP + "',N'" + MOTA + "','" + SOLUONG + "','" + DONGIA + "',N'" +
                NSX + "',N'" + MALSP + "')";
            try
            {
                da.ExcuteNonQuery(sql);
                MessageBox.Show("Thêm thành công !");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Thêm thất bại !");
                MessageBox.Show(ex.Message);
            }
        }
        public void updateSP(int MASP, string TENSP, string MOTA, int SOLUONG, float DONGIA, string NSX, string MALSP)
        {
            string sql = "UPDATE SANPHAM SET TENSP=N'" + TENSP + "',MOTA='" + MOTA +
                "',SOLUONG='" + SOLUONG + "',DONGIA='" + DONGIA + "',NSX='" + NSX +
                "',MALSP='" + MALSP + "' WHERE MASP='" + MASP + "'";
            try
            {
                da.ExcuteNonQuery(sql);
                MessageBox.Show("Sửa thành công !");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Sửa thất bại !");
                MessageBox.Show(ex.Message);
            }
        }
        public void deleteSP(int MASP)
        {
            string sql = "DELETE SANPHAM WHERE MASP='" + MASP + "' ";
            try
            {
                da.ExcuteNonQuery(sql);
                MessageBox.Show("Xóa thành công !");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Xóa thất bại ");
                MessageBox.Show(ex.Message);
            }
        }


        public List<SANPHAM_DTO> GetSPBanChay (int? nam)
        {
            List<SANPHAM_DTO> lstNV = new List<SANPHAM_DTO>();

            try
            {
                this.Open();

                string strSql = "sp_ChartSanPham";
                this.sqlCmd.CommandText = strSql;
                this.sqlCmd.Connection = this.conn;
                this.sqlCmd.CommandType = CommandType.StoredProcedure;

                this.sqlCmd.Parameters.AddWithValue("@nam", nam);

                SqlDataReader rd = sqlCmd.ExecuteReader();

                while (rd.Read())
                {
                    lstNV.Add(new SANPHAM_DTO
                    {
                        TENSP = rd["TENSP"].ToString(),
                        SOLUONG = int.Parse(rd["SOLUONGBANRA"].ToString())
                    });
                }

                this.Close(); // đóng kết nối
            }
            catch (Exception)
            {
                return null;
            }

            return lstNV;
        }

    }
}