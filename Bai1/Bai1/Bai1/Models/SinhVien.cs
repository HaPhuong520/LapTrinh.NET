using System;
using System.Collections.Generic;

#nullable disable

namespace Bai1.Models
{
    public partial class SinhVien
    {
        public int MaSv { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public double Diem { get; set; }
        public int MaLop { get; set; }
        public string Anh { get; set; }

        public virtual LopHoc MaLopNavigation { get; set; }
    }
}
