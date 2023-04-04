using doanweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanweb.Controllers
{
    public class AccountController : Controller
    {
        dbSanPhamDataContext db = new dbSanPhamDataContext();
        // GET: Account
       
        [HttpGet]
        public ActionResult DangKy()
        {

            return View();


        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, TAI_KHOAN tk)
        {

            
            var tendangnhap = collection["TenDangNhapTK"];
            var matkhau = collection["MatKhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
           
            if (String.IsNullOrEmpty(MatKhauXacNhan))
            {
                ViewData["NhapMKXN"] = "Vui lòng nhập mật khẩu xác nhận";
            }
            else
            {
                if (!matkhau.Equals(MatKhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu khác nhau quá bạn êiii!";
                }
                else
                {
                    TAI_KHOAN tkif = db.TAI_KHOANs.SingleOrDefault(i => i.TenDangNhapTK == tendangnhap);
                    if(tkif == null)
                    {
                        tk.TenDangNhapTK = tendangnhap;
                        tk.MatKhau = matkhau;
                        Session["DangKyTemp"] = tendangnhap;
                        db.TAI_KHOANs.InsertOnSubmit(tk);
                        db.SubmitChanges();
                        return RedirectToAction("DangKyThongTin");
                    }
                    else
                    {
                        ViewData["ErrorTaiKhoan"] = "Tài khoản đã được sử dụng";
                    }
                   
                   

                    
                }
            }
            return this.DangKy();

        }
        public ActionResult DangKyThongTin()
        {

            return View();


        }
        [HttpPost]
        public ActionResult DangKyThongTin(FormCollection collection, KHACH_HANG kh)
        {

            var hoten = collection["HoTenKH"];
            var tendangnhap = Session["DangKyTemp"].ToString();
           
            var email = collection["Email"];
            var diachi = collection["DiaChi"];
            var dienthoai = collection["SDT"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);
            if (String.IsNullOrEmpty(tendangnhap))
            {
                ViewData["error"] = "Error!!!";
            }
            else
            {
                KHACH_HANG khif = db.KHACH_HANGs.SingleOrDefault(c => c.SDT == dienthoai);
                if(khif == null)
                {
                    kh.MaKH = "KH" + dienthoai;
                    kh.HoTenKH = hoten;
                    kh.TenDangNhapTK = Session["DangKyTemp"].ToString();
                    kh.Email = email;

                    kh.DiaChi = diachi;
                    kh.SDT = dienthoai;

                    db.KHACH_HANGs.InsertOnSubmit(kh);
                    db.SubmitChanges();
                    return RedirectToAction("DangNhap");
                }
                else
                {
                    ViewData["ErrorNum"] = "Số điện thoại đã được sử dụng";
                }

                    
                
            }
            return this.DangKyThongTin();

        }


        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var username = collection["TenDangNhapTK"];
            var matkhau = collection["MatKhau"];
            TAI_KHOAN kh = db.TAI_KHOANs.SingleOrDefault(h => h.TenDangNhapTK == username && h.MatKhau == matkhau);
            if (kh != null)
            {
                NHAN_VIEN nv = db.NHAN_VIENs.SingleOrDefault(n => n.TenDangNhapTK == username);
                if(nv != null)
                {
                    ViewBag.NhanVien = "Chào mừng trở lại";
                    Session["NhanVien"] = nv;

                   if(nv.MaCV.Contains("NV") == true) //So sánh chuỗi MaCV có chữ NV hay không, nếu đúng ( true ) thì trả về
                    {
                        Session["NhanVienCapThap"] = nv.MaCV;

                    }
                    else //Nếu không, ngược lại
                    {
                        Session["NhanVienCapCao"] = nv.MaCV;
                        
                    }

                    
                    /*Session["ChucVu"] = nv.MaCV;*/
                    
                    //Chuyển qua trang admin, thay Index với Products bằng Thống kê của trang Admin
                    return RedirectToAction("ThongKe", "ThongKe");
                }
                else
                {
                    ViewBag.ThongBao = "Vô mua kit luôn thôi bạn êii";
                    KHACH_HANG i = db.KHACH_HANGs.SingleOrDefault(log => log.TenDangNhapTK == username);
                    Session["KhachHang"] = i;
                    Session["TaiKhoan"] = i.MaKH;
                    return RedirectToAction("Index", "Home");
                }
        
            }
            else
            {
                ViewData["LoiDangNhap"] = "Tên đăng nhập hoặc mật khẩu không đúng";
            }

            return this.DangNhap();
        }

        public ActionResult DangXuat()
        {
            List<CTHDlst> lstChitiet = Session["CTHD"] as List<CTHDlst>;
            if(lstChitiet != null)
            {
                lstChitiet.Clear();
            }
            Session["KhachHang"] = null;
            Session["NhanVien"] = null;
            Session["CTHD"] = null;
            Session["MaHD"] = null;
            Session["HoaDonTam"] = null;
            Session["NhanVienCapCao"] = null;
            Session["NhanVienCapThap"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ThongTin(string taikhoan)
        {
            
            var all_info = from i in db.KHACH_HANGs select i;
            all_info = all_info.Where(s => s.MaKH == taikhoan);

            return View(all_info);
        }
        public ActionResult Index()
        {
            
            return View();
        }

    }
}