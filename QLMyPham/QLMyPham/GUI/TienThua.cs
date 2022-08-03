using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLMyPham.GUI
{
    public partial class TienThua : Form
    {
        public TienThua()
        {
            InitializeComponent();
        }
        float giaTien = float.Parse(BanHang.Bh_thanhtoan);
        string giamgia = BanHang.Bh_giamgia;
        string tongtien = BanHang.Bh_tongtien;
        float tienkhach = TienKhach.Val;
        private void TienThua_Load(object sender, EventArgs e)
        {
            float a = tienkhach - giaTien;
            lblTienThua.Text = string.Format("{0:#,##0} VNĐ",a);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
