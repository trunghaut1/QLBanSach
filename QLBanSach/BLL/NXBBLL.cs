﻿using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanSach.BLL
{
    public class NXBBLL
    {
        QLBanSachEntities db = new QLBanSachEntities();
        // Lấy danh sách NXB
        public List<NXB> GetAll()
        {
            try
            {
                return db.NXB.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy NXB
        public NXB GetByID(int id)
        {
            try
            {
                NXB record = db.NXB.SingleOrDefault(v => v.MaNXB == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm NXB
        public bool Add(NXB value)
        {
            try
            {
                db.NXB.Add(value);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật NXB
        public bool Update(NXB value)
        {
            try
            {
                NXB record = db.NXB.SingleOrDefault(v => v.MaNXB == value.MaNXB);
                record.TenNXB = value.TenNXB;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa NXB
        public bool Delete(int id)
        {
            try
            {
                NXB record = db.NXB.SingleOrDefault(v => v.MaNXB == id);
                db.NXB.Remove(record);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm NXB theo nhiều điều kiện
        public List<NXB> Search(string ten)
        {
            try
            {
                var record = from r in db.NXB select r;
                if (ten != null) record = record.Where(r => r.TenNXB.Contains(ten));
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Kiểm tra có tồn tại sách mang mã nhà xuất bản này hay không
        public bool CheckFK(int id)
        {
            try
            {
                int record = (from r in db.Sach where r.MaNXB == id select r).Count();
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
