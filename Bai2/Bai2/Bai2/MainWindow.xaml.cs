using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bai2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<NhanVien> listNV = new ObservableCollection<NhanVien>();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnThem_setOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string ten = tbHoTen.Text;
                string gioiTinh = "";
                string soNgay = tbSoNgayCong.Text;
                string luong = tbLuong.Text;
                if (rdNam.IsChecked == true)
                {
                    gioiTinh += "Nam";
                }
                else
                {
                    gioiTinh += "Nữ";
                }
                // kiểm tra bỏ trống
                string err = "";
                if (string.IsNullOrEmpty(ten))
                {
                    err += "Họ tên không được bỏ trống\n";
                }
                if (string.IsNullOrEmpty(soNgay))
                {
                    err += "Số ngày công không được bỏ trống\n";
                }
                if (string.IsNullOrEmpty(luong))
                {
                    err += "Lương không được bỏ trống\n";
                }
                // kiểm tra định dạng
                if (!Regex.IsMatch(ten, @"^[a-zA-Z\s]+$"))
                {
                    err += "Họ tên phải là chữ, không được xuất hiện số hoặc kí tự đặc biệt\n";
                }
                if (!Regex.IsMatch(luong, @"(\d+)?.(\d+)?") ||  //số thực double
                       !Regex.IsMatch(luong, @"\d+")) // số nguyên
                {
                    err += "Lương phải là số thực\n";
                }
                if (!Regex.IsMatch(soNgay, @"(\d)+"))
                {
                    err += "Số ngày công phải là số nguyên không âm\n";
                }
                if (!string.IsNullOrWhiteSpace(err))
                {
                    throw new Exception(err);
                }
                // kiểm tra điều kiện
                if (double.Parse(luong)<3000 || double.Parse(luong) > 5000)
                {
                    err += "Lương phải nằm trong khoảng [3000,5000]\n";
                }
                if(int.Parse(soNgay)<20 || int.Parse(soNgay) > 30)
                {
                    err += "Số ngày công phải nằm trong khoảng [20,30]\n";
                }
                if (!string.IsNullOrWhiteSpace(err))
                {
                    throw new Exception(err);
                }
                else
                {
                    NhanVien x = new NhanVien(ten, gioiTinh, int.Parse(tbSoNgayCong.Text), double.Parse(tbLuong.Text));
                    listNV.Add(x);
                    lbDanhSach.ItemsSource = listNV;
                    tbHoTen.Clear();
                    tbLuong.Clear();
                    tbSoNgayCong.Clear();
                    tbHoTen.Focus();
                    rdNam.IsChecked = true;
                    rdNu.IsChecked = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnDong_setOnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnXoa_setOnClick(object sender, RoutedEventArgs e)
        {
            NhanVien x = (NhanVien)lbDanhSach.SelectedItem;
            if (x == null)
            {
                MessageBox.Show("Chưa có nhân viên nào được chọn để xóa", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult mbr = MessageBox.Show("Bạn có chắc chắn muốn xóa không", "THÔNG BÁO", MessageBoxButton.YesNo);
                if (mbr == MessageBoxResult.Yes)
                {
                    listNV.Remove(x);
                    tbHoTen.Clear();
                    tbHoTen.Focus();
                    rdNam.IsChecked = true;
                    tbLuong.Clear();
                    tbSoNgayCong.Clear();
                }
            }

        }
        private void btnChiTiet_setOnClick(object sender, RoutedEventArgs e)
        {
            NhanVien x = (NhanVien)lbDanhSach.SelectedItem;
            if (x == null)
            {
                MessageBox.Show("Chưa có nhân viên nào được chọn để xem chi tiết", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Window2 w2 = new Window2();
                w2.tbHoTen.Text = x.hoTen;
                if (x.hoTen == "Nam")
                {
                    w2.rdNam.IsChecked = true;
                }
                else
                {
                    w2.rdNam.IsChecked = true;
                }
                w2.tbSoNgayCong.Text = x.soNgayCong.ToString();
                w2.tbLuong.Text = x.luong.ToString();
                w2.tbThuong.Text = x.thuong.ToString();
                w2.Show();
                Close();
            }
        }
    }
}
