using doanweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace doanweb.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        dbSanPhamDataContext data = new dbSanPhamDataContext();
        public ActionResult Index(int? page, string searchString, string cateid = null)
        {
            var all_khachhang = from s in data.KHACH_HANGs select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                all_khachhang = all_khachhang.Where(p => p.HoTenKH.Contains(searchString));
            }

            if (page == null)
                page = 1;
            int pageSize = 16;
            int pageNum = page ?? 1;
            return View(all_khachhang.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Detail(string id)
        {
            var D_sanpham = data.KHACH_HANGs.Where(m => m.MaKH.StartsWith(id)).First();
            return View(D_sanpham);
        }
        [HttpPost]
        public ActionResult Detail(string id, FormCollection collection)
        {
            var E_khachhang = data.KHACH_HANGs.First(m => m.MaKH == id);
            var E_tendangnhap = collection["TenDangNhapTK"];
            var E_hoten = collection["HoTenKH"];
            var E_sdt = collection["SDT"];
            var E_diachi = collection["DiaChi"];
            var E_email = collection["Email"];
            if (string.IsNullOrEmpty(E_tendangnhap))
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                E_khachhang.TenDangNhapTK = E_tendangnhap.ToString();
                E_khachhang.HoTenKH = E_hoten.ToString();
                E_khachhang.SDT = E_sdt.ToString();
                E_khachhang.DiaChi = E_diachi.ToString();
                E_khachhang.Email = E_email.ToString();
                UpdateModel(E_khachhang);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Detail(id);
        }
        public ActionResult Delete(string id)
        {
            var D_Sach = data.KHACH_HANGs.First(m => m.MaKH == id);
            return View(D_Sach);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_Sach = data.KHACH_HANGs.Where(m => m.MaKH == id).First();
            data.KHACH_HANGs.DeleteOnSubmit(D_Sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}