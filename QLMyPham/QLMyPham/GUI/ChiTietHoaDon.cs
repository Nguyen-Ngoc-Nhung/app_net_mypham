using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLMyPham.BUS;
namespace QLMyPham.GUI
{
    public partial class ChiTietHoaDon : Form
    {
        int MAHD = HoaDon.MAHD;
        public ChiTietHoaDon()
        {
            InitializeComponent();
        }
        CTHD_BUS CT = new CTHD_BUS();
        SANPHAM_BUS sp = new SANPHAM_BUS();
        private void ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LBLMA.Text = MAHD.ToString();
            txt_msp.DisplayMember = "MASP";
            txt_msp.ValueMember = "MASP";
            txt_msp.DataSource = sp.getSP();
            dataGridView1.DataSource = sp.getSP();
            dtv_CTHD.DataSource = CT.getCTHD(MAHD);
        }
        private void txt_msp_TextChanged(object sender, EventArgs e)
        {
            if (txt_msp.Text.Length > 0)
            {
                char phantucuoi = txt_msp.Text[txt_msp.Text.Length - 1];
                if (char.IsDigit(phantucuoi) == false)
                {
                    MessageBox.Show("Bạn nhập không phải số");
                    txt_msp.Text = "";
                    txt_msp.Focus();
                }
            }
        }

        private void txt_sl_MaskChanged(object sender, EventArgs e)
        {
            if (txt_sl.Text.Length > 0)
            {
                char phantucuoi = txt_sl.Text[txt_sl.Text.Length - 1];
                if (char.IsDigit(phantucuoi) == false)
                {
                    MessageBox.Show("Bạn nhập không phải số");
                    txt_sl.Text = "";
                    txt_sl.Focus();
                }
            }
        }


        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt_msp.Text.Length == 0 || txt_sl.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi thêm!");
                return;
            }
            if (int.Parse(txt_sl.Text) <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng lớn hơn 0");
                return;
            }
            if (CT.kiemtraKC(MAHD, int.Parse(txt_msp.Text)) == false)
            {
                MessageBox.Show("Sản phẩm đã có trong hóa đơn!");
                return;
            }
            else
            {
                CT.insertCTHD(MAHD, int.Parse(txt_msp.Text), int.Parse(txt_sl.Text));
                dtv_CTHD.DataSource = CT.getCTHD(MAHD);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn muốn xóa?", "Thông báo",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                             MessageBoxDefaultButton.Button2) ==

                             System.Windows.Forms.DialogResult.Yes)
            {
                if (txt_msp.Text == string.Empty)
                {
                    MessageBox.Show("Mã sản phẩm không được rỗng");
                    return;
                }
                else
                {
                    CT.deletectHD(int.Parse(txt_msp.Text));
                    dtv_CTHD.DataSource = CT.getCTHD(MAHD);
                }

            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_msp.Text.Length == 0 || txt_sl.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (CT.kiemtraKC(MAHD, int.Parse(txt_msp.Text)) == true)
            {
                MessageBox.Show("Sản phẩm không có trong hóa đơn!");
                return;
            }
            else
            {
                CT.updateHD(MAHD, int.Parse(txt_msp.Text), int.Parse(txt_sl.Text));
                dtv_CTHD.DataSource = CT.getCTHD(MAHD);
            }
        }

        private void vbButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa tất cả?", "Thông báo",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                           MessageBoxDefaultButton.Button2) ==

                           System.Windows.Forms.DialogResult.Yes)
            {

                CT.deleteHD(MAHD);
                dtv_CTHD.DataSource = CT.getCTHD(MAHD);
            }
        }

        private void vbButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sp.Search(txtSearch.Text);
        }
    }
}
