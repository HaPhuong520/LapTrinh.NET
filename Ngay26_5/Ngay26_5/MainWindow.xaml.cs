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

namespace Ngay26_5
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
        ObservableCollection <Employee> li = new ObservableCollection< Employee >();
        private void btnNhap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sName, sLoai, sNgaySinh, sTienBan;
                string error = "";
                sName = txtName.Text;
                sLoai = cbLoai.Text;
                sNgaySinh = dpNgaySinh.Text;
                sTienBan = txtSoTienBan.Text;
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    error += "Bạn phải nhập họ tên";
                }
                if (string.IsNullOrEmpty(txtSoTienBan.Text))
                {
                    error += "\n Bạn phải nhập số tiền bán";
                }
                if (string.IsNullOrEmpty(dpNgaySinh.Text))
                {
                    error += "\n Bạn phải nhập ngày sinh";
                }
                else
                {
                    int namSinh = int.Parse(sNgaySinh.Substring(sNgaySinh.Length - 4, 4));
                    int tuoi = 2022 - namSinh;
                    if (tuoi < 19 || tuoi > 60)
                    {
                        error += "\n Tuổi nhân viên phải thuộc [29,60]";
                    }
                }
                if (Regex.IsMatch(txtName.Text, @"\d+"))
                {
                    error += "\n Họ tên không có kí tự số";
                }
                if (!Regex.IsMatch(txtSoTienBan.Text, @"\d+"))
                {
                    error += "\n Số tiền bán hàng chỉ gồm các số";
                }
                else
                {
                    if (double.Parse(txtSoTienBan.Text) < 0)
                    {
                        error += "\n Số tiền bán hàng phải lớn hơn 0";
                    }
                }

                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                }
                else
                {
                    Employee e1 = new Employee();
                    e1.hoTen = sName;
                    e1.loai = sLoai;
                    e1.ngaySinh = sNgaySinh;
                    e1.soTienBan = int.Parse(sTienBan);
                    li.Add(e1);
                    lbShow.ItemsSource = li;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWindow2_Click(object sender, RoutedEventArgs e)
        {
            Employee emp = (Employee)lbShow.SelectedItem;
            if(emp==null)
            {
                MessageBox.Show("Bạn chưa chọn 1 nhân viên xem chi tiết");
            }
            else
            {
                Window2 w2 = new Window2();
                w2.txtName.Text = emp.hoTen;
                w2.txtSoTienBan.Text = emp.soTienBan.ToString();
                w2.cbLoai.Text = emp.loai;
                w2.dpNgaySinh.Text = emp.ngaySinh;
                w2.Show();
                Close();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            Employee emp1 = (Employee)lbShow.SelectedItem;
            if(emp1==null)
            {
                MessageBox.Show("Bạn chưa chọn 1 nhân viên để xóa");
            }
            else
            {
                MessageBoxResult msg = MessageBox.Show("Bạn có muốn xóa không", "Thông báo", MessageBoxButton.YesNo);
                if(msg==MessageBoxResult.Yes)
                {
                    li.Remove(emp1);
                    txtName.Clear();
                    txtSoTienBan.Clear();
                    dpNgaySinh.SelectedDate = DateTime.Today;
                    cbLoai.SelectedIndex = -1;
                }    
               
            }
        }
    }
}
