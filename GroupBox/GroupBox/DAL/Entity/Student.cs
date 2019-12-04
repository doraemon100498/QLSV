﻿using GroupBox.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBox.DAL
{
    public class Student
    {
        [Key]
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public SEX GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string TenKhoa { get; set; }
        public virtual ICollection<QuaTrinhHocTap> ListQuaTrinhHocTap { get; set; }
        public static List<Student> GetList()
        {
            return new DBContext().StudentDbset.OrderBy(e=>e.MaSV).ToList();
        }
        public static List<Student> TimKiem(String key)
        {
            List<Student> tam = new List<Student>();
            List<Student> ls = Student.GetList();
            foreach (var i in ls)
                if (i.HoTen.ToLower().Contains(key.ToLower()))
                    tam.Add(i);
            return tam;
        }
        public static void Xoa(String maSV)
        {
            var db = new DBContext();
            var obj = db.StudentDbset.Where(e => e.MaSV == maSV).FirstOrDefault();
            if (obj != null)
                db.StudentDbset.Remove(obj);
            db.SaveChanges();
        }
    }
    public enum SEX
    {
        Female, Male, Other
    }
}
