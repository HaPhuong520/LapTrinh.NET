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

namespace Bai1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        ObservableCollection<NhanVien> listNV = new ObservableCollection<NhanVien>();
        private void btnNhap_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ten = tbHoTen.Text;
                //string loai = cbLoaiNV.Text;
                string loai = (cbLoaiNV.SelectedItem as ComboBoxItem).Content.ToString();
                //string doB = dpNgaySinh.SelectedDate.Value.ToString();
                //string tienBH = tbSoTienBanHang.Text;
                DateTime ns = dpNgaySinh.SelectedDate.Value;
                double tien = double.Parse(tbSoTienBanHang.Text);
                // kiểm tra bỏ trống
                string err = "";
                if (string.IsNullOrWhiteSpace(ten))
                {
                    err += "Họ tên không được để trống\n";
                }
                if (string.IsNullOrWhiteSpace(tbSoTienBanHang.Text))
                {
                    err += "Số tiền bán hàng không được để trống\n";
                }
                // kiểm tra định dạng
                if(!Regex.IsMatch(ten, @"^[a-zA-Z\s]+$"))
                {
                    err += "Họ tên phải là chữ, không được xuất hiện số hoặn kí tự đặc biệt\n";
                }
                if(!Regex.IsMatch(tbSoTienBanHang.Text, @"(\d+)?.(\d+)?") ||  //số thực double
                   !Regex.IsMatch(tbSoTienBanHang.Text, @"\d+")) // số nguyên
                {
                    err += "Số tiền bán phải là số thực\n";
                }
                // kiểm tra giá trị
                if (double.Parse(tbSoTienBanHang.Text) < 0)
                {
                    err += "Số tiền bán hàng phải là số không âm\n";
                }
                int yearNow = DateTime.Today.Year;
                if(yearNow-ns.Year<18 || yearNow - ns.Year > 60)
                {
                    err += "Tuổi phải nằm trong khoảng [18,60]";
                }
                if (!string.IsNullOrWhiteSpace(err))
                {
                    throw new Exception(err);
                }
                else
                {
                    NhanVien x = new NhanVien(ten, loai, ns, tien);
                    listNV.Add(x);
                    lbDanhSachNV.ItemsSource = listNV;
                    tbHoTen.Clear();
                    tbSoTienBanHang.Clear();
                    dpNgaySinh.SelectedDate = DateTime.Now;
                    cbLoaiNV.SelectedIndex = 0;
                    tbHoTen.Focus();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"LỖI",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btnXoa_click(object sender, RoutedEventArgs e)
        {
            NhanVien x = (NhanVien)lbDanhSachNV.SelectedItem;
            if (x == null)
            {
                MessageBox.Show("Chưa có nhân viên nào được chọn để xóa","THÔNG BÁO",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult mbr = MessageBox.Show("Bạn có muốn xóa không", "THÔNG BÁO", MessageBoxButton.YesNo);
                if (mbr == MessageBoxResult.Yes)
                {
                    listNV.Remove(x);
                    tbHoTen.Clear();
                    tbSoTienBanHang.Clear();
                    dpNgaySinh.SelectedDate = DateTime.Now;
                    cbLoaiNV.SelectedIndex = -1;
                    tbHoTen.Focus();
                }
            }
        }

        private void btnWindow2_click(object sender, RoutedEventArgs e)
        {
            NhanVien x = (NhanVien)lbDanhSachNV.SelectedItem;
            if (x == null)
            {
                MessageBox.Show("Chưa có nhân viên nào được chọn để xem thông tin", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Window2 w2 = new Window2();
                w2.tbHoTen.Text = x.ten;
                w2.cbLoaiNV.Text = x.loai;
                w2.dpNgaySinh.Text = x.ngaySinh.ToShortDateString();
                w2.tbSoTienBanHang.Text = x.soTienBanHang.ToString();
                w2.Show();
                Close();
            }

        }
    }
}
