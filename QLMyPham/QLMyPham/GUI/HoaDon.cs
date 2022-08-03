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
    public partial class HoaDon : Form
    {
        private static int mAHD;

        public static int MAHD
        {
            get { return HoaDon.mAHD; }
            set { HoaDon.mAHD = value; }
        }
        KHACHHANG_BUS kh = new KHACHHANG_BUS();
        NHANVIEN_BUS NV = new NHANVIEN_BUS();
        public HoaDon()
        {
            InitializeComponent();
        }
        HOADON_BUS hd = new HOADON_BUS();
        private void HoaDon_Load(object sender, EventArgs e)
        {
            dtv_HD.DataSource = hd.getHD();
            MAKH.DisplayMember = "MAKH";
            MAKH.ValueMember = "MAKH";
            MAKH.DataSource = kh.getKH();
            cb_manv.DisplayMember = "MANV";
            cb_manv.ValueMember = "MANV";
            cb_manv.DataSource = NV.getNV();

        }
        private void dtv_HD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                DataGridViewRow row = this.dtv_HD.Rows[e.RowIndex];
                //Đưa dữ liệu vào 
                MAHD=int.Parse(row.Cells[1].Value.ToString());
                ChiTietHoaDon childForm = new ChiTietHoaDon();
                childForm.MdiParent = this.ParentForm;
                childForm.Dock = DockStyle.Fill;
                childForm.BringToFront();
                childForm.Show();
            }
        }
        private void txtmaHD_MaskChanged(object sender, EventArgs e)
        {
            if (txtmaHD.Text.Length > 0)
            {
                char phantucuoi = txtmaHD.Text[txtmaHD.Text.Length - 1];
                if (char.IsDigit(phantucuoi) == false)
                {
                    MessageBox.Show("Bạn nhập không phải số");
                    txtmaHD.Text = "";
                    txtmaHD.Focus();
                }
            }
        }

        private void txt_THHTOAN_TextChanged(object sender, EventArgs e)
        {
            if (txt_THHTOAN.Text.Length > 0)
            {
                char phantucuoi = txt_THHTOAN.Text[txt_THHTOAN.Text.Length - 1];
                if (char.IsDigit(phantucuoi) == false)
                {
                    MessageBox.Show("Bạn nhập không phải số");
                    txt_THHTOAN.Text = "";
                    txt_THHTOAN.Focus();
                }
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txt_THHTOAN.Text, System.Globalization.NumberStyles.AllowThousands);
                txt_THHTOAN.Text = String.Format(culture, "{0:#,##0}", value);
                txt_THHTOAN.Select(txt_THHTOAN.Text.Length, 0);
            }
        }
        private static string time;//phương thức ngày giờ tạo hóa đơn

        public static string Time
        {
            get { return HoaDon.time; }
            set { HoaDon.time = value; }
        }
        //lấy thời gian hiện tại 
        
        private void btn_add_Click(object sender, EventArgs e)
        {
            time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000");
            if (cb_manv.Text.Length == 0 || MAKH.Text.Length == 0 || CB.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi thêm!");
                return;
            }
            else
            {
                hd.insertHD(Time, CB.Text, cb_manv.Text, MAKH.Text);

                System.Data.DataTable dt = new System.Data.DataTable();
                try
                {
                    dt = hd.getMaHD(Time, cb_manv.Text, MAKH.Text);

                    if (dt.Rows.Count > 0)
                    {
                        mAHD = int.Parse(dt.Rows[0][0].ToString());
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Bạn đã nhập sai cú pháp");
                }
                ChiTietHoaDon childForm = new ChiTietHoaDon();
                childForm.MdiParent = this.ParentForm;
                childForm.Dock = DockStyle.Fill;
                childForm.BringToFront();
                childForm.Show();
                dtv_HD.DataSource = hd.getHD();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa?", "Thông báo",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2) ==

                            System.Windows.Forms.DialogResult.Yes)
            {
                if (txtmaHD.Text == string.Empty)
                {
                    MessageBox.Show("Mã hóa đơn không được rỗng");
                    return;
                }
                if (hd.kiemtraKN(int.Parse(txtmaHD.Text))==false)
                {
                    MessageBox.Show("Mã hóa đơn này có trên chi tiết hóa đơn");
                    return;
                }
                else
                {
                    hd.deleteHD(int.Parse(txtmaHD.Text));
                    dtv_HD.DataSource = hd.getHD();
                }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (cb_manv.Text.Length == 0 || MAKH.Text.Length == 0 || CB.Text.Length == 0 || txtmaHD.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi sửa!");
                return;
            }
            if (hd.kiemtraKC(int.Parse(txtmaHD.Text)) == true)
            {
                MessageBox.Show("Mã hóa đơn này ko tồn tại");
                return;
            }
            else
            {
                hd.updateHD(Time, CB.Text, cb_manv.Text, MAKH.Text, int.Parse(txtmaHD.Text));
                dtv_HD.DataSource = hd.getHD();
            }
        }

        private void dtv_HD_SelectionChanged(object sender, EventArgs e)
        {
            //Hiển thị thông tin tƣơng ứng lên các textbox
            txtmaHD.Text = dtv_HD.CurrentRow.Cells[1].Value.ToString();
            txt_THHTOAN.Text = dtv_HD.CurrentRow.Cells[3].Value.ToString();
            CB.Text = dtv_HD.CurrentRow.Cells[4].Value.ToString();
            MAKH.Text = dtv_HD.CurrentRow.Cells[5].Value.ToString();
            cb_manv.Text = dtv_HD.CurrentRow.Cells[6].Value.ToString();
        }

    }
}
