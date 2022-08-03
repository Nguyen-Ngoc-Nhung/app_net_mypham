using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMyPham.DTO
{
  public  class NHANVIEN_DTO : TAIKHOAN_DTO
    {

        string idNV;
        double doanhThu;

        public double DoanhThu
        {
            get { return doanhThu; }
            set { doanhThu = value; }
        }

        public string IdNV
        {
            get { return idNV; }
            set { idNV = value; }
        }
    }
}
