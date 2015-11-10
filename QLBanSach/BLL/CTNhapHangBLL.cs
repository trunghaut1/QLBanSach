using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanSach.BLL
{
    public class CTNhapHangBLL
    {
        // Lấy chi tiết nhập hàng theo mã nhập hàng
        // >> Dùng hàm Search
        public List<CTNhapHang> Search(int? manhaphang, int? masach)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    var record = from r in db.CTNhapHang select r;
                    if (manhaphang != null) record = record.Where(r => r.MaNhapHang == manhaphang);
                    if (masach != null) record = record.Where(r => r.MaSach == masach);
                    return record.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm chi tiết nhập hàng
        public bool Add(CTNhapHang value)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    db.CTNhapHang.Add(value);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Thêm List chi tiết nhập hàng
        public bool AddList(List<CTNhapHang> value)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    foreach (var record in value)
                    {
                        db.CTNhapHang.Add(record);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật chi tiết nhập hàng
        public bool Update(CTNhapHang value)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    CTNhapHang record = db.CTNhapHang.SingleOrDefault(v => v.MaNhapHang == value.MaNhapHang && v.MaSach == value.MaSach);
                    record.SoLuong = value.SoLuong;
                    record.DonGia = value.DonGia;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa chi tiết nhập hàng
        public bool Delete(int manhaphang, int masach)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    CTNhapHang record = db.CTNhapHang.SingleOrDefault(v => v.MaNhapHang == manhaphang && v.MaSach == masach);
                    db.CTNhapHang.Remove(record);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
