using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanSach.BLL
{
    public class TacGiaBLL
    {
        // Lấy danh sách tác giả
        public List<TacGia> GetAll()
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    return db.TacGia.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy tác giả
        public TacGia GetByID(int id)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    TacGia record = db.TacGia.SingleOrDefault(v => v.MaTacGia == id);
                    return record;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm tác giả
        public bool Add(TacGia value)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    db.TacGia.Add(value);
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
        // Cập nhật tác giả
        public bool Update(TacGia value)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    TacGia record = db.TacGia.SingleOrDefault(v => v.MaTacGia == value.MaTacGia);
                    record.TenTacGia = value.TenTacGia;
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
        // Xóa tác giả
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    TacGia record = db.TacGia.SingleOrDefault(v => v.MaTacGia == id);
                    db.TacGia.Remove(record);
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
        // Tìm tác giả
        public List<TacGia> Search(string ten)
        {
            try
            {
                using (var db = new QLBanSachEntities())
                {
                    var record = from r in db.TacGia select r;
                    if (ten != null) record = record.Where(r => r.TenTacGia.Contains(ten));
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
