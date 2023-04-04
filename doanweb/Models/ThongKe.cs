using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanweb.Models
{
    public class ThongKe
    {
        public int? soluong { get; set; }
        public decimal? thunhap { get; set; }

        public int? mahd { get; set; }
        public decimal? tonggia { get; set; }
        public bool? tinhtrangthanhtoan { set; get; }
    }
}