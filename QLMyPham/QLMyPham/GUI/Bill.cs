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
    public partial class Bill : Form
    {
        HOADON_BUS hd = new HOADON_BUS();
        int MAHDBH = BanHang.MAHDBH;
        public Bill()
        {
            InitializeComponent();
        }
        public string tongtien { get; set; }
        public string giamgia { get; set; }
        private void Bill_Load(object sender, EventArgs e)
        {
            //Bill_CrystalReports rpt = new Bill_CrystalReports();
            bill rpt = new bill();

            crystalReportViewer1.ReportSource = rpt;//load lên report

            //TRUYỀN DỮ LIỆU VÀO CRYSTAL REPORT THÔNG QUA DATASET

            rpt.SetDataSource(hd.getHDBill(MAHDBH));//lấy dữ liệu load từ hóa đơn bán hàng
           
            rpt.SetDatabaseLogon("sa", "123", "DESKTOP-TGJ3UQT", "MYPHAM");//đăng nhập sẵn


            //TRUYỀN DỮ LIỆU VÀO CRYSTAL REPORT THÔNG QUA PARAMETER FIELD

            ParameterValues a = new ParameterValues();//khai báo đối tượng thuộc lớp này để chứa dữ liệu rời rạc
            ParameterDiscreteValue b = new ParameterDiscreteValue();
            b.Value = tongtien;
            a.Add(b);
            rpt.DataDefinition.ParameterFields["txt_tongtien"].ApplyCurrentValues(a);//khai báo đối tượng thuộc lớp này để tìm đến định nghĩa

            ParameterValues c = new ParameterValues();
            ParameterDiscreteValue d = new ParameterDiscreteValue();
            d.Value = giamgia;
            c.Add(d);
            rpt.DataDefinition.ParameterFields["txt_giamgia"].ApplyCurrentValues(c);
           
            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.Refresh();
        }
    }
}
