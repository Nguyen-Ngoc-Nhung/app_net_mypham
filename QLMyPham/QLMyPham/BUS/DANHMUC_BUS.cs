using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLMyPham.DAL;
using QLMyPham.DTO;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace QLMyPham.BUS
{
    class DANHMUC_BUS
    {
        Data da = new Data();
        public DataTable getDM()
        {
            DataTable dt = null;
            String sql = "SELECT * FROM DANHMUC";
            dt = da.getTable(sql);
            return dt;
        }

        public bool kiemtraKC(int MADM)
        {
            string sql = "SELECT  count (*) from  DANHMUC WHERE MADM = '" + MADM + "'";

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
        public bool kiemtraKN(int MADM)
        {
            string sql = "SELECT  count (*) from  CHITIETDANHMUC WHERE MADM = '" + MADM + "'";

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
        public void insertDM(string TENDANHMUC)
        {
            string sql = "INSERT INTO DANHMUC VALUES(N'" + TENDANHMUC + "')";
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
        public void updateDM(int MADM, string TENDANHMUC)
        {
            string sql = "UPDATE DANHMUC SET TENDANHMUC=N'" + TENDANHMUC + "' WHERE MADM='" + MADM + "'";
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
        public void deleteDM(int MADM, string TENDANHMUC)
        {
            string sql = "DELETE DANHMUC WHERE MADM='" + MADM + "'AND TENDANHMUC=N'" + TENDANHMUC + "' ";
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
    }
}
