using Bai2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        Bai2Context db = new Bai2Context();
        private void xoaDL()
        {
            tbMaSP.Clear();
            tbTenSP.Clear();
            tbSoLuongBan.Clear();
            tbDonGia.Clear();
            cbNhomHang.SelectedIndex = 0;
            tbMaSP.Focus();
        }
        private bool checkKhoa()
        {
            bool c = true;
            if (checkData())
            {
                var m = db.SanPhams.SingleOrDefault(sp => sp.MaSp == int.Parse(tbMaSP.Text));
                if (m != null)
                {
                    c = false;
                    lbErrMaSP.Content = "Mã sản phẩm bị trùng";
                }
                else
                {
                    lbErrMaSP.Content = "";
                }
            }
            return c;
        }
        private bool checkData()
        {
            bool check = true;
            string maSP = tbMaSP.Text;
            string tenSP = tbTenSP.Text;
            string donGia = tbDonGia.Text;
            string sl = tbSoLuongBan.Text;
            if (!Regex.IsMatch(maSP, "^\\d{1,}$"))
            {
                check = false;
                lbErrMaSP.Content = "Mã sản phẩm phải là số";
            }
            else
            {
                lbErrMaSP.Content = "";
                
            }
            if (!Regex.IsMatch(tenSP,"^[a-zA-Z ]+$"))
            {
                check = false;
                lbErrTenSP.Content = "Tên sản phẩm phải là chữ";
            }
            else lbErrTenSP.Content = "";
            if (!Regex.IsMatch(donGia, "^\\d{1,}$"))
            {
                check = false;
                lbErrDonGia.Content = "Đơn giá phải là số";
            }
            else lbErrDonGia.Content = "";
            if (!Regex.IsMatch(sl, "^\\d{1,}$"))
            {
                check = false;
                lbErrSoLuongBan.Content = "Số lượng bán phải là số";
            }
            else
            { lbErrSoLuongBan.Content = "";
                int num = int.Parse(sl);
                if (num < 1)
                {
                    check = false;
                    lbErrSoLuongBan.Content = "Số lượng bán nhỏ nhất là 1";
                }
                else
                    lbErrSoLuongBan.Content = "";
            }
            
            return check;

        }
        private void hienThiComboBoxNhomHang()
        {
            var query = from nh in db.NhomHangs select nh;
            cbNhomHang.ItemsSource = query.ToList();
            cbNhomHang.DisplayMemberPath = "TenNhomHang";
            cbNhomHang.SelectedValuePath = "MaNhomHang";
            cbNhomHang.SelectedIndex = 0;
        }
        private void xoaTBLoi()
        {
            lbErrDonGia.Content = "";
            lbErrMaSP.Content = "";
            lbErrSoLuongBan.Content = "";
            lbErrTenSP.Content = "";
        }
        private void hienThiDataGrid()
        {
            var query = from sp in db.SanPhams
                        join nh in db.NhomHangs
                        on sp.MaNhomHang equals nh.MaNhomHang
                        orderby sp.SoLuongBan descending
                        select new
                        {
                            sp.MaSp,
                            sp.TenSanPham,
                            sp.DonGia,
                            sp.SoLuongBan,
                            nh.TenNhomHang,
                            TienBan = sp.DonGia * sp.SoLuongBan
                        };
            dgDanhSach.ItemsSource = query.ToList();
        }
        private void Selected_Clicked(object sender, SelectedCellsChangedEventArgs e)
        {
            object obj = dgDanhSach.SelectedItem;
            if (obj != null)
            {
                try
                {
                    Type t = dgDanhSach.SelectedItem.GetType();
                    PropertyInfo[] p = t.GetProperties();
                    tbMaSP.Text = p[0].GetValue(dgDanhSach.SelectedItem).ToString();
                    tbTenSP.Text = p[1].GetValue(dgDanhSach.SelectedItem).ToString();
                    tbDonGia.Text = p[2].GetValue(dgDanhSach.SelectedItem).ToString();
                    tbSoLuongBan.Text = p[3].GetValue(dgDanhSach.SelectedItem).ToString();
                    //cbNhomHang.SelectedValue = p[4].GetValue(dgDanhSach.SelectedItem).ToString();
                    string temp = p[4].GetValue(dgDanhSach.SelectedItem).ToString().ToLower();
                    if(temp.Equals("thuc pham"))
                    {
                        cbNhomHang.SelectedIndex = 0;
                    }
                    else
                    {
                        cbNhomHang.SelectedIndex = 1;
                    }
                }catch(Exception x)
                {
                    lbErrMaSP.Content = "Có lỗi: \n" + x.Message;
                }
               
            }
        }

        private void Win_Loaded(object sender, RoutedEventArgs e)
        {
            hienThiDataGrid();
            xoaTBLoi();
            hienThiComboBoxNhomHang();
        }

        private void btnThem_clicked(object sender, RoutedEventArgs e)
        {
            bool check = checkKhoa();
            if (checkData() && checkKhoa())
            {
                try
                {
                    SanPham s = new SanPham();
                    s.MaSp = int.Parse(tbMaSP.Text);
                    s.TenSanPham = tbTenSP.Text;
                    s.DonGia = int.Parse(tbDonGia.Text);
                    s.SoLuongBan = int.Parse(tbSoLuongBan.Text);
                    s.MaNhomHang = int.Parse(cbNhomHang.SelectedValue.ToString());
                    db.SanPhams.Add(s);
                    db.SaveChanges();
                    hienThiDataGrid();
                    xoaTBLoi();
                    xoaDL();
                }catch(Exception x)
                {
                    lbErrMaSP.Content = "Có lỗi khi thêm: \n" + x.Message;
                    db.ChangeTracker.Clear();
                }
            }
        }

        private void btnTim_clicked(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1();
            w1.ShowDialog();
        }

        private void btnSua_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var spSua = db.SanPhams.SingleOrDefault(sp => sp.MaSp == int.Parse(tbMaSP.Text));
                if (spSua != null)
                {
                    spSua.TenSanPham = tbTenSP.Text;
                    spSua.DonGia = int.Parse(tbDonGia.Text);
                    spSua.SoLuongBan = int.Parse(tbSoLuongBan.Text);
                    spSua.MaNhomHang = int.Parse(cbNhomHang.SelectedValue.ToString());
                    if (checkData())
                    {
                        MessageBoxResult mbr = MessageBox.Show("Bạn có muốn sửa không?", "THÔNG BÁO", MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                        if (mbr == MessageBoxResult.Yes)
                        {
                            db.SaveChanges();
                            hienThiDataGrid();
                            xoaDL();
                        }
                    }
                }
            }catch(Exception x)
            {
                lbErrMaSP.Content = "Có lỗi:\n" + x.Message;
            }
           
        }

        private void btnXoa_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var spXoa = db.SanPhams.SingleOrDefault(sp => sp.MaSp == (int.Parse(tbMaSP.Text)));
                MessageBoxResult mbr = MessageBox.Show("Bạn có muốn xóa không?", "THÔNG BÁO", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if(mbr == MessageBoxResult.Yes)
                {
                    db.SanPhams.Remove(spXoa);
                    db.SaveChanges();
                    hienThiDataGrid();
                    xoaDL();
                    xoaTBLoi();
                }

            }catch(Exception x)
            {
                lbErrMaSP.Content = "Có lỗi: \n" + x.Message;
            }
        }
    }
}
