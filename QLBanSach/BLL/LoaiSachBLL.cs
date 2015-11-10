using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanSach.BLL
{
    public class LoaiSachBLL
    {
        // Lấy danh sách loại sách
        public List<LoaiSach> GetAll()
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    return db.LoaiSach.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy loại sách
        public LoaiSach GetByID(int id)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    LoaiSach record = db.LoaiSach.SingleOrDefault(v => v.MaLoai == id);
                    return record;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm loại sách
        public bool Add(LoaiSach value)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    db.LoaiSach.Add(value);
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
        // Cập nhật loại sách
        public bool Update(LoaiSach value)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    LoaiSach record = db.LoaiSach.SingleOrDefault(v => v.MaLoai == value.MaLoai);
                    record.TenLoai = value.TenLoai;
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
        // Xóa loại sách
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    LoaiSach record = db.LoaiSach.SingleOrDefault(v => v.MaLoai == id);
                    db.LoaiSach.Remove(record);
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
        // Tìm loại sách theo tên loại
        public List<LoaiSach> Search(string ten)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    var record = from r in db.LoaiSach select r;
                    if (ten != null) record = record.Where(r => r.TenLoai.Contains(ten));
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
