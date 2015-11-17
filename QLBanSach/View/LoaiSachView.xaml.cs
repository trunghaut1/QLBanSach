using QLBanSach.BLL;
using QLBanSach.DAL;
using System.Windows;
using System.Windows.Controls;

namespace QLBanSach.View
{
    /// <summary>
    /// Interaction logic for LoaiSachView.xaml
    /// </summary>
    public partial class LoaiSachView : UserControl
    {
        LoaiSachBLL _db = new LoaiSachBLL();
        public LoaiSachView()
        {
            InitializeComponent();
            LoadDS();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new LoaiSach()
            {
                TenLoai = txtTenLoai.Text
            };
            if (_db.Add(record))
            {
                MessageBox.Show("Thêm loại sách thành công!");
                LoadDS();
            }
            else MessageBox.Show("Thêm loại sách thất bại!");
        }
        private bool CheckNull()
        {
            if (string.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại!");
                return false;
            }
            return true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new LoaiSach()
            {
                MaLoai = int.Parse(txtMaLoai.Text),
                TenLoai = txtTenLoai.Text
            };
            if (_db.Update(record))
            {
                MessageBox.Show("Cập nhật loại sách thành công!");
                LoadDS();
            }
            else MessageBox.Show("Cập nhật loại sách thất bại!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult comf = MessageBox.Show("Xác nhận xóa loại sách này?", "Xác nhận", MessageBoxButton.YesNo);
            if (comf == MessageBoxResult.Yes)
            {
                var record = (LoaiSach)loaiSachDataGrid.SelectedItem;
                if (record != null)
                {
                    if (!_db.CheckFK(record.MaLoai)) MessageBox.Show("Tồn tại sách mang loại này, không thể xóa!");
                    else
                    {
                        if (_db.Delete(record.MaLoai))
                        {
                            MessageBox.Show("Xóa loại sách thành công!");
                            LoadDS();
                        }
                        else MessageBox.Show("Xóa loại sách thất bại!");
                    }
                }
                else MessageBox.Show("Vui lòng chọn loại sách!");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTenLoai.Text = null;
        }

        private void LoadDS()
        {

            // Do not load your data at design time.
             if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
             {
             	//Load your data here and assign the result to the CollectionViewSource.
             	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["loaiSachViewSource"];
                myCollectionViewSource.Source = _db.GetAll();
             }
        }

        private void loaiSachDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (LoaiSach)loaiSachDataGrid.SelectedItem;
            if (record != null)
            {
                txtMaLoai.Text = record.MaLoai.ToString();
                txtTenLoai.Text = record.TenLoai;
            }
        }
    }
}
