using doanweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanweb.Controllers
{
    public class ThongKeController : Controller
    {
        dbSanPhamDataContext data = new dbSanPhamDataContext();
        // GET: ThongKe
        public ActionResult ThongKe()
        {
            var all_thongke = from c in data.CT_HDs
                              join h in data.HOA_DONs on c.MaHÐ equals h.MaHÐ
                              group c by new { c.MaHÐ, h.ThanhToan } into g
                              select new ThongKe
                              {
                                  mahd = g.Key.MaHÐ,
                                  tonggia = g.Sum(c => c.TongGia),
                                  tinhtrangthanhtoan = g.Key.ThanhToan
                                  
                              };

            int? soluongdaban = data.CT_HDs.Sum(s => s.SoLuong);
            decimal? thunhap = data.CT_HDs.Sum(s => s.TongGia);

            ViewBag.soluongdaban = soluongdaban;
            ViewBag.thunhap = thunhap;

            return View(all_thongke.ToList());

        }


        /*// GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }*/
    }
}