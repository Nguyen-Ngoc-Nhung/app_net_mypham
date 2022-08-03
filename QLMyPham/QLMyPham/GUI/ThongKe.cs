using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QLMyPham.BUS;
using QLMyPham.DTO;
namespace QLMyPham.GUI
{
    public partial class ThongKe : Form
    {
         NHANVIEN_BUS   ado_nv = new NHANVIEN_BUS();
         SANPHAM_BUS    ado_sp = new SANPHAM_BUS();
         HOADON_BUS     ado_hd = new HOADON_BUS();


        public ThongKe()
        {
            InitializeComponent();
        }


        private void ThongKe_Load(object sender, EventArgs e)
        {
            txtNam.Text = DateTime.Today.Year.ToString();
            txtNam.Focus();
            btnXacNhan.PerformClick();
        }

        private void txtNam_TextChanged(object sender, EventArgs e)
        {
            if (txtNam.Text.Length > 0)
            {
                char phantucuoi = txtNam.Text[txtNam.Text.Length - 1];
                if (char.IsDigit(phantucuoi) == false)
                {
                    MessageBox.Show("Bạn nhập không phải số");
                    txtNam.Text = "";
                    txtNam.Focus();
                }
            }
        }

        private void vbButton2_Click(object sender, EventArgs e)
        {
            if (this.txtNam.Text.Trim().Length == 0) // kiểm tra rỗng 
            {
                MessageBox.Show("Vui lòng nhập năm");
                this.txtNam.Focus();
                return;
            }

            chartNhanVien.Series["sNV"].Points.Clear();
            chartSanPham.Series["sSP"].Points.Clear();

            chartNhanVien.Series["sNV"].LabelForeColor = Color.White;
            chartSanPham.Series["sSP"].LabelForeColor = Color.White;

            chartNhanVien.Titles["ttNV"].Text = string.Format("Doanh Thu Nhân Viên ({0})", this.txtNam.Text);
            chartSanPham.Titles["ttSP"].Text = string.Format("Sản Phẩm Bán Chạy ({0})", this.txtNam.Text);

            int y = int.Parse(txtNam.Text);

            List<NHANVIEN_DTO> lstNV = ado_nv.GetDoanhThu(y);

            lstNV.ForEach(nv =>
            {
                chartNhanVien.Series["sNV"].Points.AddXY(nv.HoTen, nv.DoanhThu);
            });

            List<SANPHAM_DTO> lstSP = ado_sp.GetSPBanChay(y);

            chartSanPham.Series["sSP"].IsValueShownAsLabel = true;
            lstSP.ForEach(sp =>
            {
                chartSanPham.Series["sSP"].Points.AddXY(sp.TENSP, sp.SOLUONG);
            });

            // doanh thu công ty
            try
            {
                if (chartDoanhThu.ChartAreas["DoanhThu"] != null)
                    chartDoanhThu.ChartAreas.Remove(chartDoanhThu.ChartAreas["DoanhThu"]);
            }
            catch (Exception)
            {
            }

            // chartArea
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "DoanhThu";
            chartDoanhThu.ChartAreas.Add(chartArea);
            chartArea.BackColor = Color.Azure;
            chartArea.BackGradientStyle = GradientStyle.HorizontalCenter;
            chartArea.BackHatchStyle = ChartHatchStyle.LargeGrid;
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BorderWidth = 1;
            chartArea.ShadowColor = Color.Purple;
            chartArea.ShadowOffset = 0;
            chartDoanhThu.ChartAreas[0].Axes[0].MajorGrid.Enabled = false;//x axis
            chartDoanhThu.ChartAreas[0].Axes[1].MajorGrid.Enabled = true;//y axis

            //Cursor：only apply the top area
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.AxisType = AxisType.Primary;//act on primary x axis
            chartArea.CursorX.Interval = 1;
            chartArea.CursorX.LineWidth = 1;
            chartArea.CursorX.LineDashStyle = ChartDashStyle.Dash;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.SelectionColor = Color.Yellow;
            chartArea.CursorX.AutoScroll = true;

            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.AxisType = AxisType.Primary;//act on primary y axis
            chartArea.CursorY.Interval = 1;
            chartArea.CursorY.LineWidth = 1;
            chartArea.CursorY.LineDashStyle = ChartDashStyle.Dash;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.SelectionColor = Color.Yellow;
            chartArea.CursorY.AutoScroll = true;

            // Axis
            chartArea.AxisY.Minimum = 0d;//Y axis Minimum value
            chartArea.AxisY.Title = @"Doanh thu";
            chartArea.AxisX.Minimum = 1d; //X axis Minimum value
            chartArea.AxisX.Maximum = 12d;
            chartArea.AxisX.IsLabelAutoFit = true;
            chartArea.AxisX.LabelAutoFitMaxFontSize = 12;
            chartArea.AxisX.LabelStyle.Angle = 0;
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;//show the last label
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chartArea.AxisX.IntervalType = DateTimeIntervalType.NotSet;
            chartArea.AxisX.Title = @"Tháng";
            chartArea.AxisX.TextOrientation = TextOrientation.Auto;
            chartArea.AxisX.LineWidth = 1;
            chartArea.AxisX.LineColor = Color.DarkOrchid;
            chartArea.AxisX.Enabled = AxisEnabled.True;
            chartArea.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Months;
            chartArea.AxisX.ScrollBar = new AxisScrollBar();

            chartDoanhThu.Series.Clear();

            for (int i = y; i >= y - 2; i--)
            {
                Series series1 = new Series();
                List<HOADON_DTO> lstHD = ado_hd.GetDoanhThuTheoNam(i);
                //Series Đường
                series1.ChartArea = "DoanhThu";
                chartDoanhThu.Series.Add(series1);
                //Series style
                series1.Name = @"series：Doanh Thu " + i.ToString();
                series1.ChartType = SeriesChartType.Line;  // type
                series1.BorderWidth = 4;
                series1.XValueType = ChartValueType.Int32;//x axis type
                series1.YValueType = ChartValueType.Int32;//y axis type

                //Marker
                series1.MarkerStyle = MarkerStyle.Circle;
                series1.MarkerSize = 10;
                series1.MarkerStep = 1;

                //Label
                series1.IsValueShownAsLabel = true;
                series1.SmartLabelStyle.Enabled = false;
                series1.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
                series1.LabelForeColor = Color.DarkRed;
                series1.LabelToolTip = @"Doanh thu";

                //Empty Point Style 
                DataPointCustomProperties p = new DataPointCustomProperties();
                series1.EmptyPointStyle = p;

                //Legend
                series1.LegendText = "Doanh Thu Năm " + i.ToString();
                series1.LegendToolTip = @"Thuộc tính";

                float[] values = lstHD.Select(hd => hd.Thanhtoan).ToArray();

                int x = 1;
                foreach (var v in values)
                {
                    series1.Points.AddXY(x, v);
                    x++;
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
