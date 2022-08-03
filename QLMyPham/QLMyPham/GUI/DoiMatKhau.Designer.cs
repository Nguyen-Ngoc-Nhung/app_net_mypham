namespace QLMyPham.GUI
{
    partial class DoiMatKhau
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
            this.components = new System.ComponentModel.Container();
            this.txtReNewPass = new System.Windows.Forms.MaskedTextBox();
            this.txtNewPass = new System.Windows.Forms.MaskedTextBox();
            this.txtOldPass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.vbButton2 = new QLMyPham.Custom.ButtonMyPham();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtReNewPass
            // 
            this.txtReNewPass.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtReNewPass.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReNewPass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtReNewPass.Location = new System.Drawing.Point(360, 210);
            this.txtReNewPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtReNewPass.Name = "txtReNewPass";
            this.txtReNewPass.Size = new System.Drawing.Size(236, 27);
            this.txtReNewPass.TabIndex = 119;
            this.txtReNewPass.UseSystemPasswordChar = true;
            this.txtReNewPass.Leave += new System.EventHandler(this.txtReNewPass_Leave);
            // 
            // txtNewPass
            // 
            this.txtNewPass.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNewPass.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtNewPass.Location = new System.Drawing.Point(360, 143);
            this.txtNewPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Size = new System.Drawing.Size(236, 27);
            this.txtNewPass.TabIndex = 118;
            this.txtNewPass.UseSystemPasswordChar = true;
            this.txtNewPass.Leave += new System.EventHandler(this.txtNewPass_Leave);
            // 
            // txtOldPass
            // 
            this.txtOldPass.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtOldPass.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldPass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtOldPass.Location = new System.Drawing.Point(360, 82);
            this.txtOldPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.Size = new System.Drawing.Size(236, 27);
            this.txtOldPass.TabIndex = 117;
            this.txtOldPass.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(180, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 22);
            this.label6.TabIndex = 116;
            this.label6.Text = "Mật khẩu mới";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(118, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 22);
            this.label5.TabIndex = 115;
            this.label5.Text = "Nhập lại mật khẩu mới";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(192, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 22);
            this.label3.TabIndex = 114;
            this.label3.Text = "Mật khẩu cũ";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Yellow;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkBox1.Image = global::QLMyPham.Properties.Resources.eye__2_;
            this.checkBox1.Location = new System.Drawing.Point(616, 82);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(45, 30);
            this.checkBox1.TabIndex = 121;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Yellow;
            this.checkBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkBox2.Image = global::QLMyPham.Properties.Resources.eye__2_;
            this.checkBox2.Location = new System.Drawing.Point(616, 143);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(45, 30);
            this.checkBox2.TabIndex = 122;
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.BackColor = System.Drawing.Color.Yellow;
            this.checkBox3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkBox3.Image = global::QLMyPham.Properties.Resources.eye__2_;
            this.checkBox3.Location = new System.Drawing.Point(616, 209);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(45, 30);
            this.checkBox3.TabIndex = 123;
            this.checkBox3.UseVisualStyleBackColor = false;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // vbButton2
            // 
            this.vbButton2.BackColor = System.Drawing.Color.Crimson;
            this.vbButton2.BackgroundColor = System.Drawing.Color.Crimson;
            this.vbButton2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton2.BorderRadius = 20;
            this.vbButton2.BorderSize = 0;
            this.vbButton2.FlatAppearance.BorderSize = 0;
            this.vbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton2.ForeColor = System.Drawing.Color.White;
            this.vbButton2.Image = global::QLMyPham.Properties.Resources.admin;
            this.vbButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vbButton2.Location = new System.Drawing.Point(326, 307);
            this.vbButton2.Name = "vbButton2";
            this.vbButton2.Size = new System.Drawing.Size(157, 37);
            this.vbButton2.TabIndex = 157;
            this.vbButton2.Text = "Đổi mật khẩu";
            this.vbButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.vbButton2.TextColor = System.Drawing.Color.White;
            this.vbButton2.UseVisualStyleBackColor = false;
            this.vbButton2.Click += new System.EventHandler(this.vbButton2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // DoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.BackgroundImage = global::QLMyPham.Properties.Resources.session1;
            this.ClientSize = new System.Drawing.Size(816, 374);
            this.Controls.Add(this.vbButton2);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtReNewPass);
            this.Controls.Add(this.txtNewPass);
            this.Controls.Add(this.txtOldPass);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Name = "DoiMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DoiMatKhau";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MaskedTextBox txtReNewPass;
        public System.Windows.Forms.MaskedTextBox txtNewPass;
        public System.Windows.Forms.TextBox txtOldPass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private Custom.ButtonMyPham vbButton2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}