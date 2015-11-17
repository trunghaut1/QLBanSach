using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace QLBanSach.BLL
{
    public class SachBLL 
    {
        QLBanSachEntities db = new QLBanSachEntities();
        // Lấy danh sách sách
        public List<Sach> GetAll()
        {
            try
            {
                return db.Sach.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy sách
        public Sach GetByID(int id)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    Sach record = db.Sach.SingleOrDefault(v => v.MaSach == id);
                    return record;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm sách
        public bool Add(Sach value)
        {
            try
            {

                db.Sach.Add(value);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật sách
        public bool Update(Sach value)
        {
            try
            {
                Sach record = db.Sach.SingleOrDefault(v => v.MaSach == value.MaSach);
                record.TenSach = value.TenSach;
                record.MaLoai = value.MaLoai;
                record.MaNXB = value.MaNXB;
                record.MaTacGia = value.MaTacGia;
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
        // Xóa sách
        public bool Delete(int id)
        {
            try
            {
                Sach record = db.Sach.SingleOrDefault(v => v.MaSach == id);
                db.Sach.Remove(record);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm sách
        public List<Sach> Search(string ten, int? loai, int? nxb, int? tacgia, int? giatu, int? giaden, bool? conhang)
        {
            try
            {
                var record = from r in db.Sach select r;
                if (ten != null) record = record.Where(r => r.TenSach.Contains(ten));
                if (loai != null) record = record.Where(r => r.MaLoai == loai);
                if (nxb != null) record = record.Where(r => r.MaNXB == nxb);
                if (tacgia != null) record = record.Where(r => r.MaTacGia == tacgia);
                if (giatu != null) record = record.Where(r => r.DonGia >= giatu);
                if (giaden != null) record = record.Where(r => r.DonGia <= giaden);
                if (conhang != null && conhang == true) record = record.Where(r => r.SoLuong > 0);
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Kiểm tra có tồn tại đơn hàng mang mã sách này hay không
        public bool CheckFK(int id)
        {
            try
            {
                int record1 = (from r in db.CTDonHang where r.MaSach == id select r).Count();
                int record2 = (from r in db.CTNhapHang where r.MaSach == id select r).Count();
                if (record1 > 0 || record2 > 0) return false;
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
