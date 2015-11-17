using FlatTheme.ControlStyle;
using QLBanSach.DAL;
using QLBanSach.View;
using System.Windows;

namespace QLBanSach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FlatWindow
    {
        public NhanVien _nv = new NhanVien();
        public MainWindow()
        {
            InitializeComponent();
            MainGrid.Children.Add(new DangNhap());
            _nv = new NhanVien() { MaNhanVien = 3, MatKhau = "123456" };
        }

        private void mnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            _nv = null;
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new DangNhap());
            MainMenu.IsEnabled = false;
        }

        private void mnNXB_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new NXBView());
        }

        private void mnLoaiSach_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new LoaiSachView());
        }

        private void mnTacGia_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new TacGiaView());
        }

        private void mnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new NhanVienView());
        }

        private void mnSach_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new SachView());
        }
    }
}
