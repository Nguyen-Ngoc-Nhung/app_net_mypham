using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMyPham.DTO
{
  public  class SANPHAM_DTO
    {
        string tENSP, mOTA, nSX, hINHANH, mALSP;

        public string MALSP
        {
            get { return mALSP; }
            set { mALSP = value; }
        }

        public string HINHANH
        {
            get { return hINHANH; }
            set { hINHANH = value; }
        }

        public string NSX
        {
            get { return nSX; }
            set { nSX = value; }
        }

        public string MOTA
        {
            get { return mOTA; }
            set { mOTA = value; }
        }

        public string TENSP
        {
            get { return tENSP; }
            set { tENSP = value; }
        }
        int sOLUONG;

        public int SOLUONG
        {
            get { return sOLUONG; }
            set { sOLUONG = value; }
        }
        float dONGIA;

        public float DONGIA
        {
            get { return dONGIA; }
            set { dONGIA = value; }
        }
    }
}
