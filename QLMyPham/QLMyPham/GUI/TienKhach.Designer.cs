namespace QLMyPham.GUI
{
    partial class TienKhach
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
            this.lbNhan = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.vbButton2 = new QLMyPham.Custom.ButtonMyPham();
            this.SuspendLayout();
            // 
            // lbNhan
            // 
            this.lbNhan.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNhan.Location = new System.Drawing.Point(34, 20);
            this.lbNhan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNhan.Name = "lbNhan";
            this.lbNhan.Size = new System.Drawing.Size(244, 27);
            this.lbNhan.TabIndex = 8;
            this.lbNhan.Text = "Tiền của khách";
            this.lbNhan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Ivory;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Location = new System.Drawing.Point(60, 78);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 31);
            this.textBox1.TabIndex = 9;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // vbButton2
            // 
            this.vbButton2.BackColor = System.Drawing.Color.LawnGreen;
            this.vbButton2.BackgroundColor = System.Drawing.Color.LawnGreen;
            this.vbButton2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton2.BorderRadius = 20;
            this.vbButton2.BorderSize = 0;
            this.vbButton2.FlatAppearance.BorderSize = 0;
            this.vbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton2.ForeColor = System.Drawing.Color.DarkBlue;
            this.vbButton2.Image = global::QLMyPham.Properties.Resources._014_cashier;
            this.vbButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vbButton2.Location = new System.Drawing.Point(77, 151);
            this.vbButton2.Name = "vbButton2";
            this.vbButton2.Size = new System.Drawing.Size(139, 37);
            this.vbButton2.TabIndex = 159;
            this.vbButton2.Text = "Xác nhận";
            this.vbButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.vbButton2.TextColor = System.Drawing.Color.DarkBlue;
            this.vbButton2.UseVisualStyleBackColor = false;
            this.vbButton2.Click += new System.EventHandler(this.vbButton2_Click);
            // 
            // TienKhach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(306, 220);
            this.Controls.Add(this.vbButton2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbNhan);
            this.Name = "TienKhach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TienKhach";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbNhan;
        private System.Windows.Forms.TextBox textBox1;
        private Custom.ButtonMyPham vbButton2;
    }
}