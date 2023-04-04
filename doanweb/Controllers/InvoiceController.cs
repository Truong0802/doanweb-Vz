using doanweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanweb.Controllers
{
    public class InvoiceController : Controller
    {
        dbSanPhamDataContext data = new dbSanPhamDataContext();
        // GET: ThongKe
        public ActionResult Invoice(int mahdid)
        {
            var all_thongke = from h in data.HOA_DONs
                              where h.MaHÐ == mahdid
                              join c in data.CT_HDs on h.MaHÐ equals c.MaHÐ
                              join k in data.KHACH_HANGs on h.MaKH equals k.MaKH 
                              join p in data.SAN_PHAMs on c.MaSP equals p.MaSP
                              group h by new
                              {
                                  
                                  k.HoTenKH,
                                  k.DiaChi,
                                  k.Email,
                                  k.SDT,
                                  h.ThanhToan,
                                  c.SoLuong,
                                  c.TongGia,
                                  c.NgayMua,
                                  h.TongTienTT,
                                  h.NgayGiao,
                                  h.NgayDat,
                                  p.TenSP,
                                  p.Gia
                              } into g
                              select new invoice
                              {
                                  hotenkh = g.Key.HoTenKH,
                                  diachi = g.Key.DiaChi,
                                  email = g.Key.Email,
                                  sdt = g.Key.SDT,
                                  
                                  tongtt = g.Key.TongGia,
                                  tongtien = g.Key.TongTienTT,
                                  ngaymua = g.Key.NgayMua,
                                  ngaydat = String.Format("{0:dd/MM/yyyy}", g.Key.NgayDat),
                                  ngaygiao = String.Format("{0:dd/MM/yyyy}", g.Key.NgayGiao),
                                  tensp = g.Key.TenSP,
                                  soluong = g.Key.SoLuong,
                                  giasp = g.Key.Gia,
                                  tinhtrangtt = g.Key.ThanhToan.ToString(),

                                  
                              };

            
          
            

            return View(all_thongke.ToList());

        }
    }
}