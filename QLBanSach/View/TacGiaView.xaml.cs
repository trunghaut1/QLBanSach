using QLBanSach.BLL;
using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QLBanSach.View
{
    /// <summary>
    /// Interaction logic for TacGiaView.xaml
    /// </summary>
    public partial class TacGiaView : UserControl
    {
        TacGiaBLL _db = new TacGiaBLL();
        public TacGiaView()
        {
            InitializeComponent();
            LoadDS(null);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new TacGia()
            {
                TenTacGia = txtTenTacGia.Text
            };
            if (_db.Add(record))
            {
                MessageBox.Show("Thêm tác giả thành công!");
                LoadDS(null);
            }
            else MessageBox.Show("Thêm tác giả thất bại!");
        }
        private bool CheckNull()
        {
            if (string.IsNullOrEmpty(txtTenTacGia.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tác giả!");
                return false;
            }
            return true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new TacGia()
            {
                MaTacGia = int.Parse(txtMaTacGia.Text),
                TenTacGia = txtTenTacGia.Text
            };
            if (_db.Update(record))
            {
                MessageBox.Show("Cập nhật tác giả thành công!");
                LoadDS(null);
            }
            else MessageBox.Show("Cập nhật tác giả thất bại!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult comf = MessageBox.Show("Xác nhận xóa tác giả này?", "Xác nhận", MessageBoxButton.YesNo);
            if (comf == MessageBoxResult.Yes)
            {
                var record = (TacGia)tacGiaDataGrid.SelectedItem;
                if (record != null)
                {
                    if (!_db.CheckFK(record.MaTacGia)) MessageBox.Show("Tồn tại sách mang tác giả này, không thể xóa!");
                    else
                    {
                        if (_db.Delete(record.MaTacGia))
                        {
                            MessageBox.Show("Xóa tác giả thành công!");
                            LoadDS(null);
                        }
                        else MessageBox.Show("Xóa tác giả thất bại!");
                    }
                }
                else MessageBox.Show("Vui lòng chọn tác giả!");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTenTacGia.Text = null;
        }

        private void LoadDS(List<TacGia> value)
        {

            // Do not load your data at design time.
             if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
             {
             	//Load your data here and assign the result to the CollectionViewSource.
             	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["tacGiaViewSource"];
                myCollectionViewSource.Source = value ?? _db.GetAll();
             }
        }

        private void tacGiaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (TacGia)tacGiaDataGrid.SelectedItem;
            if (record != null)
            {
                txtMaTacGia.Text = record.MaTacGia.ToString();
                txtTenTacGia.Text = record.TenTacGia;
            }
        }
        private void Search()
        {
            List<TacGia> record = _db.Search(txtTenTacGiaS.Text);
            LoadDS(record);
        }

        private void txtTenTacGiaS_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Search();
        }

        private void btnResetS_Click(object sender, RoutedEventArgs e)
        {
            txtTenTacGiaS.Text = null;
            LoadDS(null);
        }
    }
}
