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
using System.Drawing.Imaging;
using System.IO;
namespace QLMyPham.GUI
{
    public partial class SanPham : Form
    {
        SANPHAM_BUS SP = new SANPHAM_BUS();
        public SanPham()
        {
            InitializeComponent();
        }
        private void SanPham_Load(object sender, EventArgs e)
        {
            dtv_SP.DataSource = SP.getSANPHAM();
        }

        private void dtv_SP_SelectionChanged(object sender, EventArgs e)
        {
            txtMaSP.Text = dtv_SP.CurrentRow.Cells[0].Value.ToString();
            txtTenSP.Text = dtv_SP.CurrentRow.Cells[1].Value.ToString();
            txtMoTa.Text = dtv_SP.CurrentRow.Cells[2].Value.ToString();
            txtSoLuong.Text = dtv_SP.CurrentRow.Cells[3].Value.ToString();
            txtDonGia.Text = dtv_SP.CurrentRow.Cells[4].Value.ToString();
            txtNSX.Text = dtv_SP.CurrentRow.Cells[5].Value.ToString();
            txtMaLSP.Text = dtv_SP.CurrentRow.Cells[6].Value.ToString();
        }

    

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa?", "Thông báo",
             MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
             MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                if (txtTenSP.Text.Length == 0 || txtMoTa.Text.Length == 0 ||
                 txtSoLuong.Text.Length == 0 || txtDonGia.Text.Length == 0 || txtNSX.Text.Length == 0 || txtMaLSP.Text.Length == 0)
                {
                    MessageBox.Show("Mã danh mục và tên danh mục không được rỗng!");
                    return;
                }
                else
                {
                    SP.deleteSP(int.Parse(txtMaSP.Text));
                    dtv_SP.DataSource = SP.getSANPHAM();
                }
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (txtTenSP.Text.Length == 0 || txtMoTa.Text.Length == 0 ||
            txtSoLuong.Text.Length == 0 || txtDonGia.Text.Length == 0 || txtNSX.Text.Length == 0 || txtMaLSP.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            }
            else
            {
                SP.updateSP(int.Parse(txtMaSP.Text), txtTenSP.Text, txtMoTa.Text, int.Parse(txtSoLuong.Text), float.Parse(txtDonGia.Text), txtNSX.Text, txtMaLSP.Text);
                dtv_SP.DataSource = SP.getSANPHAM();
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenSP.Text.Length == 0 || txtMoTa.Text.Length == 0 ||
               txtSoLuong.Text.Length == 0 || txtDonGia.Text.Length == 0 || txtNSX.Text.Length == 0 || txtMaLSP.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            }
            else
            {
                SP.insertSP(txtTenSP.Text, txtMoTa.Text, int.Parse(txtSoLuong.Text), float.Parse(txtDonGia.Text), txtNSX.Text, txtMaLSP.Text);
                dtv_SP.DataSource = SP.getSANPHAM();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtv_SP.DataSource = SP.SearchSP(txtTenSP.Text);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void buttonMyPham1_Click(object sender, EventArgs e)
        {
            HangSapHet childForm = new HangSapHet();
            childForm.MdiParent = this.ParentForm;
            childForm.Dock = DockStyle.Fill;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
