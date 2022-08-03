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
using System.Globalization;

namespace QLMyPham.BUS
{
    class NHANVIEN_BUS :Data
    {
        Data da = new Data();
        public DataTable getNV()
        {
            DataTable dt = null;
            String sql = "select MANV,HOTEN,NGSINH,GTINH,NGTAO,EMAIL,SDT,DCHI,TAIKHOAN.MATK,TENTK,TENQUYEN FROM QUYEN,THONGTINTAIKHOAN,NHANVIEN,TAIKHOAN WHERE QUYEN.MAQUYEN=TAIKHOAN.MAQUYEN AND THONGTINTAIKHOAN.MATK=NHANVIEN.MATK AND TAIKHOAN.MATK=NHANVIEN.MATK AND TINHTRANG = N'Đang Làm'";
            dt = da.getTable(sql);
            return dt;
        }
        public bool kiemtraTrungemail(string thuoctinh, string table)
        {
            string sql = "SELECT  count (*) from  " + table + " WHERE EMAIL = '" + thuoctinh + "'";

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
        public bool kiemtraTrungSDT(string thuoctinh, string table)
        {
            string sql = "SELECT  count (*) from  " + table + " WHERE SDT = '" + thuoctinh + "'";

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
        public bool kiemtraTrungTENTK(string thuoctinh, string table)
        {
            string sql = "SELECT  count (*) from  " + table + " WHERE TENTK = '" + thuoctinh + "'";

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
        public bool kiemtraKCTK(string MATK)
        {
            string sql = "SELECT  count (*) from  TAIKHOAN WHERE MATK = '" + MATK + "'";

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

        public bool kiemtraKCNV(string MANV)
        {
            string sql = "SELECT  count (*) from  NHANVIEN WHERE MANV = '" + MANV + "'";

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
        public void insertNV(string MATK, string TENTK, string HOTEN, string NGSINH, string EMAIL, string DCHI, string GTINH, string SDT,string MANV)
        {
            string sql = "insert into TAIKHOAN VALUES ('" + MATK + "','" + TENTK + "','202cb962ac5975b964b7152d234b70','2'); WAITFOR DELAY '00:00:03';  INSERT INTO THONGTINTAIKHOAN VALUES ('" + MATK + "',N'" + HOTEN + "','" + NGSINH + "',N'" + GTINH + "','" + DateTime.Now.ToString() + "','" + EMAIL + "','" + SDT + "',N'" + DCHI + "',N'Đang Làm'); INSERT INTO NHANVIEN VALUES ('" + MANV + "','" + MATK + "')";
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
        public void updateNV(string HOTEN, string NGSINH, string EMAIL, string DCHI, string GTINH, string SDT, string MANV)
        {
            string sql = "SET DATEFORMAT dmy; update THONGTINTAIKHOAN set HOTEN =N'" + HOTEN + "',GTINH=N'" + GTINH + "',NGSINH='" + NGSINH + "',SDT='" + SDT + "',DCHI=N'" + DCHI + "',EMAIL='" + EMAIL + "' where MATK=(select MATK from NHANVIEN where MANV='" + MANV + "')";
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
        public void capnhatnv(string HOTEN, string NGSINH, string EMAIL, string DCHI, string GTINH, string SDT, string matk)
        {
            string sql = "SET DATEFORMAT dmy; update THONGTINTAIKHOAN set HOTEN =N'" + HOTEN + "',GTINH=N'" + GTINH + "',NGSINH='" + NGSINH + "',SDT='" + SDT + "',DCHI=N'" + DCHI + "',EMAIL='" + EMAIL + "' where MATK='" + matk + "'";
            try
            {
                da.ExcuteNonQuery(sql);
                MessageBox.Show("Sửa thành công !\r\n Mời bạn đăng nhập lại để xem thông tin");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Sửa thất bại !");
                MessageBox.Show(ex.Message);
            }
        }
        public void updateMATKHAU(string MATK)
        {
            String sql = "UPDATE TAIKHOAN SET MATKHAU ='202cb962ac5975b964b7152d234b70' WHERE MATK='" + MATK + "'";
            try
            {
                da.ExcuteNonQuery(sql);
                MessageBox.Show("Mật khẩu hiện tại là 123 ");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Cài mật khẩu thất bại !");
                MessageBox.Show(ex.Message);
            }
        }

        public void deleteNV(string MATK)
        {
            String sql = "UPDATE THONGTINTAIKHOAN set TINHTRANG=N'Đã nghỉ việc' WHERE MATK='" + MATK + "'";
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
        public DataTable Search(String condi)
        {
            DataTable dt = null;
            String sql = "select MANV,HOTEN,NGSINH,GTINH,NGTAO,EMAIL,SDT,DCHI,TAIKHOAN.MATK,TENTK,TENQUYEN FROM QUYEN,THONGTINTAIKHOAN,NHANVIEN,TAIKHOAN WHERE QUYEN.MAQUYEN=TAIKHOAN.MAQUYEN AND THONGTINTAIKHOAN.MATK=NHANVIEN.MATK AND TAIKHOAN.MATK=NHANVIEN.MATK AND TINHTRANG = N'Đang Làm' AND HOTEN like N'%" + condi + "%'";
            dt = da.getTable(sql);
            return dt;
        }
        public List<NHANVIEN_DTO> GetDoanhThu(int? nam)
        {
            List<NHANVIEN_DTO> lstNV = new List<NHANVIEN_DTO>();

            try
            {
                this.Open();

                string strSql = "sp_ChartNhanVien";
                this.sqlCmd.CommandText = strSql;
                this.sqlCmd.Connection = this.conn;
                this.sqlCmd.CommandType = CommandType.StoredProcedure;

                this.sqlCmd.Parameters.AddWithValue("@nam", nam);

                SqlDataReader rd = sqlCmd.ExecuteReader();

                while (rd.Read())
                {
                    TextInfo textInfo = new CultureInfo("vi", false).TextInfo;

                    lstNV.Add(new NHANVIEN_DTO
                    {
                        HoTen = textInfo.ToTitleCase(rd["HOTEN"].ToString().ToLower()),
                        DoanhThu = double.Parse(rd["DOANHTHU"].ToString())
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

