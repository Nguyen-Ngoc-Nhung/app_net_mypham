using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMyPham.DTO
{
 public    class HOADON_DTO
    {
        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        string manv, ngaytao, makh, tinhtrang;

        public string Tinhtrang
        {
            get { return tinhtrang; }
            set { tinhtrang = value; }
        }

        public string Makh
        {
            get { return makh; }
            set { makh = value; }
        }

        public string Ngaytao
        {
            get { return ngaytao; }
            set { ngaytao = value; }
        }

        public string Manv
        {
            get { return manv; }
            set { manv = value; }
        }
        float thanhtoan;

        public float Thanhtoan
        {
            get { return thanhtoan; }
            set { thanhtoan = value; }
        }
    }
}
