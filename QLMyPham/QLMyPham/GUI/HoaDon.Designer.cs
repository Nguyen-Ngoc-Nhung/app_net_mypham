namespace QLMyPham.GUI
{
    partial class HoaDon
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
            this.label8 = new System.Windows.Forms.Label();
            this.dtv_HD = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_THHTOAN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtmaHD = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CB = new System.Windows.Forms.ComboBox();
            this.MAKH = new System.Windows.Forms.ComboBox();
            this.cb_manv = new System.Windows.Forms.ComboBox();
            this.btn_delete = new QLMyPham.Custom.ButtonMyPham();
            this.btn_update = new QLMyPham.Custom.ButtonMyPham();
            this.btn_add = new QLMyPham.Custom.ButtonMyPham();
            ((System.ComponentModel.ISupportInitialize)(this.dtv_HD)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label8.Location = new System.Drawing.Point(821, 177);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 20);
            this.label8.TabIndex = 133;
            this.label8.Text = "Tình trạng";
            // 
            // dtv_HD
            // 
            this.dtv_HD.AllowUserToAddRows = false;
            this.dtv_HD.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            this.dtv_HD.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtv_HD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtv_HD.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtv_HD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtv_HD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtv_HD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column5,
            this.Column12});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtv_HD.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtv_HD.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtv_HD.GridColor = System.Drawing.Color.LavenderBlush;
            this.dtv_HD.Location = new System.Drawing.Point(0, 540);
            this.dtv_HD.Name = "dtv_HD";
            this.dtv_HD.ReadOnly = true;
            this.dtv_HD.Size = new System.Drawing.Size(1604, 341);
            this.dtv_HD.TabIndex = 132;
            this.dtv_HD.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtv_HD_CellClick);
            this.dtv_HD.SelectionChanged += new System.EventHandler(this.dtv_HD_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MAHD";
            this.Column1.HeaderText = "Mã hóa đơn";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "NGTAO";
            this.Column2.HeaderText = "Ngày tạo";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "THANHTOAN";
            this.Column3.HeaderText = "Tổng tiền";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "TINHTRANG";
            this.Column4.HeaderText = "Tình trạng";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "MAKH";
            this.Column6.HeaderText = "Mã khách hàng";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "MANV";
            this.Column5.HeaderText = "Mã nhân viên";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Chi tiết";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Text = "Chi tiết";
            this.Column12.UseColumnTextForButtonValue = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Algerian", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(460, 9);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(581, 71);
            this.label9.TabIndex = 128;
            this.label9.Text = "QUẢN LÝ HÓA ĐƠN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(160, 265);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 20);
            this.label4.TabIndex = 123;
            this.label4.Text = "Mã khách hàng";
            // 
            // txt_THHTOAN
            // 
            this.txt_THHTOAN.Enabled = false;
            this.txt_THHTOAN.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_THHTOAN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_THHTOAN.Location = new System.Drawing.Point(945, 236);
            this.txt_THHTOAN.Margin = new System.Windows.Forms.Padding(4);
            this.txt_THHTOAN.Name = "txt_THHTOAN";
            this.txt_THHTOAN.Size = new System.Drawing.Size(236, 27);
            this.txt_THHTOAN.TabIndex = 122;
            this.txt_THHTOAN.TextChanged += new System.EventHandler(this.txt_THHTOAN_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label6.Location = new System.Drawing.Point(831, 243);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.TabIndex = 121;
            this.label6.Text = "Tổng tiền";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(178, 202);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 19);
            this.label1.TabIndex = 115;
            this.label1.Text = "Mã Nhân Viên";
            // 
            // txtmaHD
            // 
            this.txtmaHD.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmaHD.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtmaHD.Location = new System.Drawing.Point(335, 133);
            this.txtmaHD.Margin = new System.Windows.Forms.Padding(4);
            this.txtmaHD.Name = "txtmaHD";
            this.txtmaHD.Size = new System.Drawing.Size(236, 27);
            this.txtmaHD.TabIndex = 136;
            this.txtmaHD.MaskChanged += new System.EventHandler(this.txtmaHD_MaskChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(187, 140);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 135;
            this.label2.Text = "Mã hóa đơn";
            // 
            // CB
            // 
            this.CB.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB.FormattingEnabled = true;
            this.CB.Items.AddRange(new object[] {
            "Đã thanh toán",
            "Chưa thanh toán"});
            this.CB.Location = new System.Drawing.Point(945, 176);
            this.CB.Name = "CB";
            this.CB.Size = new System.Drawing.Size(236, 29);
            this.CB.TabIndex = 137;
            // 
            // MAKH
            // 
            this.MAKH.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MAKH.FormattingEnabled = true;
            this.MAKH.Location = new System.Drawing.Point(335, 266);
            this.MAKH.Name = "MAKH";
            this.MAKH.Size = new System.Drawing.Size(236, 29);
            this.MAKH.TabIndex = 138;
            // 
            // cb_manv
            // 
            this.cb_manv.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_manv.FormattingEnabled = true;
            this.cb_manv.Location = new System.Drawing.Point(335, 202);
            this.cb_manv.Name = "cb_manv";
            this.cb_manv.Size = new System.Drawing.Size(236, 29);
            this.cb_manv.TabIndex = 139;
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
            this.btn_delete.Location = new System.Drawing.Point(906, 323);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(96, 37);
            this.btn_delete.TabIndex = 142;
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
            this.btn_update.Location = new System.Drawing.Point(705, 323);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(98, 37);
            this.btn_update.TabIndex = 141;
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
            this.btn_add.Location = new System.Drawing.Point(484, 323);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(102, 37);
            this.btn_add.TabIndex = 140;
            this.btn_add.Text = "Thêm";
            this.btn_add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_add.TextColor = System.Drawing.Color.White;
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // HoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1604, 881);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.cb_manv);
            this.Controls.Add(this.MAKH);
            this.Controls.Add(this.CB);
            this.Controls.Add(this.txtmaHD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtv_HD);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_THHTOAN);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Name = "HoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HoaDon";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HoaDon_Load);
            this.MdiChildActivate += new System.EventHandler(this.HoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtv_HD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.DataGridView dtv_HD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txt_THHTOAN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn Column12;
        public System.Windows.Forms.MaskedTextBox txtmaHD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CB;
        private System.Windows.Forms.ComboBox MAKH;
        private System.Windows.Forms.ComboBox cb_manv;
        private Custom.ButtonMyPham btn_delete;
        private Custom.ButtonMyPham btn_update;
        private Custom.ButtonMyPham btn_add;
    }
}