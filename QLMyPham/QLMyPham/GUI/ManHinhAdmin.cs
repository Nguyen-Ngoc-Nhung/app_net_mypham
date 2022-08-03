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
    public partial class ManHinhAdmin : Form
    {
        String NAME = DangNhap.Name_user;
        public ManHinhAdmin()
        {
            InitializeComponent();
        }
        private void toolStripMenuItem59_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            toolStripStatusLabel6.Text = DateTime.Now.ToString();
        }

        private void toolStripMenuItem58_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                DangNhap frmmhc = new DangNhap();
                frmmhc.FormClosed += new FormClosedEventHandler(frmmhc_Closed);
                frmmhc.Show();
                this.Hide();
            }    
        }

        private void frmmhc_Closed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        private void ManHinhAdmin_Load(object sender, EventArgs e)
        {
           label3.Text = NAME;
        }
        private Form formCheck(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == ftype)
                {
                    return f;
                }
            }
            return null;
        }
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form frm = formCheck(typeof(ThongTinTK));
            if (frm == null)
            {
                ThongTinTK f = new ThongTinTK();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                frm.Activate();
            }
        }

      
        private void toolStripMenuItem61_Click(object sender, EventArgs e)
        {
            Form frm = formCheck(typeof(NhanVien));
            if (frm == null)
            {
                NhanVien f = new NhanVien();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                frm.Activate();
            }
        }

        private void toolStripMenuItem66_Click(object sender, EventArgs e)
        {
            Form frm = formCheck(typeof(KhachHang));
            if (frm == null)
            {
                KhachHang f = new KhachHang();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                frm.Activate();
            }
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = formCheck(typeof(HoaDon));
            if (frm == null)
            {
                HoaDon f = new HoaDon();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                frm.Activate();
            }
        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = formCheck(typeof(DanhMuc));
            if (frm == null)
            {
                DanhMuc f = new DanhMuc();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                frm.Activate();
            }
        }

        private void toolStripMenuItem72_Click(object sender, EventArgs e)
        {
            Form frm = formCheck(typeof(GioiThieu));
            if (frm == null)
            {
                GioiThieu f = new GioiThieu();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                frm.Activate();
            }
        }

        private void toolStripMenuItem67_Click(object sender, EventArgs e)
        {
            Form frm = formCheck(typeof(SanPham));
            if (frm == null)
            {
                SanPham f = new SanPham();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                frm.Activate();
            }
        }

        private void toolStripMenuItem69_Click(object sender, EventArgs e)
        {
            Form frm = formCheck(typeof(ThongKe));
            if (frm == null)
            {
                ThongKe f = new ThongKe();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                frm.Activate();
            }
        }

       
    }
}
