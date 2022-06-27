using System;
using System.Collections.Generic;

#nullable disable

namespace Bai1.Models
{
    public partial class LopHoc
    {
        public LopHoc()
        {
            SinhViens = new HashSet<SinhVien>();
        }

        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public string GiangVien { get; set; }

        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
