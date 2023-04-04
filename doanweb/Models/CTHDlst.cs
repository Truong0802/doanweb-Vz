using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanweb.Models
{
    public class CTHDlst
    {
        dbSanPhamDataContext db = new dbSanPhamDataContext();
        public int Mahd { get; set; }   
        public string tensp { get; set; }

        public string masp { get; set; }
        public int iSoluong { get; set; }
        public Double tonggia { get; set; }
        

        public CTHDlst(string idsanpham, int mahd)
        {
            Mahd = mahd;
            masp = idsanpham;
            SAN_PHAM sp = db.SAN_PHAMs.SingleOrDefault(s => s.MaSP == idsanpham);
            tensp = sp.TenSP;
            tonggia = double.Parse(sp.Gia.ToString());
            iSoluong = 1;
        }

    }
}