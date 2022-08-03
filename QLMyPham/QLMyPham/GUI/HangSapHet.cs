using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using QLMyPham.Report;
using QLMyPham.BUS;
using CrystalDecisions.Shared;
namespace QLMyPham.GUI
{
    public partial class HangSapHet : Form
    {
        public HangSapHet()
        {
            InitializeComponent();
        }
        SANPHAM_BUS SP = new SANPHAM_BUS();
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            //Bill_CrystalReports rpt = new Bill_CrystalReports();
            SanPhamHetHang rpt = new SanPhamHetHang();

            crystalReportViewer1.ReportSource = rpt;//load lên report

            //TRUYỀN DỮ LIỆU VÀO CRYSTAL REPORT THÔNG QUA DATASET

            rpt.SetDataSource(SP.getSANPHAMhethang());//lấy dữ liệu load từ hóa đơn bán hàng

            rpt.SetDatabaseLogon("sa", "123", "DESKTOP-TGJ3UQT", "MYPHAM");//đăng nhập sẵn

            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.Refresh();
        }
    }
}
