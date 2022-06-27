using Bai1.Models;
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

namespace Bai1
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
        Bai1Context db = new Bai1Context();  
        private void Win1_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from sv in db.SinhViens
                        join lop in db.LopHocs
                        on sv.MaLop equals lop.MaLop
                        where sv.Diem > 5
                        where sv.MaLop == 3
                        select new
                        {
                            sv.MaSv,
                            sv.HoTen,
                            sv.DiaChi,
                            sv.Diem,
                            sv.MaLop,
                            lop.TenLop
                        };
            dgWindow1.ItemsSource = query.ToList();
        }
    }
}
