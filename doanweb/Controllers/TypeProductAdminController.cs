using doanweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanweb.Controllers
{
    public class TypeProductAdminController : Controller
    {
        dbSanPhamDataContext data = new dbSanPhamDataContext();
        // GET: TypeProductAdmin
        public ActionResult DanhSachDanhMuc()
        {
            var all_thongke = from l in data.Loai_SPs select l;
                              
                            

            return View(all_thongke.ToList());
        }

        public ActionResult DanhSachThuongHieu()
        {
            var all_thongke = from l in data.THUONG_HIEUs select l;



            return View(all_thongke.ToList());
        }



//Xóa
        public ActionResult Delete(int idtype)
        {
            var D_Sach = data.Loai_SPs.First(m => m.Ma_Loai == idtype);
            return View(D_Sach);
        }
        [HttpPost]
        public ActionResult Delete(int idtype, FormCollection collection)
        {
            var D_Sach = data.Loai_SPs.Where(m => m.Ma_Loai == idtype).First();
            data.Loai_SPs.DeleteOnSubmit(D_Sach);
            data.SubmitChanges();
            return RedirectToAction("DanhSachDanhMuc");
        }

        public ActionResult DeleteTH(int idbrand)
        {
            var D_Sach = data.THUONG_HIEUs.First(m => m.Ma_TH == idbrand);
            return View(D_Sach);
        }
        [HttpPost]
        public ActionResult DeleteTH(int idbrand, FormCollection collection)
        {
            var D_Sach = data.THUONG_HIEUs.Where(m => m.Ma_TH == idbrand).First();
            data.THUONG_HIEUs.DeleteOnSubmit(D_Sach);
            data.SubmitChanges();
            return RedirectToAction("DanhSachThuongHieu");
        }



//Thêm
        public ActionResult ThemDanhMuc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemDanhMuc(FormCollection collection, Loai_SP s)
        {

            var E_tendm = collection["TenLoai"];
            if (string.IsNullOrEmpty(E_tendm))
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                
                s.TenLoai = E_tendm;
                data.Loai_SPs.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("DanhSachDanhMuc");
            }
            return this.ThemDanhMuc();
        }

        public ActionResult ThemTH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemTH(FormCollection collection, THUONG_HIEU s)
        {

            var E_tenth = collection["TenTH"];
            if (string.IsNullOrEmpty(E_tenth))
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {

                s.TenTH = E_tenth;
                data.THUONG_HIEUs.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("DanhSachThuongHieu");
            }
            return this.ThemDanhMuc();
        }
    }
}