using QLMyPham.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLMyPham
{
    public partial class ThongTinTK : Form
    {
        string name = DangNhap.Name_user;
        string ngsinh = DangNhap.Ngsinh;
        string ngtao = DangNhap.Ngtao;
        string diachi = DangNhap.Diachi;
        string sdt = DangNhap.Sdt;
        string email = DangNhap.Email;
        string gioitinh = DangNhap.Gioitinh;
        public ThongTinTK()
        {
            InitializeComponent();
        }

        private void ThongTinTK_Load(object sender, EventArgs e)
        {
            lblDC.Text = diachi;
            lblEmail.Text = email;
            lblHoTen.Text = name;
            lblNgaySinh.Text = ngsinh;
            lblNVL.Text = ngtao;
            lblGioiTinh.Text = gioitinh;
            lblSDT.Text = sdt;
        }


        private void vbButton2_Click(object sender, EventArgs e)
        {
            DoiMatKhau MK = new DoiMatKhau();
            MK.ShowDialog();
        }

        private void vbButton1_Click(object sender, EventArgs e)
        {
            CapNhatTTCN childForm = new CapNhatTTCN();
            childForm.ShowDialog();
        }
    }
}
