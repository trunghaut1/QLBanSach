using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanSach.BLL
{
    public class CTDonHangBLL
    {
        QLBanSachEntities db = new QLBanSachEntities();
        // Lấy chi tiết đơn hàng theo mã đơn hàng
        // >> Dùng hàm Search
        public List<CTDonHang> Search(int? madonhang, int? masach)
        {
            try
            {
                var record = from r in db.CTDonHang select r;
                if (madonhang != null) record = record.Where(r => r.MaDonHang == madonhang);
                if (masach != null) record = record.Where(r => r.MaSach == masach);
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm chi tiết đơn hàng
        public bool Add(CTDonHang value)
        {
            try
            {
                db.CTDonHang.Add(value);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Thêm List chi tiết đơn hàng
        public bool AddList(List<CTDonHang> value)
        {
            try
            {
                foreach (var record in value)
                {
                    db.CTDonHang.Add(record);
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật chi tiết đơn hàng
        public bool Update(CTDonHang value)
        {
            try
            {
                CTDonHang record = db.CTDonHang.SingleOrDefault(v => v.MaDonHang == value.MaDonHang && v.MaSach == value.MaSach);
                record.SoLuong = value.SoLuong;
                record.DonGia = value.DonGia;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa chi tiết đơn hàng
        public bool Delete(int madonhang, int masach)
        {
            try
            {
                CTDonHang record = db.CTDonHang.SingleOrDefault(v => v.MaDonHang == madonhang && v.MaSach == masach);
                db.CTDonHang.Remove(record);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
