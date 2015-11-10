using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanSach.BLL
{
    public class DonHangBLL
    {
        // Lấy danh sách đơn hàng
        public List<DonHang> GetAll()
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    return db.DonHang.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy đơn hàng
        public DonHang GetByID(int id)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    DonHang record = db.DonHang.SingleOrDefault(v => v.MaDonHang == id);
                    return record;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm đơn hàng
        public bool Add(DonHang value)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    db.DonHang.Add(value);
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
        // Cập nhật đơn hàng
        public bool Update(DonHang value)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    DonHang record = db.DonHang.SingleOrDefault(v => v.MaDonHang == value.MaDonHang);
                    record.MaNhanVien = value.MaNhanVien;
                    record.NgayLap = value.NgayLap;
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
        // Xóa đơn hàng
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    DonHang record = db.DonHang.SingleOrDefault(v => v.MaDonHang == id);
                    db.DonHang.Remove(record);
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
        // Tìm đơn hàng
        public List<DonHang> Search(int? nhanvien, DateTime? tungay, DateTime? denngay)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    var record = from r in db.DonHang select r;
                    if (nhanvien != null) record = record.Where(r => r.MaNhanVien == nhanvien);
                    if (tungay != null) record = record.Where(r => r.NgayLap >= tungay);
                    if (denngay != null) record = record.Where(r => r.NgayLap <= denngay);
                    return record.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
