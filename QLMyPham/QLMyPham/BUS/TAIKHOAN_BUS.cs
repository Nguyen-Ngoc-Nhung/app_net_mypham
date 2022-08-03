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
    class TAIKHOAN_BUS
    {
        TAIKHOAN_DTO TK = new TAIKHOAN_DTO();
        Data da = new Data();
        public DataTable getTKAD(String TENTK,String MK)
        {
            DataTable dt = null;
            String sql = "select * from TAIKHOAN,THONGTINTAIKHOAN WHERE THONGTINTAIKHOAN.MATK=TAIKHOAN.MATK AND TINHTRANG=N'Đang Làm' AND TENTK = '" + TENTK + "'and MATKHAU = '" + MK + "' AND MAQUYEN=1";
            dt = da.getTable(sql);
            return dt;
        }
        public DataTable getTKNV(String TENTK, String MK)
        {
            DataTable dt = null;
            String sql = "select * from TAIKHOAN,THONGTINTAIKHOAN WHERE THONGTINTAIKHOAN.MATK=TAIKHOAN.MATK AND TINHTRANG=N'Đang Làm' AND TENTK = '" + TENTK + "'and MATKHAU = '" + MK + "' AND MAQUYEN=2";
            dt = da.getTable(sql);
            return dt;
        }
        public DataTable tentk(string name)
        {
            DataTable dt = null;
            string sql = "SELECT HOTEN,GTINH,NGSINH,NGTAO,EMAIL,SDT,DCHI FROM THONGTINTAIKHOAN,TAIKHOAN WHERE THONGTINTAIKHOAN.MATK=TAIKHOAN.MATK AND TENTK='" + name + "'";
            dt = da.getTable(sql);
            return dt;
        }
        public DataTable GETMANV(string name)
        {
            DataTable dt = null;
            string sql = "SELECT MANV FROM TAIKHOAN,NHANVIEN WHERE TAIKHOAN.MATK =NHANVIEN.MATK AND TENTK='" + name + "'";
            dt = da.getTable(sql);
            return dt;
        }
         public void updateMATKHAU(string TENTK,string PASS)
        {
            String sql = "UPDATE TAIKHOAN SET MATKHAU ='"+PASS+"' WHERE TENTK='" + TENTK + "'";
            try
            {
                da.ExcuteNonQuery(sql);
                MessageBox.Show("Đổi mật khẩu thành công.!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Đổi mật khẩu thất bại.!");
                MessageBox.Show(ex.Message);
            }
        }
         
    }
}
