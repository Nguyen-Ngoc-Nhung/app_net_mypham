using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLMyPham.BUS;
namespace QLMyPham.GUI
{
    public partial class CapNhatTTCN : Form
    {
        public CapNhatTTCN()
        {
            InitializeComponent();
        }
        NHANVIEN_BUS nv = new NHANVIEN_BUS();
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

        string MATK = DangNhap.Matk;//phương thức mã nhân viên đăng nhập
        string name = DangNhap.Name_user;
        string ngsinh = DangNhap.Ngsinh;
        string diachi = DangNhap.Diachi;
        string sdt = DangNhap.Sdt;
        string email = DangNhap.Email;
        string gioitinh = DangNhap.Gioitinh;
        private void CapNhatTTCN_Load(object sender, EventArgs e)
        {
            txt_diachi.Text = diachi;
            EMAIL.Text = email;
            txt_tennv.Text = name;
            dt_ngaysinh.Text = ngsinh;
            cb_gioitinh.Text = gioitinh;
            txt_sdt.Text = sdt;
        }

        private void vbButton1_Click(object sender, EventArgs e)
        {

            if (txt_tennv.Text.Length == 0 || txt_sdt.Text.Length == 0 || txt_diachi.Text.Length == 0 || cb_gioitinh.Text.Length == 0 || EMAIL.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
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
            else
            {
                nv.capnhatnv(txt_tennv.Text, dt_ngaysinh.Value.ToString("dd/MM/yyyy"), EMAIL.Text, txt_diachi.Text, cb_gioitinh.Text, txt_sdt.Text, MATK);
            }
        }
        private void frmmhc_Closed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
