using QLBanSach.BLL;
using QLBanSach.DAL;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace QLBanSach.View
{
    /// <summary>
    /// Interaction logic for NXBView.xaml
    /// </summary>
    public partial class NXBView : UserControl
    {
        NXBBLL _db = new NXBBLL();
        public NXBView()
        {
            InitializeComponent();
            LoadDS();
        }

        private void LoadDS()
        {
            // Do not load your data at design time.
             if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
             {
             	//Load your data here and assign the result to the CollectionViewSource.
             	CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["nXBViewSource"];
                myCollectionViewSource.Source = _db.GetAll();
             }
        }

        private void nXBDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (NXB)nXBDataGrid.SelectedItem;
            if(record != null)
            {
                txtMaNXB.Text = record.MaNXB.ToString();
                txtTenNXB.Text = record.TenNXB;
            }
        }

        private void btnReset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtTenNXB.Text = null; 
        }
        private bool CheckNull()
        {
            if(string.IsNullOrEmpty(txtTenNXB.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà xuất bản!");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new NXB()
            {
                TenNXB = txtTenNXB.Text
            };
            if(_db.Add(record))
            {
                MessageBox.Show("Thêm nhà xuất bản thành công!");
                LoadDS();
            }
            else MessageBox.Show("Thêm nhà xuất bản thất bại!");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new NXB()
            {
                MaNXB = int.Parse(txtMaNXB.Text),
                TenNXB = txtTenNXB.Text
            };
            if (_db.Update(record))
            {
                MessageBox.Show("Cập nhật nhà xuất bản thành công!");
                LoadDS();
            }
            else MessageBox.Show("Cập nhật nhà xuất bản thất bại!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult comf = MessageBox.Show("Xác nhận xóa nhà xuất bản này?", "Xác nhận", MessageBoxButton.YesNo);
            if (comf == MessageBoxResult.Yes)
            {
                var record = (NXB)nXBDataGrid.SelectedItem;
                if (record != null)
                {
                    if (!_db.CheckFK(record.MaNXB)) MessageBox.Show("Tồn tại sách mang nhà xuất bản này, không thể xóa!");
                    else
                    {
                        if (_db.Delete(record.MaNXB))
                        {
                            MessageBox.Show("Xóa nhà xuất bản thành công!");
                            LoadDS();
                        }
                        else MessageBox.Show("Xóa nhà xuất bản thất bại!");
                    }
                }
                else MessageBox.Show("Vui lòng chọn nhà xuất bản!");
            }
        }
    }
}
