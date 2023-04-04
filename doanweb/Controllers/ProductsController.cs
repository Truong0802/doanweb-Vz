using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using doanweb.Models;
using PagedList;

namespace doanweb.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        dbSanPhamDataContext data = new dbSanPhamDataContext();
        public ActionResult Index(int? page, string searchString, int cateid = 0)
        {
            var all_sanpham = from s in data.SAN_PHAMs select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                all_sanpham = all_sanpham.Where(p => p.TenSP.Contains(searchString));
            }

            ViewBag.LoaiSP = new SelectList(data.Loai_SPs, "Ma_Loai", "TenLoai");
            if (cateid != 0)
            {
                var sanpham_with_category = data.Loai_SPs.Where(s => s.Ma_Loai == cateid).FirstOrDefault();
                all_sanpham = all_sanpham.Where(p => p.Ma_Loai == sanpham_with_category.Ma_Loai);
            }

            if (page == null)
                page = 1;
            /* var all_book = (from Sach in data.Saches select Sach).OrderBy(m => m.masach);*/
            int pageSize = 16;
            int pageNum = page ?? 1;
            return View(all_sanpham.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Create()
        {
            ViewBag.LoaiSP = new SelectList(data.Loai_SPs, "Ma_Loai", "TenLoai");
            ViewBag.MaPhanLoai = new SelectList(data.PhanLoais, "MaPhanLoai", "TenPhanLoai");
            ViewBag.MaThuongHieu = new SelectList(data.THUONG_HIEUs, "Ma_TH", "TenTH");
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SAN_PHAM s, int cateid = 0,int idtypeSP = 0,int brandid = 0)
        {
            var E_masp = collection["MaSP"];
            var E_tensp = collection["TenSP"];
            var E_hinhsp = collection["HinhAnhSP"];
            var E_hinhphu1 = collection["HinhAnhCT1"];
            var E_hinhphu2 = collection["HinhAnhCT2"];
            var E_hinhphu3 = collection["HinhAnhCT3"];
            var E_hinhphu4 = collection["HinhAnhCT4"];
            var E_giaban = Convert.ToDecimal(collection["Gia"]);
            var E_soluong = Convert.ToInt32(collection["SoLuong"]);
            var E_id = Convert.ToInt32(collection["ID"]);
            if (string.IsNullOrEmpty(E_masp))
            {
                ViewData["Error"] = "Don't empty";
            }
            else 
            {
                if (idtypeSP != 0 )
                {
                    s.Ma_Loai = idtypeSP;
                }
                if(cateid != 0 )
                {
                    s.MaPhanLoai = cateid;
                }    
                if(brandid !=0)
                {
                    s.Ma_TH = brandid;
                }
                s.SLTruyCap = 0;
                s.MaSP = E_masp.ToString();
                s.TenSP = E_tensp.ToString();
                s.HinhAnhSP = E_hinhsp.ToString();
                s.HinhAnhCT1 = E_hinhphu1.ToString();
                s.HinhAnhCT2 = E_hinhphu2.ToString();
                s.HinhAnhCT3 = E_hinhphu3.ToString();
                s.HinhAnhCT4 = E_hinhphu4.ToString();
                s.Gia = E_giaban;
                s.SoLuong = E_soluong;
                data.SAN_PHAMs.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }

        public ActionResult Edit(string id)
        {
            ViewBag.LoaiSP = new SelectList(data.Loai_SPs, "Ma_Loai", "TenLoai");
            ViewBag.MaPhanLoai = new SelectList(data.PhanLoais, "MaPhanLoai", "TenPhanLoai");
            ViewBag.MaThuongHieu = new SelectList(data.THUONG_HIEUs, "Ma_TH", "TenTH");
            var D_sanpham = data.SAN_PHAMs.Where(m => m.MaSP.StartsWith(id)).First();
            return View(D_sanpham);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_sanpham = data.SAN_PHAMs.First(m => m.MaSP == id);
            var E_masp = collection["MaSP"];
            var E_tensp = collection["TenSP"];
            var E_hinhsp = collection["HinhAnhSP"];
            var E_hinhphu1 = collection["HinhAnhCT1"];
            var E_hinhphu2 = collection["HinhAnhCT2"];
            var E_hinhphu3 = collection["HinhAnhCT3"];
            var E_hinhphu4 = collection["HinhAnhCT4"];
            var E_giaban = Convert.ToDecimal(collection["Gia"]);
            var E_soluong = Convert.ToInt32(collection["SoLuong"]);
            var E_id = Convert.ToInt32(collection["ID"]);
            if (string.IsNullOrEmpty(E_masp))
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                E_sanpham.MaSP = E_masp.ToString();
                E_sanpham.TenSP = E_tensp.ToString();
                E_sanpham.HinhAnhSP = E_hinhsp.ToString();
                E_sanpham.HinhAnhCT1 = E_hinhphu1.ToString();
                E_sanpham.HinhAnhCT2 = E_hinhphu2.ToString();
                E_sanpham.HinhAnhCT3 = E_hinhphu3.ToString();
                E_sanpham.HinhAnhCT4 = E_hinhphu4.ToString();
                E_sanpham.Gia = E_giaban;
                E_sanpham.SoLuong = E_soluong;
                UpdateModel(E_sanpham);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(string id)
        {
            var D_Sach = data.SAN_PHAMs.First(m => m.MaSP == id);
            return View(D_Sach);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_Sach = data.SAN_PHAMs.Where(m => m.MaSP == id).First();
            data.SAN_PHAMs.DeleteOnSubmit(D_Sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}