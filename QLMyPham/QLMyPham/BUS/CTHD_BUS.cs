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
    class CTHD_BUS
    {
        Data da = new Data();
        public DataTable getCTHD(int MAHD)
        {
            DataTable dt = null;
            String sql = "SELECT TENSP,CHITIETHD.SOLUONG FROM CHITIETHD ,SANPHAM WHERE SANPHAM.MASP=CHITIETHD.MASP AND MAHD='"+MAHD+"'";
            dt = da.getTable(sql);
            return dt;
        }
       
        public bool kiemtraKC(int MAHD, int MASP)
        {
            string sql = "SELECT  count (*) from  CHITIETHD WHERE MAHD = '"+MAHD+"' AND MASP ='"+MASP+"'";
            
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
        public void insertCTHD(int MAHD, int MASP,int SOLUONG)
        {
            string sql = " insert into CHITIETHD values ('" + MAHD + "','" + MASP + "','"+SOLUONG+"')";
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
        public void updateHD(int MAHD, int MASP, int SOLUONG)
        {
            String sql = "UPDATE CHITIETHD set MASP='" + MASP + "' , SOLUONG='" + SOLUONG + "' where MAHD='" + MAHD + "'";
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
        public void deleteHD(int MAHD)
        {
            String sql = "delete CHITIETHD where MAHD='" + MAHD + "'";
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
        public void deletectHD(int MASP)
        {
            String sql = "delete CHITIETHD where MASP='" + MASP + "'";
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
    }
}
