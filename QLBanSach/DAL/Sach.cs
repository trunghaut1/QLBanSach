//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLBanSach.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            this.CTDonHang = new HashSet<CTDonHang>();
            this.CTNhapHang = new HashSet<CTNhapHang>();
        }
    
        public int MaSach { get; set; }
        public Nullable<int> MaLoai { get; set; }
        public string TenSach { get; set; }
        public Nullable<int> MaNXB { get; set; }
        public Nullable<int> MaTacGia { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<int> DonGia { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDonHang> CTDonHang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTNhapHang> CTNhapHang { get; set; }
        public virtual LoaiSach LoaiSach { get; set; }
        public virtual NXB NXB { get; set; }
        public virtual TacGia TacGia { get; set; }
    }
}
