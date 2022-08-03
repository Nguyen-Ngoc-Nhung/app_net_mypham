using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QLMyPham.BUS;
using System.Security.Cryptography;

namespace QLMyPham
{
    public partial class DangNhap : Form
    {
        private static string name_user;//họ tên tài khoản

        private static string pass;//mật khẩu
        private static string user;//tên tài khoản

        public static string User
        {
            get { return DangNhap.user; }
            set { DangNhap.user = value; }
        }
        public static string Pass
        {
            get { return DangNhap.pass; }
            set { DangNhap.pass = value; }
        }
        private static string diachi, gioitinh, ngsinh, ngtao, email, sdt;
        private static string manv;

        public static string Manv
        {
            get { return DangNhap.manv; }
            set { DangNhap.manv = value; }
        }
        public static string Sdt
        {
            get { return DangNhap.sdt; }
            set { DangNhap.sdt = value; }
        }

        public static string Email
        {
            get { return DangNhap.email; }
            set { DangNhap.email = value; }
        }

        public static string Ngtao
        {
            get { return DangNhap.ngtao; }
            set { DangNhap.ngtao = value; }
        }

        public static string Ngsinh
        {
            get { return DangNhap.ngsinh; }
            set { DangNhap.ngsinh = value; }
        }

        public static string Gioitinh
        {
            get { return DangNhap.gioitinh; }
            set { DangNhap.gioitinh = value; }
        }

        public static string Diachi
        {
            get { return DangNhap.diachi; }
            set { DangNhap.diachi = value; }
        }

        public static string Name_user
        {
            get { return DangNhap.name_user; }
            set { DangNhap.name_user = value; }

        }
        private static string matk;

        public static string Matk
        {
            get { return DangNhap.matk; }
            set { DangNhap.matk = value; }
        }
        private StringBuilder encrypt(String str)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] hash = md5.ComputeHash(inputBytes);
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x"));
            }
            return sb;
        }

        public DangNhap()
        {
            InitializeComponent();
        }
        TAIKHOAN_BUS TK = new TAIKHOAN_BUS();
        int dem = 0;
        private void btnDangnhap_Click(object sender, EventArgs e)
        {
          
        }
        private void frmmhc_Closed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void vbButton2_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập \r\nVui lòng nhập!", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Focus();
                return;
            }
            if (txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu \r\nVui lòng nhập!", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            try
            {
                dt = TK.getTKAD(txtUsername.Text.ToUpper(), encrypt(txtPassword.Text.ToString()).ToString());

                if (dt.Rows.Count > 0)
                {
                    matk = dt.Rows[0][0].ToString();
                    dt2 = TK.tentk(txtUsername.Text.ToUpper());
                    user = txtUsername.Text.ToUpper();
                    pass = encrypt(txtPassword.Text.ToString()).ToString();
                    name_user = dt2.Rows[0][0].ToString();
                    gioitinh = dt2.Rows[0][1].ToString();
                    ngsinh = dt2.Rows[0][2].ToString();
                    ngtao = dt2.Rows[0][3].ToString();
                    email = dt2.Rows[0][4].ToString();
                    sdt = dt2.Rows[0][5].ToString();
                    diachi = dt2.Rows[0][6].ToString();
                    MessageBox.Show("Đăng nhập thành công !");
                    ManHinhAdmin frmmhc = new ManHinhAdmin();
                    frmmhc.FormClosed += new FormClosedEventHandler(frmmhc_Closed);
                    frmmhc.Show();
                    this.Hide();
                }
                else
                {
                    dt1 = TK.getTKNV(txtUsername.Text.ToUpper(), encrypt(txtPassword.Text.ToString()).ToString());
                    if (dt1.Rows.Count > 0)
                    {
                        matk = dt1.Rows[0][0].ToString();
                        dt2 = TK.tentk(txtUsername.Text.ToUpper());
                        user = txtUsername.Text.ToUpper();
                        pass = encrypt(txtPassword.Text.ToString()).ToString();
                        name_user = dt2.Rows[0][0].ToString();
                        gioitinh = dt2.Rows[0][1].ToString();
                        ngsinh = dt2.Rows[0][2].ToString();
                        ngtao = dt2.Rows[0][3].ToString();
                        email = dt2.Rows[0][4].ToString();
                        sdt = dt2.Rows[0][5].ToString();
                        diachi = dt2.Rows[0][6].ToString();
                        dt3 = TK.GETMANV(txtUsername.Text.ToUpper());
                        manv = dt3.Rows[0][0].ToString();
                        MessageBox.Show("Đăng nhập thành công !");
                        ManHinhNV frmmhc = new ManHinhNV();
                        frmmhc.FormClosed += new FormClosedEventHandler(frmmhc_Closed);
                        frmmhc.Show();
                        this.Hide();
                    }
                    else
                    {
                        dem++;
                        MessageBox.Show("Đăng nhập thất bại,mời bạn nhập lại");
                        if (dem == 3)
                        {
                            MessageBox.Show("Bạn đã nhập sai 3 lần , mời bạn liên hệ quản lý reset mật khẩu !");
                            return;
                        }
                    }
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Bạn đã nhập sai cú pháp");
            }
            catch (SqlException)
            {
                MessageBox.Show("Lỗi kết nối CSDL !");
            }
        }



    }
}
