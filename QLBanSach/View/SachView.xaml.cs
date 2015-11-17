using FlatTheme.ControlStyle;
using QLBanSach.BLL;
using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QLBanSach.View
{
    /// <summary>
    /// Interaction logic for SachView.xaml
    /// </summary>
    public partial class SachView : UserControl
    {
        SachBLL _db = new SachBLL();
        public SachView()
        {
            InitializeComponent();
            LoadComboBox(true,true,true);
            LoadSearch(true,true,true);
            LoadDS(null);          
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new Sach()
            {
                TenSach = txtTenSach.Text,
                DonGia = int.Parse(txtDonGia.Text),
                SoLuong = int.Parse(txtSoLuong.Text),
                MaLoai = (int)loaiSachComboBox.SelectedValue,
                MaNXB = (int)nXBComboBox.SelectedValue,
                MaTacGia = (int)tacGiaComboBox.SelectedValue
            };
            if (_db.Add(record))
            {
                MessageBox.Show("Thêm sách thành công!");
                sachDataGrid.Items.Refresh();
                LoadDS(null);
            }
            else MessageBox.Show("Thêm sách thất bại!");
        }
        private bool CheckNull()
        {
            if (string.IsNullOrEmpty(txtTenSach.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sách!");
                return false;
            }
            if (string.IsNullOrEmpty(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng!");
                return false;
            }
            if (string.IsNullOrEmpty(txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn giá!");
                return false;
            }
            if(loaiSachComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại sách!");
                return false;
            }
            if (nXBComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà xuất bản!");
                return false;
            }
            if (tacGiaComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tác giả!");
                return false;
            }
            return true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new Sach()
            {
                MaSach = int.Parse(txtMaSach.Text),
                TenSach = txtTenSach.Text,
                SoLuong = int.Parse(txtSoLuong.Text),
                DonGia = int.Parse(txtDonGia.Text),
                MaTacGia = (int)tacGiaComboBox.SelectedValue,
                MaLoai = (int)loaiSachComboBox.SelectedValue,
                MaNXB = (int)nXBComboBox.SelectedValue
            };
            if (_db.Update(record))
            {
                MessageBox.Show("Cập nhật sách thành công!");
                LoadDS(null);
            }
            else MessageBox.Show("Cập nhật sách thất bại!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult comf = MessageBox.Show("Xác nhận xóa sách này?", "Xác nhận", MessageBoxButton.YesNo);
            if (comf == MessageBoxResult.Yes)
            {
                var record = (Sach)sachDataGrid.SelectedItem;
                if (record != null)
                {
                    if (!_db.CheckFK(record.MaSach)) MessageBox.Show("Tồn tại đơn hàng mang mã sách này, không thể xóa!");
                    else
                    {
                        if (_db.Delete(record.MaSach))
                        {
                            MessageBox.Show("Xóa sách thành công!");
                            LoadDS(null);
                        }
                        else MessageBox.Show("Xóa sách thất bại!");
                    }
                }
                else MessageBox.Show("Vui lòng chọn sách!");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTenSach.Text = null;
            txtDonGia.Text = null;
            txtSoLuong.Text = null;
            loaiSachComboBox.SelectedIndex = -1;
            nXBComboBox.SelectedIndex = -1;
            tacGiaComboBox.SelectedIndex = -1;
        }

        private void btnResetS_Click(object sender, RoutedEventArgs e)
        {
            ResetSearch();
        }
        private void LoadSearch(bool _loai, bool _tacgia, bool _nxb)
        {
            var loaisach = new LoaiSachBLL();
            var tacgia = new TacGiaBLL();
            var nxb = new NXBBLL();
            if (_loai) loaiSachS.ItemsSource = loaisach.GetAll();
            if (_tacgia) tacGiaS.ItemsSource = tacgia.GetAll();
            if (_nxb) nXBS.ItemsSource = nxb.GetAll();
        }
        private void ResetSearch()
        {
            txtGiaTu.Text = null;
            txtGiaDen.Text = null;
            txtTenSachS.Text = null;
            loaiSachS.SelectedIndex = -1;
            tacGiaS.SelectedIndex = -1;
            nXBS.SelectedIndex = -1;
            chkConHang.IsChecked = false;
            LoadDS(null);
        }
        private void SearchPanel_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            Search();
        }
        private void Search()
        {
            int? giatu = String.IsNullOrEmpty(txtGiaTu.Text) ? null : int.Parse(txtGiaTu.Text) as int?;
            int? giaden = String.IsNullOrEmpty(txtGiaDen.Text) ? null : int.Parse(txtGiaDen.Text) as int?;
            var record = _db.Search(txtTenSachS.Text, (int?)loaiSachS.SelectedValue, (int?)nXBS.SelectedValue,
                (int?)tacGiaS.SelectedValue, giatu, giaden, chkConHang.IsEnabled);
            LoadDS(record);
        }

        private void LoadDS(List<Sach> value)
        {
            // Do not load your data at design time.
             if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
             {
             	//Load your data here and assign the result to the CollectionViewSource.
             	CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["sachViewSource"];
                myCollectionViewSource.Source = value ?? _db.GetAll();
             }
        }
        private void LoadComboBox(bool _loai, bool _tacgia, bool _nxb)
        {
            var loaisach = new LoaiSachBLL();
            var tacgia = new TacGiaBLL();
            var nxb = new NXBBLL();
            if(_loai) loaiSachComboBox.ItemsSource = loaisach.GetAll();
            if (_tacgia) tacGiaComboBox.ItemsSource = tacgia.GetAll();
            if (_nxb) nXBComboBox.ItemsSource = nxb.GetAll();
        }

        private void sachDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (Sach)sachDataGrid.SelectedItem;
            if (record != null)
            {
                txtMaSach.Text = record.MaSach.ToString();
                txtTenSach.Text = record.TenSach;
                txtSoLuong.Text = record.SoLuong.ToString();
                txtDonGia.Text = record.DonGia.ToString();
                loaiSachComboBox.SelectedValue = record.MaLoai;
                tacGiaComboBox.SelectedValue = record.MaTacGia;
                nXBComboBox.SelectedValue = record.MaNXB;
            }
        }

        private void NumberOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }
        private void chkConHang_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void ComboBoxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (cbx.SelectedIndex != -1) Search();
        }

        private void btnAddTacGia_Click(object sender, RoutedEventArgs e)
        {
            FlatWindow window = new FlatWindow()
            {
                Title = "Quản lý tác giả",
                Name = "TacGia",
                Content = new TacGiaView(),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                FontSize = 16
            };
            window.Style = Application.Current.FindResource("FlatWindow") as Style;
            window.Closed += new EventHandler(AddWindow_Closed);
            window.ShowDialog();

        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            FlatWindow window = (FlatWindow)sender;
            if(window.Name == "TacGia")
            {
                LoadComboBox(false, true, false);
                LoadSearch(false, true, false);
            }
            if (window.Name == "LoaiSach")
            {
                LoadComboBox(true, false, false);
                LoadSearch(true, false, false);
            }
            if (window.Name == "NXB")
            {
                LoadComboBox(false, false, true);
                LoadSearch(false, false, true);
            }
        }

        private void btnAddNXB_Click(object sender, RoutedEventArgs e)
        {
            FlatWindow window = new FlatWindow()
            {
                Title = "Quản nhà xuất bản",
                Name = "NXB",
                Content = new NXBView(),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                FontSize = 16
            };
            window.Style = Application.Current.FindResource("FlatWindow") as Style;
            window.Closed += new EventHandler(AddWindow_Closed);
            window.ShowDialog();
        }

        private void btnAddLoaiSach_Click(object sender, RoutedEventArgs e)
        {
            FlatWindow window = new FlatWindow()
            {
                Title = "Quản lý loại sách",
                Name = "LoaiSach",
                Content = new LoaiSachView(),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                FontSize = 16
            };
            window.Style = Application.Current.FindResource("FlatWindow") as Style;
            window.Closed += new EventHandler(AddWindow_Closed);
            window.ShowDialog();
        }
    }
}
