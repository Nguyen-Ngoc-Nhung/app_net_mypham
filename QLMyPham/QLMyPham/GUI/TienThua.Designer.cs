namespace QLMyPham.GUI
{
    partial class TienThua
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TienThua));
            this.label1 = new System.Windows.Forms.Label();
            this.lblTienThua = new System.Windows.Forms.Label();
            this.btnThoat = new QLMyPham.Custom.ButtonMyPham();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tiền trả khách";
            // 
            // lblTienThua
            // 
            this.lblTienThua.AutoSize = true;
            this.lblTienThua.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienThua.ForeColor = System.Drawing.Color.Red;
            this.lblTienThua.Location = new System.Drawing.Point(94, 129);
            this.lblTienThua.Name = "lblTienThua";
            this.lblTienThua.Size = new System.Drawing.Size(53, 21);
            this.lblTienThua.TabIndex = 1;
            this.lblTienThua.Text = "label2";
            this.lblTienThua.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnThoat.BackgroundColor = System.Drawing.Color.MediumAquamarine;
            this.btnThoat.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnThoat.BorderRadius = 20;
            this.btnThoat.BorderSize = 0;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThoat.Location = new System.Drawing.Point(98, 193);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(106, 36);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Text = "Đồng ý";
            this.btnThoat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnThoat.TextColor = System.Drawing.Color.White;
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // TienThua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(316, 285);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.lblTienThua);
            this.Controls.Add(this.label1);
            this.Name = "TienThua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TienThua";
            this.Load += new System.EventHandler(this.TienThua_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTienThua;
        private Custom.ButtonMyPham btnThoat;
    }
}