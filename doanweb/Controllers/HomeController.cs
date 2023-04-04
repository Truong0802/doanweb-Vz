using doanweb.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace doanweb.Controllers
{
    public class HomeController : Controller
    {
        dbSanPhamDataContext data = new dbSanPhamDataContext();
        public ActionResult Index(int? page, string searchString)
        {
            var all_book = from s in data.SAN_PHAMs select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                all_book = all_book.Where(p => p.TenSP.Contains(searchString));
            }

            if (page == null)
                page = 1;
            int pageSize = 8;
            int pageNum = page ?? 1;
            return View(all_book.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Detail(string id)
        {
            var D_sanpham = data.SAN_PHAMs.Where(m => m.MaSP.StartsWith(id)).First();
            
            return View(D_sanpham);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


      
        public ActionResult XacnhanDonhang()
        {
            int mapl = (Session["SPDC"] as SAN_PHAM).MaPhanLoai;
            /*var spdc = (from p in data.SAN_PHAMs where p.MaPhanLoai == mapl select p).OrderBy( m => m.Gia); *///Sắp xếp theo giá
            var spdc = (from p in data.SAN_PHAMs where p.MaPhanLoai == mapl select p).OrderByDescending(m => m.SLTruyCap);
            int pageSize = 3;
            
            return View(spdc.ToPagedList(1, pageSize));
        }

        //Danh sách sản phẩm
        public ActionResult Productlist(int? page, string searchString, int categoryid = 0,int categorytypeid = 0,int brandid = 0)
        {
            var all_book = from s in data.SAN_PHAMs select s;
            ViewBag.Loai = new SelectList(data.Loai_SPs, "Ma_Loai", "TenLoai");
            ViewBag.PLoai = new SelectList(data.PhanLoais, "MaPhanLoai", "TenPhanLoai");
            ViewBag.TH = new SelectList(data.THUONG_HIEUs, "Ma_TH", "TenTH");
            
            if (categoryid != 0)
            {
                all_book = (IOrderedQueryable<SAN_PHAM>)all_book.Where(i => i.Ma_Loai == categoryid);
            }
            if (categorytypeid != 0)
            {
                all_book = (IOrderedQueryable<SAN_PHAM>)all_book.Where(i => i.MaPhanLoai == categorytypeid);
            }
            if (brandid != 0)
            {
                all_book = (IOrderedQueryable<SAN_PHAM>)all_book.Where(i => i.Ma_TH == brandid);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                all_book = all_book.Where(p => p.TenSP.Contains(searchString));
            }

            if (page == null)
                page = 1;
            int pageSize = 8;
            int pageNum = page ?? 1;
            return View(all_book.ToPagedList(pageNum, pageSize));
        }

        

        //Lịch sử giao dịch
 
        
        public ActionResult LichSuGiaoDich(string taikhoangiaodich)
        {
           
            var hdList = from c in data.CT_HDs
                         join h in data.HOA_DONs on c.MaHÐ equals h.MaHÐ
                         where h.MaKH == taikhoangiaodich
                         join p in data.SAN_PHAMs on c.MaSP equals p.MaSP
                         group c by new { h.MaHÐ, p.TenSP, c.SoLuong, c.NgayMua, c.TongGia, p.HinhAnhSP } into g
                         select new Lichsugiaodich
                         {
                            mahd = g.Key.MaHÐ,
                            tensp = g.Key.TenSP,
                            iSoluong = g.Key.SoLuong,
                            ngaymua = g.Key.NgayMua,
                            tonggia = g.Key.TongGia,
                            hinhanh = g.Key.HinhAnhSP
                         };

            
            return View(hdList);
           

        }

       
    }
}