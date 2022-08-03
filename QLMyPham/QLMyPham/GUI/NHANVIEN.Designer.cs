namespace QLMyPham.GUI
{
    partial class NhanVien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cb_gioitinh = new System.Windows.Forms.ComboBox();
            this.dt_ngaysinh = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_sdt = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_diachi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_tennv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_mnv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtv_nhanvien = new System.Windows.Forms.DataGridView();
            this.MaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgaySINH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GioiTINH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiaCHI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.EMAIL = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TENTK = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.MATK = new System.Windows.Forms.TextBox();
            this.btn_delete = new QLMyPham.Custom.ButtonMyPham();
            this.btn_update = new QLMyPham.Custom.ButtonMyPham();
            this.btn_add = new QLMyPham.Custom.ButtonMyPham();
            this.vbButton2 = new QLMyPham.Custom.ButtonMyPham();
            this.vbButton1 = new QLMyPham.Custom.ButtonMyPham();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_nhanvien)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_gioitinh
            // 
            this.cb_gioitinh.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_gioitinh.FormattingEnabled = true;
            this.cb_gioitinh.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.cb_gioitinh.Location = new System.Drawing.Point(301, 241);
            this.cb_gioitinh.Name = "cb_gioitinh";
            this.cb_gioitinh.Size = new System.Drawing.Size(121, 29);
            this.cb_gioitinh.TabIndex = 105;
            // 
            // dt_ngaysinh
            // 
            this.dt_ngaysinh.CustomFormat = "dd/MM/yyyy";
            this.dt_ngaysinh.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_ngaysinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_ngaysinh.Location = new System.Drawing.Point(780, 115);
            this.dt_ngaysinh.Name = "dt_ngaysinh";
            this.dt_ngaysinh.Size = new System.Drawing.Size(236, 29);
            this.dt_ngaysinh.TabIndex = 104;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Algerian", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(423, 34);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(479, 54);
            this.label9.TabIndex = 103;
            this.label9.Text = "QUẢN LÝ NHÂN VIÊN";
            // 
            // txt_sdt
            // 
            this.txt_sdt.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sdt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_sdt.Location = new System.Drawing.Point(780, 176);
            this.txt_sdt.Margin = new System.Windows.Forms.Padding(4);
            this.txt_sdt.Name = "txt_sdt";
            this.txt_sdt.Size = new System.Drawing.Size(236, 27);
            this.txt_sdt.TabIndex = 98;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(642, 183);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 97;
            this.label4.Text = "Số điện thoại";
            // 
            // txt_diachi
            // 
            this.txt_diachi.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_diachi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_diachi.Location = new System.Drawing.Point(780, 244);
            this.txt_diachi.Margin = new System.Windows.Forms.Padding(4);
            this.txt_diachi.Name = "txt_diachi";
            this.txt_diachi.Size = new System.Drawing.Size(236, 27);
            this.txt_diachi.TabIndex = 96;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label6.Location = new System.Drawing.Point(685, 251);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 95;
            this.label6.Text = "Địa Chỉ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label7.Location = new System.Drawing.Point(172, 241);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 20);
            this.label7.TabIndex = 94;
            this.label7.Text = "Giới Tính";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Location = new System.Drawing.Point(668, 124);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 93;
            this.label3.Text = "Ngày Sinh";
            // 
            // txt_tennv
            // 
            this.txt_tennv.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tennv.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_tennv.Location = new System.Drawing.Point(301, 183);
            this.txt_tennv.Margin = new System.Windows.Forms.Padding(4);
            this.txt_tennv.Name = "txt_tennv";
            this.txt_tennv.Size = new System.Drawing.Size(198, 27);
            this.txt_tennv.TabIndex = 92;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(131, 183);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 20);
            this.label2.TabIndex = 91;
            this.label2.Text = "Tên Nhân Viên";
            // 
            // txt_mnv
            // 
            this.txt_mnv.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mnv.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_mnv.Location = new System.Drawing.Point(301, 121);
            this.txt_mnv.Margin = new System.Windows.Forms.Padding(4);
            this.txt_mnv.Name = "txt_mnv";
            this.txt_mnv.Size = new System.Drawing.Size(198, 27);
            this.txt_mnv.TabIndex = 90;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(144, 127);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 19);
            this.label1.TabIndex = 89;
            this.label1.Text = "Mã Nhân Viên";
            // 
            // dtv_nhanvien
            // 
            this.dtv_nhanvien.AllowUserToAddRows = false;
            this.dtv_nhanvien.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            this.dtv_nhanvien.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtv_nhanvien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtv_nhanvien.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtv_nhanvien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtv_nhanvien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtv_nhanvien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaNV,
            this.TenNV,
            this.NgaySINH,
            this.GioiTINH,
            this.Column2,
            this.Column1,
            this.SDT,
            this.DiaCHI,
            this.Column5,
            this.Column3,
            this.Column4,
            this.Column12});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtv_nhanvien.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtv_nhanvien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtv_nhanvien.GridColor = System.Drawing.Color.LavenderBlush;
            this.dtv_nhanvien.Location = new System.Drawing.Point(0, 506);
            this.dtv_nhanvien.Name = "dtv_nhanvien";
            this.dtv_nhanvien.ReadOnly = true;
            this.dtv_nhanvien.Size = new System.Drawing.Size(1604, 375);
            this.dtv_nhanvien.TabIndex = 107;
            this.dtv_nhanvien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtv_nhanvien_CellClick);
            this.dtv_nhanvien.SelectionChanged += new System.EventHandler(this.dtv_nhanvien_SelectionChanged);
            // 
            // MaNV
            // 
            this.MaNV.DataPropertyName = "MANV";
            this.MaNV.HeaderText = "Mã nhân viên";
            this.MaNV.Name = "MaNV";
            this.MaNV.ReadOnly = true;
            // 
            // TenNV
            // 
            this.TenNV.DataPropertyName = "HOTEN";
            this.TenNV.HeaderText = "Tên nhân viên";
            this.TenNV.Name = "TenNV";
            this.TenNV.ReadOnly = true;
            // 
            // NgaySINH
            // 
            this.NgaySINH.DataPropertyName = "NGSINH";
            this.NgaySINH.HeaderText = "Ngày sinh";
            this.NgaySINH.Name = "NgaySINH";
            this.NgaySINH.ReadOnly = true;
            // 
            // GioiTINH
            // 
            this.GioiTINH.DataPropertyName = "GTINH";
            this.GioiTINH.HeaderText = "Giới tính";
            this.GioiTINH.Name = "GioiTINH";
            this.GioiTINH.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "NGTAO";
            this.Column2.HeaderText = "Ngày vào làm";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "EMAIL";
            this.Column1.HeaderText = "Email";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // SDT
            // 
            this.SDT.DataPropertyName = "SDT";
            this.SDT.HeaderText = "Số điện thoại";
            this.SDT.Name = "SDT";
            this.SDT.ReadOnly = true;
            // 
            // DiaCHI
            // 
            this.DiaCHI.DataPropertyName = "DCHI";
            this.DiaCHI.HeaderText = "Địa chỉ";
            this.DiaCHI.Name = "DiaCHI";
            this.DiaCHI.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "MATK";
            this.Column5.HeaderText = "Mã tài khoản";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "TENTK";
            this.Column3.HeaderText = "Tên tài khoản";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "TENQUYEN";
            this.Column4.HeaderText = "Quyền";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Resert mật khẩu";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Text = "Reset";
            this.Column12.UseColumnTextForButtonValue = true;
            // 
            // EMAIL
            // 
            this.EMAIL.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EMAIL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.EMAIL.Location = new System.Drawing.Point(1228, 127);
            this.EMAIL.Margin = new System.Windows.Forms.Padding(4);
            this.EMAIL.Name = "EMAIL";
            this.EMAIL.Size = new System.Drawing.Size(236, 27);
            this.EMAIL.TabIndex = 112;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label5.Location = new System.Drawing.Point(1148, 134);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.TabIndex = 111;
            this.label5.Text = "Email";
            // 
            // TENTK
            // 
            this.TENTK.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TENTK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TENTK.Location = new System.Drawing.Point(1228, 248);
            this.TENTK.Margin = new System.Windows.Forms.Padding(4);
            this.TENTK.Name = "TENTK";
            this.TENTK.Size = new System.Drawing.Size(236, 27);
            this.TENTK.TabIndex = 114;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label10.Location = new System.Drawing.Point(1085, 255);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 20);
            this.label10.TabIndex = 113;
            this.label10.Text = "Tên tài khoản";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label8.Location = new System.Drawing.Point(1085, 190);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 20);
            this.label8.TabIndex = 109;
            this.label8.Text = "Mã tài khoản";
            // 
            // MATK
            // 
            this.MATK.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MATK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MATK.Location = new System.Drawing.Point(1228, 183);
            this.MATK.Margin = new System.Windows.Forms.Padding(4);
            this.MATK.Name = "MATK";
            this.MATK.Size = new System.Drawing.Size(236, 27);
            this.MATK.TabIndex = 110;
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_delete.BackgroundColor = System.Drawing.Color.OrangeRed;
            this.btn_delete.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_delete.BorderRadius = 20;
            this.btn_delete.BorderSize = 0;
            this.btn_delete.FlatAppearance.BorderSize = 0;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.ForeColor = System.Drawing.Color.White;
            this.btn_delete.Image = global::QLMyPham.Properties.Resources.tsbDelete;
            this.btn_delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_delete.Location = new System.Drawing.Point(474, 332);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(96, 37);
            this.btn_delete.TabIndex = 133;
            this.btn_delete.Text = "Xóa";
            this.btn_delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_delete.TextColor = System.Drawing.Color.White;
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_update
            // 
            this.btn_update.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_update.BackgroundColor = System.Drawing.Color.OrangeRed;
            this.btn_update.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_update.BorderRadius = 20;
            this.btn_update.BorderSize = 0;
            this.btn_update.FlatAppearance.BorderSize = 0;
            this.btn_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_update.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update.ForeColor = System.Drawing.Color.White;
            this.btn_update.Image = global::QLMyPham.Properties.Resources.tsbRefresh;
            this.btn_update.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_update.Location = new System.Drawing.Point(659, 332);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(98, 37);
            this.btn_update.TabIndex = 132;
            this.btn_update.Text = "Sửa";
            this.btn_update.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_update.TextColor = System.Drawing.Color.White;
            this.btn_update.UseVisualStyleBackColor = false;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_add.BackgroundColor = System.Drawing.Color.OrangeRed;
            this.btn_add.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_add.BorderRadius = 20;
            this.btn_add.BorderSize = 0;
            this.btn_add.FlatAppearance.BorderSize = 0;
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.ForeColor = System.Drawing.Color.White;
            this.btn_add.Image = global::QLMyPham.Properties.Resources.tsbAddNew;
            this.btn_add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_add.Location = new System.Drawing.Point(271, 332);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(102, 37);
            this.btn_add.TabIndex = 131;
            this.btn_add.Text = "Thêm";
            this.btn_add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_add.TextColor = System.Drawing.Color.White;
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // vbButton2
            // 
            this.vbButton2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.vbButton2.BackgroundColor = System.Drawing.Color.DeepSkyBlue;
            this.vbButton2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton2.BorderRadius = 20;
            this.vbButton2.BorderSize = 0;
            this.vbButton2.FlatAppearance.BorderSize = 0;
            this.vbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton2.ForeColor = System.Drawing.Color.White;
            this.vbButton2.Image = global::QLMyPham.Properties.Resources.search;
            this.vbButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vbButton2.Location = new System.Drawing.Point(847, 332);
            this.vbButton2.Name = "vbButton2";
            this.vbButton2.Size = new System.Drawing.Size(184, 37);
            this.vbButton2.TabIndex = 156;
            this.vbButton2.Text = "Tìm kiếm theo tên";
            this.vbButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.vbButton2.TextColor = System.Drawing.Color.White;
            this.vbButton2.UseVisualStyleBackColor = false;
            this.vbButton2.Click += new System.EventHandler(this.vbButton2_Click);
            // 
            // vbButton1
            // 
            this.vbButton1.BackColor = System.Drawing.Color.SeaGreen;
            this.vbButton1.BackgroundColor = System.Drawing.Color.SeaGreen;
            this.vbButton1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton1.BorderRadius = 20;
            this.vbButton1.BorderSize = 0;
            this.vbButton1.FlatAppearance.BorderSize = 0;
            this.vbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton1.ForeColor = System.Drawing.Color.White;
            this.vbButton1.Image = global::QLMyPham.Properties.Resources.profile;
            this.vbButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vbButton1.Location = new System.Drawing.Point(1101, 332);
            this.vbButton1.Name = "vbButton1";
            this.vbButton1.Size = new System.Drawing.Size(98, 37);
            this.vbButton1.TabIndex = 157;
            this.vbButton1.Text = "Xuất";
            this.vbButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.vbButton1.TextColor = System.Drawing.Color.White;
            this.vbButton1.UseVisualStyleBackColor = false;
            this.vbButton1.Click += new System.EventHandler(this.vbButton1_Click);
            // 
            // NhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1604, 881);
            this.Controls.Add(this.vbButton1);
            this.Controls.Add(this.vbButton2);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.TENTK);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.EMAIL);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MATK);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtv_nhanvien);
            this.Controls.Add(this.cb_gioitinh);
            this.Controls.Add(this.dt_ngaysinh);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_sdt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_diachi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_tennv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_mnv);
            this.Controls.Add(this.label1);
            this.Name = "NhanVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.NHANVIEN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtv_nhanvien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cb_gioitinh;
        public System.Windows.Forms.DateTimePicker dt_ngaysinh;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.MaskedTextBox txt_sdt;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txt_diachi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txt_tennv;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txt_mnv;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dtv_nhanvien;
        public System.Windows.Forms.MaskedTextBox EMAIL;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox TENTK;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox MATK;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgaySINH;
        private System.Windows.Forms.DataGridViewTextBoxColumn GioiTINH;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiaCHI;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn Column12;
        private Custom.ButtonMyPham btn_delete;
        private Custom.ButtonMyPham btn_update;
        private Custom.ButtonMyPham btn_add;
        private Custom.ButtonMyPham vbButton2;
        private Custom.ButtonMyPham vbButton1;

    }
}