using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBox.DAL.Entity
{
    public class QuaTrinhHocTap
    {
        [Key]
        public string MaQTHT { get; set; }
        public string MonHoc { get; set; }
        public long Diem { get; set; }
        public string MaSV { get; set; }
        [ForeignKey("MaSV")]
        public virtual Student Student { get; set; }
        public static List<QuaTrinhHocTap> GetList(String maSV)
        {
            return new DBContext().QuaTrinhHocTapDbset.Where(e => e.MaSV == maSV).ToList();
        }
        public static long DTB(String maSV)
        {
            var db = new DBContext().QuaTrinhHocTapDbset.Where(e => e.MaSV == maSV).ToList();
            int dem = 0;
            long dtb = 0;
            foreach(var i in db)
            {
                dtb += i.Diem;
                dem++;
            }
            dtb = dtb / dem;
            return dtb;
        }
        public static void Xoa(String maSV)
        {
            var db = new DBContext();
            var ls = db.QuaTrinhHocTapDbset.Where(e => e.MaSV == maSV).ToList();
            db.QuaTrinhHocTapDbset.RemoveRange(ls);
            db.SaveChanges();
        }
    }
}
