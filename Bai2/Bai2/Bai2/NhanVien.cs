using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai2
{
    class NhanVien
    {
        public string hoTen { get; set; }
        public string gioiTinh { get; set; }
        public int soNgayCong { get; set; }
        public double luong { get; set; }
        public double thuong
        {
            get
            {
                if (soNgayCong >= 27)
                    return (0.1*luong);
                return 0;
            }
        }
        public NhanVien()
        {

        }
        public NhanVien(string hoTen,string gioiTinh,int soNgayCong, double luong)
        {
            this.hoTen = hoTen;
            this.gioiTinh = gioiTinh;
            this.soNgayCong = soNgayCong;
            this.luong = luong;
        }
        public override string ToString()
        {
            return hoTen+" - "+gioiTinh+" - "+soNgayCong+" - "+luong+" - "+thuong;
        }
    }
}
