using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLMyPham.GUI;
namespace QLMyPham
{
    public partial class ManHinhNV : Form
    {
        public ManHinhNV()
        {
            InitializeComponent();
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
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

        String NAME = DangNhap.Name_user;
        private void ManHinhNV_Load(object sender, EventArgs e)
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

        private void bánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = formCheck(typeof(BanHang));
            if (frm == null)
            {
                BanHang f = new BanHang();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                frm.Activate();
            }
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void hàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
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

      
    }
}
