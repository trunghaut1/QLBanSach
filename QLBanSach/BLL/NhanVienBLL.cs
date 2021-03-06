﻿using QLBanSach.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanSach.BLL
{
    public class NhanVienBLL
    {
        QLBanSachEntities db = new QLBanSachEntities();
        // Lấy danh sách nhân viên
        public List<NhanVien> GetAll()
        {
            try
            {
                return db.NhanVien.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy nhân viên
        public NhanVien GetByID(int id)
        {
            try
            {
                NhanVien record = db.NhanVien.SingleOrDefault(v => v.MaNhanVien == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm nhân viên
        public bool Add(NhanVien value)
        {
            try
            {
                db.NhanVien.Add(value);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật nhân viên
        public bool Update(NhanVien value)
        {
            try
            {
                NhanVien record = db.NhanVien.SingleOrDefault(v => v.MaNhanVien == value.MaNhanVien);
                record.TenNhanVien = value.TenNhanVien;
                record.SDT = value.SDT;
                record.QuanTri = value.QuanTri;
                record.MatKhau = value.MatKhau;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa nhân viên
        public bool Delete(int id)
        {
            try
            {
                NhanVien record = db.NhanVien.SingleOrDefault(v => v.MaNhanVien == id);
                db.NhanVien.Remove(record);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm nhân viên
        public List<NhanVien> Search(string ten, string sdt, bool? quantri)
        {
            try
            {
                var record = from r in db.NhanVien select r;
                if (ten != null) record = record.Where(r => r.TenNhanVien.Contains(ten));
                if (sdt != null) record = record.Where(r => r.SDT.Contains(sdt));
                if (quantri != null) record = record.Where(r => r.QuanTri.Value == quantri.Value);
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Kiểm tra có tồn tại đơn hàng mang mã nv này hay không
        public bool CheckFK(int id)
        {
            try
            {
                int record1 = (from r in db.DonHang where r.MaNhanVien == id select r).Count();
                int record2 = (from r in db.NhapHang where r.MaNhanVien == id select r).Count();
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
