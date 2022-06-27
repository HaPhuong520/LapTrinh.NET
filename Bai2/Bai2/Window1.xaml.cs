using Bai2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bai2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        Bai2Context db = new Bai2Context();

        private void Win1_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from sp in db.SanPhams
                        join nh in db.NhomHangs
                        on sp.MaNhomHang equals nh.MaNhomHang
                        where sp.MaNhomHang ==1 where nh.MaNhomHang==1
                        select new
                        {
                            sp.MaSp,
                            sp.TenSanPham,
                            sp.DonGia,
                            sp.SoLuongBan,
                            nh.TenNhomHang,
                            TienBan = sp.SoLuongBan * sp.DonGia
                        };
            dgWin1.ItemsSource = query.ToList();
        }
    }
}
