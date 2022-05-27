using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ngay26_5
{
    class Employee
    {
        public string hoTen { get; set; }
        public string loai { get; set; }
        public string ngaySinh { get; set; }

        public double soTienBan { get; set; }
        public double hoaHong
        {
            get
            {
                if(soTienBan<1000)
                {
                    return 0;
                }    
                else if(soTienBan >= 1000 && soTienBan <= 5000)
                {
                    return 0.05 * soTienBan;
                }
                else
                {
                    return 0.1 * soTienBan;
                }
            }
        }
        public override string ToString()
        {
            return hoTen+"-"+ngaySinh+"-"+loai+"-"+"Tiền bán hàng:"+soTienBan+"Hoa hồng"+hoaHong;
        }
    }
}
