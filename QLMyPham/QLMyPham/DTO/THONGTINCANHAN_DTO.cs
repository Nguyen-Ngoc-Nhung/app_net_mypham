using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMyPham.DTO
{
    public class THONGTINCANHAN_DTO
    {
        string idTTCN, hoTen, ngSinh, gioiTinh, email, sdt, dChi, ngayTao;

        public string NgayTao
        {
            get { return ngayTao; }
            set { ngayTao = value; }
        }

        public THONGTINCANHAN_DTO()
        {
            this.hoTen = string.Empty;
            this.ngSinh = "1/1/2001";
            this.gioiTinh = "Nam";
            this.email = string.Empty;
            this.sdt = string.Empty;
            this.dChi = string.Empty;
        }

        public string NgSinh
        {
            get { return ngSinh; }
            set { ngSinh = value; }
        }

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Sdt
        {
            get { return sdt; }
            set { sdt = value; }
        }

        public string DChi
        {
            get { return dChi; }
            set { dChi = value; }
        }

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }

        public string IdTTCN
        {
            get { return idTTCN; }
            set { idTTCN = value; }
        }
    }
}
