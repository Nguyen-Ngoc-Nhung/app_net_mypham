namespace QLMyPham.GUI
{
    partial class BanHang
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
            this.cb_mahang = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_soluong = new System.Windows.Forms.TextBox();
            this.VNĐ = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTaoHD = new QLMyPham.Custom.ButtonMyPham();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDTL = new System.Windows.Forms.Label();
            this.cb_makh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_delete = new QLMyPham.Custom.ButtonMyPham();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_update = new QLMyPham.Custom.ButtonMyPham();
            this.btn_add = new QLMyPham.Custom.ButtonMyPham();
            this.dtv_hd = new System.Windows.Forms.DataGridView();
            this.MaHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayBAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLUONG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dongia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThanhTIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_tongtien = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTaoKH = new QLMyPham.Custom.ButtonMyPham();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_sdt = new System.Windows.Forms.TextBox();
            this.txt_giamgia = new System.Windows.Forms.TextBox();
            this.txtThanhToan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_tinhtong = new System.Windows.Forms.Button();
            this.btnThanhToan = new QLMyPham.Custom.ButtonMyPham();
            this.btnNew = new QLMyPham.Custom.ButtonMyPham();
            this.btnInHD = new QLMyPham.Custom.ButtonMyPham();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_hd)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_mahang
            // 
            this.cb_mahang.Enabled = false;
            this.cb_mahang.FormattingEnabled = true;
            this.cb_mahang.Location = new System.Drawing.Point(264, 42);
            this.cb_mahang.Margin = new System.Windows.Forms.Padding(4);
            this.cb_mahang.Name = "cb_mahang";
            this.cb_mahang.Size = new System.Drawing.Size(140, 29);
            this.cb_mahang.TabIndex = 97;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(168, 42);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 19);
            this.label11.TabIndex = 94;
            this.label11.Text = "Mã hàng";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(426, 42);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 19);
            this.label9.TabIndex = 93;
            this.label9.Text = "Số lượng";
            // 
            // txt_soluong
            // 
            this.txt_soluong.Enabled = false;
            this.txt_soluong.Location = new System.Drawing.Point(522, 42);
            this.txt_soluong.Margin = new System.Windows.Forms.Padding(4);
            this.txt_soluong.Name = "txt_soluong";
            this.txt_soluong.Size = new System.Drawing.Size(140, 29);
            this.txt_soluong.TabIndex = 89;
            this.txt_soluong.TextChanged += new System.EventHandler(this.txt_soluong_TextChanged);
            // 
            // VNĐ
            // 
            this.VNĐ.AutoSize = true;
            this.VNĐ.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VNĐ.Location = new System.Drawing.Point(1441, 712);
            this.VNĐ.Name = "VNĐ";
            this.VNĐ.Size = new System.Drawing.Size(50, 21);
            this.VNĐ.TabIndex = 75;
            this.VNĐ.Text = "VNĐ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTaoHD);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblDTL);
            this.groupBox1.Controls.Add(this.cb_makh);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(127, 81);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(746, 139);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chung";
            // 
            // btnTaoHD
            // 
            this.btnTaoHD.BackColor = System.Drawing.Color.Crimson;
            this.btnTaoHD.BackgroundColor = System.Drawing.Color.Crimson;
            this.btnTaoHD.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnTaoHD.BorderRadius = 20;
            this.btnTaoHD.BorderSize = 0;
            this.btnTaoHD.FlatAppearance.BorderSize = 0;
            this.btnTaoHD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoHD.ForeColor = System.Drawing.Color.White;
            this.btnTaoHD.Image = global::QLMyPham.Properties.Resources.bill;
            this.btnTaoHD.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnTaoHD.Location = new System.Drawing.Point(359, 51);
            this.btnTaoHD.Name = "btnTaoHD";
            this.btnTaoHD.Size = new System.Drawing.Size(150, 40);
            this.btnTaoHD.TabIndex = 87;
            this.btnTaoHD.Text = "Tạo hóa đơn";
            this.btnTaoHD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTaoHD.TextColor = System.Drawing.Color.White;
            this.btnTaoHD.UseVisualStyleBackColor = false;
            this.btnTaoHD.Click += new System.EventHandler(this.vbButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(546, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 21);
            this.label3.TabIndex = 85;
            this.label3.Text = "Điểm tích lũy :";
            // 
            // lblDTL
            // 
            this.lblDTL.AutoSize = true;
            this.lblDTL.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDTL.ForeColor = System.Drawing.Color.Red;
            this.lblDTL.Location = new System.Drawing.Point(685, 60);
            this.lblDTL.Name = "lblDTL";
            this.lblDTL.Size = new System.Drawing.Size(19, 21);
            this.lblDTL.TabIndex = 84;
            this.lblDTL.Text = "0";
            // 
            // cb_makh
            // 
            this.cb_makh.FormattingEnabled = true;
            this.cb_makh.Location = new System.Drawing.Point(169, 58);
            this.cb_makh.Margin = new System.Windows.Forms.Padding(4);
            this.cb_makh.Name = "cb_makh";
            this.cb_makh.Size = new System.Drawing.Size(162, 29);
            this.cb_makh.TabIndex = 72;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(40, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 19);
            this.label2.TabIndex = 65;
            this.label2.Text = "Mã khách hàng";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_delete);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn_update);
            this.groupBox2.Controls.Add(this.btn_add);
            this.groupBox2.Controls.Add(this.dtv_hd);
            this.groupBox2.Controls.Add(this.cb_mahang);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txt_soluong);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(127, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1302, 338);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin các mặt hàng";
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_delete.BackgroundColor = System.Drawing.Color.OrangeRed;
            this.btn_delete.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_delete.BorderRadius = 20;
            this.btn_delete.BorderSize = 0;
            this.btn_delete.Enabled = false;
            this.btn_delete.FlatAppearance.BorderSize = 0;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.ForeColor = System.Drawing.Color.White;
            this.btn_delete.Image = global::QLMyPham.Properties.Resources.tsbDelete;
            this.btn_delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_delete.Location = new System.Drawing.Point(970, 40);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(96, 37);
            this.btn_delete.TabIndex = 127;
            this.btn_delete.Text = "Xóa";
            this.btn_delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_delete.TextColor = System.Drawing.Color.White;
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.vbButton4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Image = global::QLMyPham.Properties.Resources._044_store;
            this.label1.Location = new System.Drawing.Point(18, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 21);
            this.label1.TabIndex = 76;
            this.label1.Text = "    ";
            // 
            // btn_update
            // 
            this.btn_update.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_update.BackgroundColor = System.Drawing.Color.OrangeRed;
            this.btn_update.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_update.BorderRadius = 20;
            this.btn_update.BorderSize = 0;
            this.btn_update.Enabled = false;
            this.btn_update.FlatAppearance.BorderSize = 0;
            this.btn_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_update.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_update.ForeColor = System.Drawing.Color.White;
            this.btn_update.Image = global::QLMyPham.Properties.Resources.tsbRefresh;
            this.btn_update.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_update.Location = new System.Drawing.Point(1128, 40);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(98, 37);
            this.btn_update.TabIndex = 126;
            this.btn_update.Text = "Sửa";
            this.btn_update.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_update.TextColor = System.Drawing.Color.White;
            this.btn_update.UseVisualStyleBackColor = false;
            this.btn_update.Click += new System.EventHandler(this.vbButton3_Click);
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_add.BackgroundColor = System.Drawing.Color.OrangeRed;
            this.btn_add.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_add.BorderRadius = 20;
            this.btn_add.BorderSize = 0;
            this.btn_add.Enabled = false;
            this.btn_add.FlatAppearance.BorderSize = 0;
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.ForeColor = System.Drawing.Color.White;
            this.btn_add.Image = global::QLMyPham.Properties.Resources.tsbAddNew;
            this.btn_add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_add.Location = new System.Drawing.Point(803, 40);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(102, 37);
            this.btn_add.TabIndex = 125;
            this.btn_add.Text = "Thêm";
            this.btn_add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_add.TextColor = System.Drawing.Color.White;
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.vbButton2_Click);
            // 
            // dtv_hd
            // 
            this.dtv_hd.AllowUserToAddRows = false;
            this.dtv_hd.AllowUserToDeleteRows = false;
            this.dtv_hd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtv_hd.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtv_hd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtv_hd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtv_hd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaHD,
            this.Column1,
            this.NgayBAN,
            this.MaKH,
            this.MaH,
            this.TENSP,
            this.SoLUONG,
            this.Dongia,
            this.ThanhTIEN});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtv_hd.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtv_hd.GridColor = System.Drawing.Color.Salmon;
            this.dtv_hd.Location = new System.Drawing.Point(5, 100);
            this.dtv_hd.Name = "dtv_hd";
            this.dtv_hd.ReadOnly = true;
            this.dtv_hd.Size = new System.Drawing.Size(1291, 220);
            this.dtv_hd.TabIndex = 103;
            this.dtv_hd.SelectionChanged += new System.EventHandler(this.dtv_hd_SelectionChanged);
            // 
            // MaHD
            // 
            this.MaHD.DataPropertyName = "MAHD";
            this.MaHD.HeaderText = "Mã hóa đơn";
            this.MaHD.Name = "MaHD";
            this.MaHD.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MANV";
            this.Column1.HeaderText = "Mã nhân viên";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // NgayBAN
            // 
            this.NgayBAN.DataPropertyName = "NGTAO";
            this.NgayBAN.HeaderText = "Ngày Bán";
            this.NgayBAN.Name = "NgayBAN";
            this.NgayBAN.ReadOnly = true;
            // 
            // MaKH
            // 
            this.MaKH.DataPropertyName = "MAKH";
            this.MaKH.HeaderText = "Mã Khách hàng";
            this.MaKH.Name = "MaKH";
            this.MaKH.ReadOnly = true;
            // 
            // MaH
            // 
            this.MaH.DataPropertyName = "MASP";
            this.MaH.HeaderText = "Mã hàng";
            this.MaH.Name = "MaH";
            this.MaH.ReadOnly = true;
            // 
            // TENSP
            // 
            this.TENSP.DataPropertyName = "TENSP";
            this.TENSP.HeaderText = "Tên sản phẩm";
            this.TENSP.Name = "TENSP";
            this.TENSP.ReadOnly = true;
            // 
            // SoLUONG
            // 
            this.SoLUONG.DataPropertyName = "SOLUONG";
            this.SoLUONG.HeaderText = "Số lượng";
            this.SoLUONG.Name = "SoLUONG";
            this.SoLUONG.ReadOnly = true;
            // 
            // Dongia
            // 
            this.Dongia.DataPropertyName = "DONGIA";
            this.Dongia.HeaderText = "Đơn giá";
            this.Dongia.Name = "Dongia";
            this.Dongia.ReadOnly = true;
            // 
            // ThanhTIEN
            // 
            this.ThanhTIEN.DataPropertyName = "TONG";
            this.ThanhTIEN.HeaderText = "Thành tiền";
            this.ThanhTIEN.Name = "ThanhTIEN";
            this.ThanhTIEN.ReadOnly = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.White;
            this.label17.Font = new System.Drawing.Font("Algerian", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(615, 9);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(488, 54);
            this.label17.TabIndex = 72;
            this.label17.Text = "HÓA ĐƠN BÁN HÀNG";
            // 
            // txt_tongtien
            // 
            this.txt_tongtien.Enabled = false;
            this.txt_tongtien.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txt_tongtien.Location = new System.Drawing.Point(1277, 611);
            this.txt_tongtien.Multiline = true;
            this.txt_tongtien.Name = "txt_tongtien";
            this.txt_tongtien.Size = new System.Drawing.Size(146, 27);
            this.txt_tongtien.TabIndex = 74;
            this.txt_tongtien.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_tongtien.TextChanged += new System.EventHandler(this.txt_tongtien_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnTaoKH);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txt_sdt);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(895, 81);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(534, 139);
            this.groupBox3.TabIndex = 75;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Khách mới";
            // 
            // btnTaoKH
            // 
            this.btnTaoKH.BackColor = System.Drawing.Color.Crimson;
            this.btnTaoKH.BackgroundColor = System.Drawing.Color.Crimson;
            this.btnTaoKH.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnTaoKH.BorderRadius = 20;
            this.btnTaoKH.BorderSize = 0;
            this.btnTaoKH.FlatAppearance.BorderSize = 0;
            this.btnTaoKH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoKH.ForeColor = System.Drawing.Color.White;
            this.btnTaoKH.Image = global::QLMyPham.Properties.Resources.khachhang;
            this.btnTaoKH.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaoKH.Location = new System.Drawing.Point(360, 47);
            this.btnTaoKH.Name = "btnTaoKH";
            this.btnTaoKH.Size = new System.Drawing.Size(167, 40);
            this.btnTaoKH.TabIndex = 124;
            this.btnTaoKH.Text = "Tạo khách hàng";
            this.btnTaoKH.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTaoKH.TextColor = System.Drawing.Color.White;
            this.btnTaoKH.UseVisualStyleBackColor = false;
            this.btnTaoKH.Click += new System.EventHandler(this.vbButton1_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(18, 61);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 19);
            this.label5.TabIndex = 122;
            this.label5.Text = "Số điện thoại";
            // 
            // txt_sdt
            // 
            this.txt_sdt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_sdt.Location = new System.Drawing.Point(145, 51);
            this.txt_sdt.Margin = new System.Windows.Forms.Padding(6);
            this.txt_sdt.Name = "txt_sdt";
            this.txt_sdt.Size = new System.Drawing.Size(178, 29);
            this.txt_sdt.TabIndex = 123;
            // 
            // txt_giamgia
            // 
            this.txt_giamgia.Enabled = false;
            this.txt_giamgia.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_giamgia.Location = new System.Drawing.Point(1277, 662);
            this.txt_giamgia.Multiline = true;
            this.txt_giamgia.Name = "txt_giamgia";
            this.txt_giamgia.Size = new System.Drawing.Size(146, 27);
            this.txt_giamgia.TabIndex = 79;
            this.txt_giamgia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_giamgia.TextChanged += new System.EventHandler(this.txt_giamgia_TextChanged_1);
            // 
            // txtThanhToan
            // 
            this.txtThanhToan.Enabled = false;
            this.txtThanhToan.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtThanhToan.Location = new System.Drawing.Point(1277, 707);
            this.txtThanhToan.Multiline = true;
            this.txtThanhToan.Name = "txtThanhToan";
            this.txtThanhToan.Size = new System.Drawing.Size(146, 27);
            this.txtThanhToan.TabIndex = 81;
            this.txtThanhToan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtThanhToan.TextChanged += new System.EventHandler(this.txtThanhToan_TextChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1441, 621);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 21);
            this.label4.TabIndex = 82;
            this.label4.Text = "VNĐ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1441, 667);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 21);
            this.label6.TabIndex = 83;
            this.label6.Text = "VNĐ";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::QLMyPham.Properties.Resources._021_coupon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(1112, 662);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 31);
            this.button1.TabIndex = 78;
            this.button1.Text = "Giảm giá";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btn_tinhtong
            // 
            this.btn_tinhtong.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_tinhtong.Enabled = false;
            this.btn_tinhtong.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tinhtong.Image = global::QLMyPham.Properties.Resources._011_cart;
            this.btn_tinhtong.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_tinhtong.Location = new System.Drawing.Point(1112, 611);
            this.btn_tinhtong.Name = "btn_tinhtong";
            this.btn_tinhtong.Size = new System.Drawing.Size(141, 31);
            this.btn_tinhtong.TabIndex = 73;
            this.btn_tinhtong.Text = "Tổng tiền";
            this.btn_tinhtong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_tinhtong.UseVisualStyleBackColor = false;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.DarkCyan;
            this.btnThanhToan.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.btnThanhToan.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnThanhToan.BorderRadius = 20;
            this.btnThanhToan.BorderSize = 0;
            this.btnThanhToan.Enabled = false;
            this.btnThanhToan.FlatAppearance.BorderSize = 0;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Image = global::QLMyPham.Properties.Resources._014_cashier;
            this.btnThanhToan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThanhToan.Location = new System.Drawing.Point(1097, 707);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(167, 40);
            this.btnThanhToan.TabIndex = 127;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnThanhToan.TextColor = System.Drawing.Color.White;
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.vbButton7_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.DarkCyan;
            this.btnNew.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.btnNew.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnNew.BorderRadius = 20;
            this.btnNew.BorderSize = 0;
            this.btnNew.Enabled = false;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Image = global::QLMyPham.Properties.Resources._020_cosmetics;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(622, 712);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(167, 40);
            this.btnNew.TabIndex = 126;
            this.btnNew.Text = "Hóa đơn mới";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.TextColor = System.Drawing.Color.White;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.vbButton6_Click);
            // 
            // btnInHD
            // 
            this.btnInHD.BackColor = System.Drawing.Color.DarkCyan;
            this.btnInHD.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.btnInHD.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnInHD.BorderRadius = 20;
            this.btnInHD.BorderSize = 0;
            this.btnInHD.Enabled = false;
            this.btnInHD.FlatAppearance.BorderSize = 0;
            this.btnInHD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInHD.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInHD.ForeColor = System.Drawing.Color.White;
            this.btnInHD.Image = global::QLMyPham.Properties.Resources._013_cash_register;
            this.btnInHD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInHD.Location = new System.Drawing.Point(865, 712);
            this.btnInHD.Name = "btnInHD";
            this.btnInHD.Size = new System.Drawing.Size(167, 40);
            this.btnInHD.TabIndex = 125;
            this.btnInHD.Text = "In hóa đơn";
            this.btnInHD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInHD.TextColor = System.Drawing.Color.White;
            this.btnInHD.UseVisualStyleBackColor = false;
            this.btnInHD.Click += new System.EventHandler(this.vbButton5_Click);
            // 
            // BanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1604, 881);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnInHD);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtThanhToan);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_giamgia);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_tinhtong);
            this.Controls.Add(this.VNĐ);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txt_tongtien);
            this.Name = "BanHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BanHang";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BanHang_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_hd)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cb_mahang;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txt_soluong;
        private System.Windows.Forms.Button btn_tinhtong;
        private System.Windows.Forms.Label VNĐ;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cb_makh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView dtv_hd;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_tongtien;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txt_sdt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_giamgia;
        private System.Windows.Forms.TextBox txtThanhToan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDTL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayBAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLUONG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dongia;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThanhTIEN;
        private Custom.ButtonMyPham btnTaoHD;
        private Custom.ButtonMyPham btnTaoKH;
        private Custom.ButtonMyPham btn_add;
        private Custom.ButtonMyPham btn_update;
        private Custom.ButtonMyPham btn_delete;
        private Custom.ButtonMyPham btnInHD;
        private Custom.ButtonMyPham btnNew;
        private Custom.ButtonMyPham btnThanhToan;
    }
}