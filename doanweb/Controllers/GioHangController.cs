using doanweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanweb.Controllers
{
    public class GioHangController : Controller
    {
     //Phần giỏ hàng
        dbSanPhamDataContext data = new dbSanPhamDataContext();
        public KHACH_HANG layKhachHang()
        {
           
           
                KHACH_HANG kh = (KHACH_HANG)Session["KhachHang"];
                return kh;
          
            
        }
        public List<cart> layGioHang()
        {
          
            KHACH_HANG kh = layKhachHang();
            List<cart> listGioHang = data.carts.Where(n => n.MaKH == kh.MaKH).ToList();
            if (listGioHang == null)
            {
                listGioHang = new List<cart>();
            }
            return listGioHang;
        }

        public ActionResult themGioHang(string idaddcart, string strURL)
        {
            if (Session["KhachHang"] == null || Session["KhachHang"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                List<cart> listGioHang = layGioHang();
                cart sanpham = listGioHang.Find(n => n.MaSP == idaddcart);
                KHACH_HANG kh = layKhachHang();
                SAN_PHAM sp = data.SAN_PHAMs.FirstOrDefault(s => s.MaSP == idaddcart);
                if (kh == null || kh.ToString() == "")
                {
                    return RedirectToAction("DangNhap", "User");
                }
                else
                {
                    if (sanpham == null)
                    {
                        sanpham = new cart();
                        sanpham.MaSP = idaddcart;
                        sanpham.MaKH = kh.MaKH;
                        sanpham.SoluongMua = 1;
                        sanpham.Gia = sp.Gia;
                        data.carts.InsertOnSubmit(sanpham);
                        data.SubmitChanges();
                        return Redirect(strURL);
                    }
                    else
                    {
                        sanpham.SoluongMua++;

                        data.SubmitChanges();
                        sanpham.Gia = sanpham.SoluongMua * sp.Gia;
                        data.SubmitChanges();
                        return Redirect(strURL);
                    }
                }
            }
            
        }
        private int tongSoLuongSanPham()
        {
            int tsl = 0;
            List<cart> listGioHang = layGioHang();
            if (listGioHang != null)
            {
                tsl = Convert.ToInt32(listGioHang.Sum(n => n.SoluongMua));
            }
            return tsl;
        }

        
        private int tongSanPham()
        {
            int ssp = 0;
            List<cart> listGioHang = layGioHang();
            if (listGioHang != null)
            {
                ssp = listGioHang.Count;
            }
            return ssp;
        }
        private decimal tongGia()
        {
            decimal tonggia = 0;
            
            List<cart> listGioHang = layGioHang();
            if (listGioHang != null)
            {
                tonggia = Convert.ToDecimal(listGioHang.Sum(n => n.Gia));
            }
            return tonggia;
            
        }
        
        private decimal Gia1sp()
        {
            decimal gia = 0;
            List<cart> listGioHang = layGioHang();
            if (listGioHang != null)
            {
                foreach (var item in listGioHang)
                {
                    SAN_PHAM sp = data.SAN_PHAMs.SingleOrDefault(s => s.MaSP == item.MaSP);
                    gia = Convert.ToDecimal( 1 * sp.Gia);
                }
            }
            return gia;
        }
        public ActionResult GioHang()
        {
            if (Session["KhachHang"] == null || Session["KhachHang"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                List<cart> listGioHang = layGioHang();
                ViewBag.Tongsoluongsanpham = tongSoLuongSanPham();
                ViewBag.Tongsosanpham = tongSanPham();
                ViewBag.TongGia = tongGia();
                ViewBag.Gia1sp = Gia1sp();
                return View(listGioHang);
            }
            
        }
        public ActionResult XoaGioHang(string delcartid)
        {
            List<cart> listGioHang = layGioHang();
            cart sanpham = listGioHang.SingleOrDefault(n => n.MaSP == delcartid);
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.MaSP == delcartid);
                data.carts.DeleteOnSubmit(sanpham);
                data.SubmitChanges();
                return RedirectToAction("GioHang");

            }
            return RedirectToAction("GioHang");
        }
       
        public ActionResult CapnhatGioHang(string upid, FormCollection collection)
        {
            List<cart> listGioHang = layGioHang();
            cart sanpham = listGioHang.SingleOrDefault(n => n.MaSP == upid);
            if (sanpham != null)
            {
                sanpham.SoluongMua = int.Parse(collection["txtSoLg"].ToString());
                sanpham.Gia = int.Parse(collection["txtSoLg"].ToString()) * sanpham.SAN_PHAM.Gia;
                data.SubmitChanges();
            }
            
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<cart> listGioHang = layGioHang();
            foreach (var item in listGioHang)
            {
                data.carts.DeleteOnSubmit(item); data.SubmitChanges();
            }
            if (Session["XNDH"] != null)
            {
                Session["XNDH"] = null;
                return RedirectToAction("XacnhanDonhang", "Home");
            }
            return RedirectToAction("GioHang");
        }

 //Lấy giỏ hàng của payment
        public List<CTHDlst> Layhoadon()
        {
            List<CTHDlst> lstChitiet = Session["CTHD"] as List<CTHDlst>;
            if (lstChitiet == null)
            {
                lstChitiet = new List<CTHDlst>();
                Session["CTHD"] = lstChitiet;
            }
            return lstChitiet;
        }

        //Đặt hàng 
        public ActionResult Dathang()
        {
            List<cart> listGioHang = layGioHang();
            List<CTHDlst> lsthd = Layhoadon();
            HOA_DON hd = new HOA_DON();
            hd.NgayDat = DateTime.Now;
           
            hd.MaKH = (Session["KhachHang"] as KHACH_HANG).MaKH;
            hd.TongTienTT = (decimal)0;
            data.HOA_DONs.InsertOnSubmit(hd);
            data.SubmitChanges();
            Session["HoaDonTam"] = hd;
            return RedirectToAction("Paymentcart", "GioHang");
            
            
        }

        // Trang thanh toán của cart
        [HttpGet]
        public ActionResult Paymentcart()
        {
            if (Session["KhachHang"] == null || Session["KhachHang"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account");
            }

            List<cart> listGioHang = layGioHang();
            ViewBag.TongHoaDon = tongGia();
            ViewBag.TongSoLuong = tongSoLuongSanPham();
            ViewBag.Gia1sp = Gia1sp();
            return View(listGioHang);
        }
        [HttpPost]
        public ActionResult Paymentcart(FormCollection collection)
        {
            List<cart> listGioHang = layGioHang();

            HOA_DON hd = (HOA_DON)Session["HoaDonTam"];

            cart listid = listGioHang.Find(l => l.MaKH == hd.MaKH);
            List<CTHDlst> lsthd = Layhoadon();
            if (listid != null)
            {
                foreach (cart g in listGioHang)
                {
                    CT_HD cthd = new CT_HD();
                    cthd.NgayMua = String.Format("{0:dd/MM/yyyy}", DateTime.Now).ToString();
                    cthd.MaHÐ = hd.MaHÐ;
                    cthd.MaSP = g.MaSP;
                    SAN_PHAM sp = data.SAN_PHAMs.Single(n => n.MaSP == g.MaSP);
                    cthd.SoLuong = g.SoluongMua;
                    cthd.TongGia = sp.Gia * g.SoluongMua;
                    Session["SPDC"] = sp;
                    // Cập nhật số lượng
                    if (sp.SoLuong > 0)
                    {
                        
                        sp.SLTruyCap = sp.SLTruyCap + g.SoluongMua; //Cập nhật độ HOT của sản phẩm
                        sp.SoLuong = sp.SoLuong - g.SoluongMua;
                        data.CT_HDs.InsertOnSubmit(cthd);
                        data.SubmitChanges();
                      
                    }
                    else
                    {
                        ViewData["ErrorMuaHang"] = "Số lượng hàng không đủ";
                        return this.Paymentcart();
                    }
                    HOA_DON tongtt = data.HOA_DONs.SingleOrDefault(s => s.MaHÐ == hd.MaHÐ);
                    if (tongtt != null)
                    {
                        tongtt.ThanhToan = true;
                        tongtt.TongTienTT = tongtt.TongTienTT + cthd.TongGia;
                       
                        data.SubmitChanges();
                    }
                }

              
                

            }
            Session["XNDH"] = "Ok";
            if (listid != null)
            {
                return RedirectToAction("XoaTatCaGioHang", "GioHang");
            }
            
            return RedirectToAction("XacnhanDonhang", "Home");
        }

      

// Phần Mua ngay
        public ActionResult ChonSanPham(string idsanpham)
        {
            if (Session["KhachHang"] == null || Session["KhachHang"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                List<CTHDlst> lsthd = Layhoadon();
                HOA_DON hd = new HOA_DON();
                hd.NgayDat = DateTime.Now;
                
                hd.MaKH = (Session["KhachHang"] as KHACH_HANG).MaKH;
                hd.TongTienTT = (decimal)0;
                data.HOA_DONs.InsertOnSubmit(hd);
                data.SubmitChanges();

                HOA_DON tthd = data.HOA_DONs.SingleOrDefault(h => h.MaHÐ == hd.MaHÐ);
                int mahd = tthd.MaHÐ;
                Session["MaHD"] = tthd;
                CTHDlst sanpham = lsthd.Find(s => s.Mahd == mahd);
                if (sanpham == null)
                {
                    sanpham = new CTHDlst(idsanpham, mahd);
                    lsthd.Add(sanpham);
                    return RedirectToAction("Payment", "GioHang");
                }
                else
                {

                    return RedirectToAction("Payment", "GioHang");
                }
            }

        }

        [HttpGet]
        public ActionResult Payment()
        {
            if (Session["KhachHang"] == null || Session["KhachHang"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Account");
            }
            if (Session["CTHD"] == null || Session["CTHD"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            List<CTHDlst> lst = Layhoadon();
            ViewBag.TongHoaDon = tongGia();
            ViewBag.TongSoLuong = tongSoLuongSanPham();
            ViewBag.Gia1sp = Gia1sp();
            return View(lst);
        }
        [HttpPost]
        public ActionResult Payment(FormCollection collection)
        {
            List<cart> listGioHang = layGioHang();   
            List<CTHDlst> lsthd = Layhoadon();
            
                foreach (var item in lsthd)
                {
                    CT_HD cthd = new CT_HD();
                    cthd.NgayMua = String.Format("{0:dd/MM/yyyy}", DateTime.Now).ToString();
                    cthd.MaHÐ = item.Mahd;
                    cthd.MaSP = item.masp;
                    cthd.SoLuong = item.iSoluong;
                    cthd.TongGia = (decimal)item.tonggia;

                   
                    HOA_DON tongtt = data.HOA_DONs.SingleOrDefault(s => s.MaHÐ == item.Mahd);
                    if (tongtt.TongTienTT == 0)
                    {
                        tongtt.ThanhToan = true;
                        tongtt.TongTienTT = (decimal)item.tonggia;
                        data.SubmitChanges();
                    }
                // Cập nhật số lượng

                SAN_PHAM slt = data.SAN_PHAMs.Single(p => p.MaSP == item.masp);
                if (slt.SoLuong > 0)
                {
                    
                    slt.SLTruyCap = slt.SLTruyCap + item.iSoluong;
                    slt.SoLuong = slt.SoLuong - item.iSoluong;
                    Session["SPDC"] = slt;
                    data.CT_HDs.InsertOnSubmit(cthd);
                    data.SubmitChanges();
                    //xóa list sau mỗi lần thanh toán, tránh bị lặp đồ trong lần thanh toán tiếp theo
                    lsthd.Clear();
                    return RedirectToAction("XacnhanDonhang", "Home");
                }
                else
                {
                    ViewData["ErrorMuaHang"] = "Số lượng hàng không đủ";
                    return this.Payment();
                }
            }
                data.SubmitChanges();
                Session["CTHD"] = null;
            //xóa list sau mỗi lần thanh toán, tránh bị lặp đồ trong lần thanh toán tiếp theo
            lsthd.Clear();
            return RedirectToAction("XacnhanDonhang", "Home");
        }

        //Hủy đơn hàng đang tạo
        public ActionResult HuyDonHang(int delid)
        {
            List<CTHDlst> lstGioHang = Layhoadon();
            CTHDlst sanpham = lstGioHang.SingleOrDefault(n => n.Mahd == delid);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.Mahd == delid);
                var D_sanpham = data.HOA_DONs.Where(m => m.MaHÐ == delid).First();
                data.HOA_DONs.DeleteOnSubmit(D_sanpham);
                data.SubmitChanges();
                lstGioHang.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}