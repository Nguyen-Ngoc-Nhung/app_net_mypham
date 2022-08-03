using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLMyPham.BUS;
using System.Text.RegularExpressions;
namespace QLMyPham.GUI
{
    public partial class DoiMatKhau : Form
    {
        string user = DangNhap.User;
        string pass=DangNhap.Pass;
        TAIKHOAN_BUS TK = new TAIKHOAN_BUS();
        private StringBuilder encrypt(String str)
        {
            StringBuilder sb = new StringBuilder();//đối tượng lớn
            MD5 md5 = MD5.Create();
            //chuyển chuỗi về kiểu byte
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(str);
            //mã hóa chuỗi
            byte[] hash = md5.ComputeHash(inputBytes);
            //hàm lặp mã hóa
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x"));//in ra đoạn mã hóa chữ thường
            }
            return sb;
        }
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtOldPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtOldPass.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                txtNewPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtNewPass.UseSystemPasswordChar = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                txtReNewPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtReNewPass.UseSystemPasswordChar = true;
            }
        }
        // Function to validate the password.
        public static bool IsStrongPass(String password)
        {

            // Regex to check valid password.
            String regex = "^(?=.*[0-9])"
                           + "(?=.*[a-z])(?=.*[A-Z])"
                           + "(?=.*[@#$%^&+=])"
                           + "(?=\\S+$).{8,20}$";
            //1] Min 1 uppercase letter.
            //2] Min 1 lowercase letter.
            //3] Min 1 special character.
            //4] Min 1 number.
            //5] Min 8 characters.
            //6] Max 30 characters.
            // Compile the ReGex
            Regex re = new Regex(regex);


            if (re.IsMatch(password))
                return (true);
            else
                return (false);
        }
        private void vbButton2_Click(object sender, EventArgs e)
        {
            if ((txtOldPass.Text == null) || (txtOldPass.Text.ToString().Equals("")))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu cũ\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOldPass.Focus();
                return;
            }
            if ((txtNewPass.Text == null) || (txtNewPass.Text.ToString().Equals("")))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu mới\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewPass.Focus();
                return;
            }
            if ((txtReNewPass.Text == null) || (txtReNewPass.Text.ToString().Equals("")))
            {
                MessageBox.Show("Bạn chưa nhập lại mật khẩu mới\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReNewPass.Focus();
                return;
            }
            if (!txtNewPass.Text.ToString().Equals(txtReNewPass.Text.ToString()))
            {
                MessageBox.Show("Hai mật khẩu mới không trùng nhau\r\nVui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReNewPass.Focus();
                return;
            }
            if (!encrypt(txtOldPass.Text.ToString()).ToString().Equals(pass))
            {
                MessageBox.Show("Mật khẩu cũ không đúng\r\nVui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOldPass.Focus();
                return;
            }
            if (IsStrongPass(txtNewPass.Text) == false)
            {
                MessageBox.Show("Mật khẩu này chưa đủ mạnh\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewPass.Focus();
                return;
            }
            TK.updateMATKHAU(user, encrypt(txtNewPass.Text.ToString()).ToString());
        }

        private void txtNewPass_Leave(object sender, EventArgs e)
        {
            if (IsStrongPass(txtNewPass.Text) == false)
            {
                errorProvider1.SetError(txtNewPass, "Mật khẩu này chưa đủ mạnh\r\n1] Tối thiểu 1 ký tự hoa.\r\n2] Tối thiểu 1 ký tự thường.\r\n3] Tối thiểu 1 ký tự đặc biệt.\r\n4] Tối thiểu 1 số.\r\n5] Tối thiểu 8 ký tự.\r\n6] Tối đa 30 ký tự.");
                txtNewPass.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtReNewPass_Leave(object sender, EventArgs e)
        {
            if (IsStrongPass(txtReNewPass.Text) == false)
            {
                errorProvider1.SetError(txtReNewPass, "Mật khẩu này chưa đủ mạnh\r\n1] Tối thiểu 1 ký tự hoa.\r\n2] Tối thiểu 1 ký tự thường.\r\n3] Tối thiểu 1 ký tự đặc biệt.\r\n4] Tối thiểu 1 số.\r\n5] Tối thiểu 8 ký tự.\r\n6] Tối đa 30 ký tự.");
                txtNewPass.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
