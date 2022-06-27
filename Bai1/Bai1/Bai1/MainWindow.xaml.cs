using Bai1.Models;
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
        Bai1Context db = new Bai1Context();
        private void hienThiDataGrid()
        {
            var query = from sv in db.SinhViens select sv;
            dgDanhSach.ItemsSource = query.ToList();
        }
        private void hienThiComboBoxLop()
        {
            var query = from lop in db.LopHocs select lop;
            cbLopHoc.ItemsSource = query.ToList();
            cbLopHoc.DisplayMemberPath = "TenLop";
            cbLopHoc.SelectedValuePath = "MaLop";
            cbLopHoc.SelectedIndex = 0;
        }
        private void xoaTBLoi()
        {
            lbErrDiem.Content = "";
            lbErrHoTenSV.Content = "";
            lbErrMaSV.Content = "";
        }
        private void xoaDL()
        {
            tbMaSV.Clear();
            tbHoTenSV.Clear();
            tbDiem.Clear();
            cbDiaChi.SelectedIndex = 0;
            cbLopHoc.SelectedIndex = 0;
            tbMaSV.Focus();
        }
        private bool checkData()
        {
            bool ans = true;
            string maSV = tbMaSV.Text;
            string hoTen = tbHoTenSV.Text;
            string diem = tbDiem.Text;
            if (!Regex.IsMatch(maSV, "^\\d{1,}$"))
            {
                ans = false;
                lbErrMaSV.Content = "Phải nhập mã sinh viên là số";
            }
            else
                lbErrMaSV.Content = "";
            if(!Regex.IsMatch(hoTen, "^[a-zA-Z ]+$"))
            {
                ans = false;
                lbErrHoTenSV.Content = "Phải nhập họ tên là kí tự chữ";
            }    
            else
                lbErrHoTenSV.Content = "";
            if(!Regex.IsMatch(diem, "^\\d{1,}$"))
            {
                ans = false;
                lbErrDiem.Content = "Phải nhập điểm là số";
            }
            else
            {
                lbErrDiem.Content = "";
                int score = int.Parse(diem);
                if(score<0 || score > 10)
                {
                    ans = false;
                    lbErrDiem.Content = "Điểm phải nằm trong khoảng [0,10]";
                }
                else
                {
                    lbErrDiem.Content = "";
                }
            }    
            return ans;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            xoaTBLoi();
            hienThiDataGrid();
            hienThiComboBoxLop();
        }

        private void btnDong_clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnThem_clicked(object sender, RoutedEventArgs e)
        {
            bool check = checkData();
            if (check)
            {
                try
                {
                    SinhVien sv = new SinhVien();
                    sv.MaLop = int.Parse(cbLopHoc.SelectedValue.ToString());
                    sv.MaSv = int.Parse(tbMaSV.Text);
                    sv.HoTen = tbHoTenSV.Text;
                    sv.DiaChi = cbDiaChi.Text;
                    sv.Diem = float.Parse(tbDiem.Text);

                    db.SinhViens.Add(sv);
                    db.SaveChanges();
                    hienThiDataGrid();
                    xoaTBLoi();
                    xoaDL();
                }
                catch(Exception x)
                {
                    lbErrMaSV.Content="Có lỗi: \n"+x.Message;
                    db.ChangeTracker.Clear();
                }
            }
        }

        private void btnXoa_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult mbr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "THÔNG BÁO", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                if (mbr == MessageBoxResult.Yes)
                {
                    var svXoa = db.SinhViens.SingleOrDefault(sv => sv.MaSv == int.Parse(tbMaSV.Text));
                    db.SinhViens.Remove(svXoa);
                    db.SaveChanges();
                    hienThiDataGrid();
                    xoaTBLoi();
                    xoaDL();
                }
                
            }
            catch(Exception ex1)
            {
                lbErrMaSV.Content = "Có lỗi: \n" + ex1.Message;
                db.ChangeTracker.Clear();
            }
        }

        private void xoaDong_clicked(object sender, RoutedEventArgs e)
        {
            object obj = dgDanhSach.SelectedItem;
            if(obj != null)
            {
                try
                {
                    SinhVien sv = (SinhVien)obj;
                    int maSV = sv.MaSv;
                    MessageBoxResult mbr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "THÔNG BÁO", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (mbr == MessageBoxResult.Yes)
                    {
                        var svXoa = db.SinhViens.SingleOrDefault(sv => sv.MaSv == maSV);
                        db.SinhViens.Remove(svXoa);
                        db.SaveChanges();
                        hienThiDataGrid();
                        xoaDL();
                    }
                    
                }catch(Exception ex1)
                {
                    lbErrMaSV.Content = "Bạn phải chọn 1 hàng";
                }
            }
        }

        private void Selected_clicked(object sender, SelectionChangedEventArgs e)
        {
            object obj = dgDanhSach.SelectedItem;
            if (obj != null)
            {
                try
                {
                    SinhVien sv = (SinhVien)obj;
                    tbMaSV.Text = sv.MaSv.ToString();
                    tbHoTenSV.Text = sv.HoTen.ToString();
                    tbDiem.Text = sv.Diem.ToString();
                    cbLopHoc.SelectedValue = sv.MaLop.ToString();
                    string dc = sv.DiaChi.ToString().ToLower();
                    if(dc.Equals("hà nội"))
                    {
                        cbDiaChi.SelectedIndex = 0;
                    }else if(dc.Equals("hải dương"))
                    {
                        cbDiaChi.SelectedIndex = 1;
                    }else if(dc.Equals("hưng yên"))
                    {
                        cbDiaChi.SelectedIndex = 2;
                    }else if(dc.Equals("hải phòng"))
                    {
                        cbDiaChi.SelectedIndex = 3;
                    }else if(dc.Equals("quảng ninh"))
                    {
                        cbDiaChi.SelectedIndex = 4;
                    }else if(dc.Equals("vĩnh phúc"))
                    {
                        cbDiaChi.SelectedIndex = 5;
                    }else if(dc.Equals("đà nẵng"))
                    {
                        cbDiaChi.SelectedIndex = 6;
                    }else if(dc.Equals("đà lạt"))
                    {
                        cbDiaChi.SelectedIndex = 7;
                    }else if(dc.Equals("phú quốc"))
                    {
                        cbDiaChi.SelectedIndex = 8;
                    }else if(dc.Equals("hồ chí minh"))
                    {
                        cbDiaChi.SelectedIndex = 9;
                    }

                }catch(Exception x)
                {
                    lbErrMaSV.Content = "Có lỗi: \n" + x.Message;
                }
            }
        }

        private void btnSua_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var svSua = db.SinhViens.SingleOrDefault(sv => sv.MaSv == int.Parse(tbMaSV.Text));
                if (svSua != null)
                {
                    if (checkData())
                    {
                        svSua.HoTen = tbHoTenSV.Text;
                        svSua.Diem = float.Parse(tbDiem.Text);
                        svSua.DiaChi = cbDiaChi.Text;
                        svSua.MaLop = int.Parse(cbLopHoc.SelectedValue.ToString());

                        db.SaveChanges();
                        hienThiDataGrid();
                        xoaDL();
                    }
                }

            }catch(Exception ex)
            {
                lbErrMaSV.Content = "Có lỗi: \n" + ex.Message;
            }
        }

        private void btnTim_clicked(object sender, RoutedEventArgs e)
        {
            Window1 obj = new Window1();
            obj.ShowDialog();
        }
    }
}
