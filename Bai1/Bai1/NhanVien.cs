using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai1
{
    class NhanVien
    {
        public string ten { get; set; }
        public string loai { get; set; }
        public DateTime ngaySinh { get; set; }
        public double soTienBanHang { get; set; }
        public double hoaHong
        {
            get
            {
                if (soTienBanHang > 5000)
                {
                    return (0.2 * soTienBanHang);
                } else if(soTienBanHang > 1000)
                {
                    return (0.1 * soTienBanHang);
                }
                return 0;

            }
        }
        public NhanVien()
        {

        }
        public NhanVien(string ten, string loai, DateTime ngaySinh, double soTienBanHang)
        {
            this.ten = ten;
            this.loai = loai;
            this.ngaySinh = ngaySinh;
            this.soTienBanHang = soTienBanHang;
        }
        public override string ToString()
        {
            return ten+" - "+ngaySinh.ToShortDateString()+" - "+loai+" - Tiền bán hàng: "+soTienBanHang+" - Hoa hồng: "+hoaHong;
        }
    }
}
