using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMyPham.DTO
{
  public  class TAIKHOAN_DTO:THONGTINCANHAN_DTO
    {
        string tENtk, mATKHAU;

        public string MATKHAU
        {
            get { return mATKHAU; }
            set { mATKHAU = value; }
        }

        public string TENtk
        {
            get { return tENtk; }
            set { tENtk = value; }
        }
        int mAQUYEN;

        public int MAQUYEN
        {
            get { return mAQUYEN; }
            set { mAQUYEN = value; }
        }
    }
}
