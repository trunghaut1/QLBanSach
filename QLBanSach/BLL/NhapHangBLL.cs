using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanSach.BLL
{
    public class NhapHangBLL
    {
        QLBanSachEntities db = new QLBanSachEntities();
        // Lấy danh sách nhập hàng
        public List<NhapHang> GetAll()
        {
            try
            {
                return db.NhapHang.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy nhập hàng
        public NhapHang GetByID(int id)
        {
            try
            {
                NhapHang record = db.NhapHang.SingleOrDefault(v => v.MaNhapHang == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm nhập hàng
        public bool Add(NhapHang value)
        {
            try
            {
                db.NhapHang.Add(value);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật nhập hàng
        public bool Update(NhapHang value)
        {
            try
            {
                NhapHang record = db.NhapHang.SingleOrDefault(v => v.MaNhapHang == value.MaNhapHang);
                record.MaNhanVien = value.MaNhanVien;
                record.NgayLap = value.NgayLap;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa nhập hàng
        public bool Delete(int id)
        {
            try
            {
                NhapHang record = db.NhapHang.SingleOrDefault(v => v.MaNhapHang == id);
                db.NhapHang.Remove(record);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm nhập hàng
        public List<NhapHang> Search(int? nhanvien, DateTime? tungay, DateTime? denngay)
        {
            try
            {
                var record = from r in db.NhapHang select r;
                if (nhanvien != null) record = record.Where(r => r.MaNhanVien == nhanvien);
                if (tungay != null) record = record.Where(r => r.NgayLap >= tungay);
                if (denngay != null) record = record.Where(r => r.NgayLap <= denngay);
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
