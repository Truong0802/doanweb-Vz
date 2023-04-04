using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanweb.Models
{
    public class invoice
    {
        public string hotenkh { get; set; }
        public string diachi { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public decimal? tongtien { get; set; }
        public decimal? tongtt { get; set; }
        public string ngaymua { get; set; }
        
        public string ngaygiao { get; set; }
        public string ngaydat { get; set; }

        public string tensp { get; set; }
        public int? soluong { get; set; }

        public decimal? giasp { get; set; }

        public string tinhtrangtt { get; set; }

        
    }
}