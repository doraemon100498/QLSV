using GroupBox.DAL;
using GroupBox.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadHome(0, Student.GetList());
            }
            catch (Exception ex) { }
        }
        private void LoadHome(int index,List<Student> ls)
        {
            listBox1.Items.Clear();
            lsbCNTT.Items.Clear();
            lsbVatLy.Items.Clear();
            lstVan.Items.Clear();
            txtHoTen.Text = "";
            ckbGioiTinh.Checked = false;
            dtpNgaySinh.Value = DateTime.Now;
            lblDTB.Text = "";
            foreach (var i in ls)
            {
                if (i.GioiTinh == SEX.Male)
                    listBox1.Items.Add("Anh " + i.HoTen);
                else if (i.GioiTinh == SEX.Female)
                    listBox1.Items.Add("Chị " + i.HoTen);
            }
            txtHoTen.Text = ls[index].HoTen;
            if (ls[index].GioiTinh == SEX.Female)
                ckbGioiTinh.Checked = false;
            else if (ls[index].GioiTinh == SEX.Male)
                ckbGioiTinh.Checked = true;
            dtpNgaySinh.Value = ls[index].NgaySinh;
            lblDTB.Text = Convert.ToString(QuaTrinhHocTap.DTB(ls[index].MaSV));
            List<QuaTrinhHocTap> qt = QuaTrinhHocTap.GetList(ls[index].MaSV);
            if (ls[index].TenKhoa == "CNTT")
            {
                tabControl1.SelectedTab = tabPage3;
                foreach (var i in qt)
                    lsbCNTT.Items.Add(i.MonHoc + ":" + i.Diem);
            }
            else if (ls[index].TenKhoa == "Văn")
            {
                tabControl1.SelectedTab = tabPage1;
                foreach (var i in qt)
                    lstVan.Items.Add(i.MonHoc + ":" + i.Diem);
            }
            else if (ls[index].TenKhoa == "Vật lý")
            {
                tabControl1.SelectedTab = tabPage2;
                foreach (var i in qt)
                    lsbVatLy.Items.Add(i.MonHoc + ":" + i.Diem);
            }
        }
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                int index = listBox1.SelectedIndex;
                List<Student> ls = Student.GetList();
                lsbCNTT.Items.Clear();
                lsbVatLy.Items.Clear();
                lstVan.Items.Clear();
                txtHoTen.Text = "";
                ckbGioiTinh.Checked = false;
                dtpNgaySinh.Value = DateTime.Now;
                lblDTB.Text = "";
                txtHoTen.Text = ls[index].HoTen;
                if (ls[index].GioiTinh == SEX.Female)
                    ckbGioiTinh.Checked = false;
                else if (ls[index].GioiTinh == SEX.Male)
                    ckbGioiTinh.Checked = true;
                dtpNgaySinh.Value = ls[index].NgaySinh;
                lblDTB.Text = Convert.ToString(QuaTrinhHocTap.DTB(ls[index].MaSV));
                List<QuaTrinhHocTap> qt = QuaTrinhHocTap.GetList(ls[index].MaSV);
                if (ls[index].TenKhoa == "CNTT")
                {
                    tabControl1.SelectedTab = tabPage3;
                    foreach (var i in qt)
                        lsbCNTT.Items.Add(i.MonHoc + ":" + i.Diem);
                }
                else if (ls[index].TenKhoa == "Văn")
                {
                    tabControl1.SelectedTab = tabPage1;
                    foreach (var i in qt)
                        lstVan.Items.Add(i.MonHoc + ":" + i.Diem);
                }
                else if (ls[index].TenKhoa == "Vật lý")
                {
                    tabControl1.SelectedTab = tabPage2;
                    foreach (var i in qt)
                        lsbVatLy.Items.Add(i.MonHoc + ":" + i.Diem);
                }
            }
        }
        private void ToolStripTextBox1_Click(object sender, EventArgs e)
        {
            try
            {
                List<Student> ls=new List<Student>();
                String key = txtTimKiem.Text;
                if (key == "")
                    ls = Student.GetList();
                else ls = Student.TimKiem(key);
                if (ls != null)
                    LoadHome(0, ls);
            }
            catch (Exception ex) { }           
        }
        private void TsbXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn xóa sinh viên này không?", "Thông báo",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                if (listBox1.SelectedIndex != -1)
                {
                    int index = listBox1.SelectedIndex;
                    List<Student> ls = Student.GetList();
                    QuaTrinhHocTap.Xoa(ls[index].MaSV);
                    Student.Xoa(ls[index].MaSV);
                    if (index > 0)
                        LoadHome(0, Student.GetList());
                    else
                    {
                        listBox1.Items.Clear();
                        lsbCNTT.Items.Clear();
                        lsbVatLy.Items.Clear();
                        lstVan.Items.Clear();
                        txtHoTen.Text = "";
                        ckbGioiTinh.Checked = false;
                        dtpNgaySinh.Value = DateTime.Now;
                        lblDTB.Text = "";
                    }
                }
            }
        }
    }
}
