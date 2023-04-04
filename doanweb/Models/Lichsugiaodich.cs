using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanweb.Models
{
    public class Lichsugiaodich
    {
        dbSanPhamDataContext db = new dbSanPhamDataContext();
        public int mahd { set; get; }
        public string tensp { get; set; }
        public string hinhanh { get; set; }
        public string makh { set; get; }
        public string masp { get; set; }
        public int? iSoluong { get; set; }
        public decimal? tonggia { get; set; }
        public string ngaymua { get; set; }

        /*public Lichsugiaodich(string makh)
        {
            this.makh = makh;
            HOA_DON hd = db.HOA_DONs.SingleOrDefault(s=>s.MaKH == makh);
            this.mahd = hd.MaHÐ;
            CT_HD cthd = db.CT_HDs.SingleOrDefault(c => c.MaHÐ == hd.MaHÐ);
            SAN_PHAM sp = db.SAN_PHAMs.SingleOrDefault(p => p.MaSP == cthd.MaSP);
            this.tensp = sp.TenSP;
            iSoluong = cthd.SoLuong;
            tonggia = (double)cthd.TongGia;
        }*/
    }
}