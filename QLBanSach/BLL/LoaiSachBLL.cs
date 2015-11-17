﻿using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanSach.BLL
{
    public class LoaiSachBLL
    {
        QLBanSachEntities db = new QLBanSachEntities();
        // Lấy danh sách loại sách
        public List<LoaiSach> GetAll()
        {
            try
            {
                return db.LoaiSach.ToList();
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
                LoaiSach record = db.LoaiSach.SingleOrDefault(v => v.MaLoai == id);
                return record;
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
                db.LoaiSach.Add(value);
                db.SaveChanges();
                return true;
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
                LoaiSach record = db.LoaiSach.SingleOrDefault(v => v.MaLoai == value.MaLoai);
                record.TenLoai = value.TenLoai;
                db.SaveChanges();
                return true;
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
                LoaiSach record = db.LoaiSach.SingleOrDefault(v => v.MaLoai == id);
                db.LoaiSach.Remove(record);
                db.SaveChanges();
                return true;
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
                var record = from r in db.LoaiSach select r;
                if (ten != null) record = record.Where(r => r.TenLoai.Contains(ten));
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Kiểm tra có tồn tại sách mang mã loại này hay không
        public bool CheckFK(int id)
        {
            try
            {
                int record = (from r in db.Sach where r.MaLoai == id select r).Count();
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
