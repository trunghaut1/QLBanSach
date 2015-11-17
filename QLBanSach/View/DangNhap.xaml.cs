using QLBanSach.BLL;
using QLBanSach.DAL;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace QLBanSach.View
{
    /// <summary>
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : UserControl
    {
        NhanVienBLL _db = new NhanVienBLL();
        public DangNhap()
        {
            InitializeComponent();
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void txtMaNV_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void Log()
        {
            if (String.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!");
                return;
            }
            NhanVien nv = _db.GetByID(int.Parse(txtMaNV.Text));
            if (nv == null) MessageBox.Show("Mã nhân viên không tồn tại!");
            else
            {
                if(nv.MatKhau == txtPass.Password)
                {
                    var main = App.Current.MainWindow as MainWindow;
                    main.MainMenu.IsEnabled = true;
                    if ((bool)nv.QuanTri) main.mnNhanVien.IsEnabled = true;
                    main._nv = nv;
                }
                else MessageBox.Show("Mật khẩu không chính xác!");
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Log();
        }
    }
}
