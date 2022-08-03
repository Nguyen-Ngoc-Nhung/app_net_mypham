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
using QLMyPham.DTO;
namespace QLMyPham.GUI
{
    public partial class DanhMuc : Form
    {
        DANHMUC_BUS DM = new DANHMUC_BUS();
        public DanhMuc()
        {
            InitializeComponent();
        }

        //private void btnThoat_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
        //        this.Close();
        //}

        private void dtv_DANHMUC_SelectionChanged(object sender, EventArgs e)
        {
            txtMaDM.Text = dtv_DANHMUC.CurrentRow.Cells[0].Value.ToString();
            txtTenDM.Text = dtv_DANHMUC.CurrentRow.Cells[1].Value.ToString();
        }

        private void DanhMuc_Load(object sender, EventArgs e)
        {
            dtv_DANHMUC.DataSource = DM.getDM();

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txtTenDM.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            else
            {
                DM.insertDM(txtTenDM.Text);
                dtv_DANHMUC.DataSource = DM.getDM();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa?", "Thông báo",
              MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
              MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                if (txtTenDM.Text.Length == 0 || txtMaDM.Text == string.Empty)
                {
                    MessageBox.Show("Mã danh mục và tên danh mục không được rỗng!");
                    return;
                }
                if (DM.kiemtraKN(int.Parse(txtMaDM.Text)) == false)
                {
                    MessageBox.Show("Mã danh mục này tồn tại trên loại sản phẩm!");
                    return;
                }
                else
                {
                    DM.deleteDM(int.Parse(txtMaDM.Text), txtTenDM.Text);
                    dtv_DANHMUC.DataSource = DM.getDM();
                }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txtTenDM.Text.Length == 0 || txtMaDM.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (DM.kiemtraKC(int.Parse(txtMaDM.Text)) == true)
            {
                MessageBox.Show("Mã danh mục ko tồn tại!");
                return;
            }
            else
            {
                DM.updateDM(int.Parse(txtMaDM.Text), txtTenDM.Text);
                dtv_DANHMUC.DataSource = DM.getDM();
            }
        }
    }
}
