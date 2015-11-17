using QLBanSach.DAL;
using QLBanSach.BLL;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QLBanSach.View
{
    /// <summary>
    /// Interaction logic for NhanVienView.xaml
    /// </summary>
    public partial class NhanVienView : UserControl
    {
        NhanVienBLL _db = new NhanVienBLL();
        public NhanVienView()
        {
            InitializeComponent();
            LoadDS(null);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new NhanVien()
            {
                TenNhanVien = txtTenNhanVien.Text,
                SDT = txtSDT.Text,
                MatKhau = txtPass.Password,
                QuanTri = false
            };
            if (_db.Add(record))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadDS(null);
            }
            else MessageBox.Show("Thêm nhân viên thất bại!");
        }
        private bool CheckNull()
        {
            if (string.IsNullOrEmpty(txtTenNhanVien.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên!");
                return false;
            }
            if (string.IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
                return false;
            }
            return true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var admin = (NhanVien)nhanVienDataGrid.SelectedItem;
            var record = new NhanVien()
            {
                MaNhanVien = int.Parse(txtMaNhanVien.Text),
                TenNhanVien = txtTenNhanVien.Text,
                SDT = txtSDT.Text,
                MatKhau = txtPass.Password,
                QuanTri = admin.QuanTri
            };
            if (_db.Update(record))
            {
                MessageBox.Show("Cập nhật nhân viên thành công!");
                LoadDS(null);
            }
            else MessageBox.Show("Cập nhật nhân viên thất bại!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult comf = MessageBox.Show("Xác nhận xóa nhân viên này?", "Xác nhận", MessageBoxButton.YesNo);
            if (comf == MessageBoxResult.Yes)
            {
                var record = (NhanVien)nhanVienDataGrid.SelectedItem;
                if (record != null)
                {
                    if (!_db.CheckFK(record.MaNhanVien)) MessageBox.Show("Tồn tại đơn hàng mang mã nhân viên này, không thể xóa!");
                    else
                    {
                        if (_db.Delete(record.MaNhanVien))
                        {
                            MessageBox.Show("Xóa nhân viên thành công!");
                            LoadDS(null);
                        }
                        else MessageBox.Show("Xóa nhân viên thất bại!");
                    }
                }
                else MessageBox.Show("Vui lòng chọn nhân viên!");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTenNhanVien.Text = null;
            txtSDT.Text = null;
            txtPass.Password = null;
        }

        private void btnResetS_Click(object sender, RoutedEventArgs e)
        {
            txtTenNhanVienS.Text = null;
            txtSDTS.Text = null;
            chkQuanTri.IsChecked = false;
            LoadDS(null);
        }
        private void Search()
        {
            List<NhanVien> record = _db.Search(txtTenNhanVienS.Text,txtSDTS.Text,chkQuanTri.IsChecked);
            LoadDS(record);
        }

        private void LoadDS(List<NhanVien> value)
        {

            // Do not load your data at design time.
             if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
             {
             	//Load your data here and assign the result to the CollectionViewSource.
             	CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["nhanVienViewSource"];
                myCollectionViewSource.Source = value ?? _db.GetAll();
             }
        }

        private void nhanVienDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (NhanVien)nhanVienDataGrid.SelectedItem;
            if (record != null)
            {
                txtMaNhanVien.Text = record.MaNhanVien.ToString();
                txtTenNhanVien.Text = record.TenNhanVien;
                txtSDT.Text = record.SDT;
                txtPass.Password = record.MatKhau;
            }
        }

        private void SearchPanel_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            Search();
        }

        private void chkQuanTri_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
    }
}
