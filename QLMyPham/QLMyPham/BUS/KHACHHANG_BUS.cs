using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using QLMyPham.DAL;
using QLMyPham.DTO;
using System.Windows.Forms;

namespace QLMyPham.BUS
{
    class KHACHHANG_BUS
    {
        Data da = new Data();
        public DataTable getKH()
        {
            DataTable dt = null;
            String sql = "SELECT * FROM KHACHHANG";
            dt = da.getTable(sql);
            return dt;
        }

        public bool kiemtraKC(string MAKH)
        {
            string sql = "SELECT  count (*) from  KHACHHANG WHERE MAKH = '" + MAKH + "'";

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
        public bool kiemtraKN(string MAKH)
        {
            string sql = "SELECT  count (*) from  HOADON WHERE MAKH = '" + MAKH + "'";

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
        public void insertKH(string MAKH,int DIEMTICHLUY)
        {
            string sql = " insert into KHACHHANG values ('" + MAKH + "','" + DIEMTICHLUY + "')";
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
        public void insertKHMacDinh(string MAKH)
        {
            string sql = " insert into KHACHHANG values ('" + MAKH + "','0')";
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
        public void updateKH(string MAKH, int DIEMTICHLUY)
        {
            String sql = "UPDATE KHACHHANG set DIEMTICHLUY='" + DIEMTICHLUY + "' where MAKH='" + MAKH + "'";
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
        public void deleteKH(string MAKH)
        {
            String sql = "delete KHACHHANG where  MAKH='" + MAKH + "'";
            try
            {
                da.ExcuteNonQuery(sql);
                MessageBox.Show("Xóa thành công !");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi CSDL !" + ex.Message);

            }
        }
        public DataTable getdtl(string MAKH)
        {
            DataTable dt = null;
            String sql = "SELECT DIEMTICHLUY FROM KHACHHANG where  MAKH='" + MAKH + "'";
            dt = da.getTable(sql);
            return dt;
        }
    }
}
