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
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
namespace QLMyPham.GUI
{
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
        }
        public static bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        public bool IsValidVietNamPhoneNumber(string phoneNum)
        {
            if (string.IsNullOrEmpty(phoneNum))
                return false;
            string sMailPattern = @"^((09(\d){8})|(03(\d){8})|(08(\d){8})|(07(\d){8})|(05(\d){8}))$";
            return Regex.IsMatch(phoneNum.Trim(), sMailPattern);
        }

        NHANVIEN_BUS nv = new NHANVIEN_BUS();
        private void NHANVIEN_Load(object sender, EventArgs e)
        {
            dtv_nhanvien.DataSource = nv.getNV();
        }

        private void dtv_nhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                DataGridViewRow row = this.dtv_nhanvien.Rows[e.RowIndex];
                //Đưa dữ liệu vào 
                nv.updateMATKHAU(row.Cells[9].Value.ToString());

            }
        }

        private void dtv_nhanvien_SelectionChanged(object sender, EventArgs e)
        {
            //Hiển thị thông tin tƣơng ứng lên các textbox
            txt_mnv.Text = dtv_nhanvien.CurrentRow.Cells[1].Value.ToString();
            txt_tennv.Text = dtv_nhanvien.CurrentRow.Cells[2].Value.ToString();
            cb_gioitinh.Text = dtv_nhanvien.CurrentRow.Cells[4].Value.ToString();
            txt_sdt.Text = dtv_nhanvien.CurrentRow.Cells[7].Value.ToString();
            txt_diachi.Text = dtv_nhanvien.CurrentRow.Cells[8].Value.ToString();
            EMAIL.Text = dtv_nhanvien.CurrentRow.Cells[6].Value.ToString();
            MATK.Text = dtv_nhanvien.CurrentRow.Cells[9].Value.ToString();
            TENTK.Text = dtv_nhanvien.CurrentRow.Cells[10].Value.ToString();
        }


        private void ExportExcel(DataGridView dgv, string duongDan, string tenTap)
        {
            app obj = new app();
            obj.Application.Workbooks.Add(Type.Missing);
            obj.Columns.ColumnWidth = 25;

            // Lấy cái Header DataGridView
            for (int i = 1; i < dgv.Columns.Count + 1; i++)
            {
                obj.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
            }
            //
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (dgv.Rows[i].Cells[j].Value != null)
                    {
                        obj.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            obj.ActiveWorkbook.SaveCopyAs(duongDan + tenTap + ".xlsx");
            obj.ActiveWorkbook.Saved = true;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt_mnv.Text.Length == 0 || txt_tennv.Text.Length == 0 || txt_sdt.Text.Length == 0 || txt_diachi.Text.Length == 0 || cb_gioitinh.Text.Length == 0 || EMAIL.Text.Length == 0 || MATK.Text.Length == 0 || TENTK.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi thêm!");
                return;
            }
            if (isEmail(EMAIL.Text) == false)
            {
                MessageBox.Show("Email sai định dạng");
                return;
            }
            if (IsValidVietNamPhoneNumber(txt_sdt.Text) == false)
            {
                MessageBox.Show("Số điện thoại sai định dạng!");
                return;
            }
            if (nv.kiemtraKCNV(txt_mnv.Text) == false)
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại!");
                return;
            }
            if (nv.kiemtraKCTK(MATK.Text) == false)
            {
                MessageBox.Show("Mã tài khoản này đã tồn tại!");
                return;
            }
            if (nv.kiemtraTrungemail(EMAIL.Text, "THONGTINTAIKHOAN") == false)
            {
                MessageBox.Show("Email này đã tồn tại!");
                return;
            }
            if (nv.kiemtraTrungSDT(txt_sdt.Text, "THONGTINTAIKHOAN") == false)
            {
                MessageBox.Show("Số điện thoại này đã tồn tại!");
                return;
            }
            if (nv.kiemtraTrungTENTK(TENTK.Text, "TAIKHOAN") == false)
            {
                MessageBox.Show("Tên tài khoản này đã tồn tại!");
                return;
            }
            else
            {
                nv.insertNV(MATK.Text, TENTK.Text, txt_tennv.Text, dt_ngaysinh.Value.ToString(), EMAIL.Text, txt_diachi.Text, cb_gioitinh.Text, txt_sdt.Text, txt_mnv.Text);
                dtv_nhanvien.DataSource = nv.getNV();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa?", "Thông báo",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                             MessageBoxDefaultButton.Button2) ==

                             System.Windows.Forms.DialogResult.Yes)
            {
                if (MATK.Text == string.Empty)
                {
                    MessageBox.Show("Mã tài khoản không được rỗng");
                    return;
                }
                else
                {
                    nv.deleteNV(MATK.Text);
                    dtv_nhanvien.DataSource = nv.getNV();
                }
            }
        }

        private void vbButton2_Click(object sender, EventArgs e)
        {
            dtv_nhanvien.DataSource = nv.Search(txt_tennv.Text);
        }

        private void vbButton1_Click(object sender, EventArgs e)
        {
            ExportExcel(dtv_nhanvien, @"D:\", "nhanvien");
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_mnv.Text.Length == 0 || txt_tennv.Text.Length == 0 || txt_sdt.Text.Length == 0 || txt_diachi.Text.Length == 0 || cb_gioitinh.Text.Length == 0 || EMAIL.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi thêm!");
                return;
            }
            if (isEmail(EMAIL.Text) == false)
            {
                MessageBox.Show("Email sai định dạng");
                return;
            }
            if (IsValidVietNamPhoneNumber(txt_sdt.Text) == false)
            {
                MessageBox.Show("Số điện thoại sai định dạng!");
                return;
            }
            if (nv.kiemtraKCNV(txt_mnv.Text) == true)
            {
                MessageBox.Show("Mã nhân viên này ko tồn tại!");
                return;
            }
            if (nv.kiemtraKCTK(MATK.Text) == true)
            {
                MessageBox.Show("Mã tài khoản này ko tồn tại!");
                return;
            }
            else
            {
                nv.updateNV(txt_tennv.Text, dt_ngaysinh.Value.ToString("dd/MM/yyyy"), EMAIL.Text, txt_diachi.Text, cb_gioitinh.Text, txt_sdt.Text, txt_mnv.Text);
                dtv_nhanvien.DataSource = nv.getNV();
            }
        }

    }
}
