using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLBanSach.BLL
{
    public class TacGiaBLL
    {
        QLBanSachEntities db = new QLBanSachEntities();
        // Lấy danh sách tác giả
        public List<TacGia> GetAll()
        {
            try
            {
                return db.TacGia.ToList();
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
                TacGia record = db.TacGia.SingleOrDefault(v => v.MaTacGia == id);
                return record;
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
                db.TacGia.Add(value);
                db.SaveChanges();
                return true;
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
                TacGia record = db.TacGia.SingleOrDefault(v => v.MaTacGia == value.MaTacGia);
                record.TenTacGia = value.TenTacGia;
                db.SaveChanges();
                return true;
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
                TacGia record = db.TacGia.SingleOrDefault(v => v.MaTacGia == id);
                db.TacGia.Remove(record);
                db.SaveChanges();
                return true;
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
                var record = from r in db.TacGia select r;
                if (ten != null) record = record.Where(r => r.TenTacGia.Contains(ten));
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Kiểm tra có tồn tại sách mang tác giả này hay không
        public bool CheckFK(int id)
        {
            try
            {
                int record = (from r in db.Sach where r.MaTacGia == id select r).Count();
                if (record > 0) return false;
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
