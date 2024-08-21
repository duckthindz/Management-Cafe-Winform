using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CT2_Nhom04_QuanLyQuanCafe
{
    public partial class frmQuanLyQuanCafe_KhuVucBan : Form
    {
        DBConnect db = new DBConnect();
        bool ctn = false;
        public frmQuanLyQuanCafe_KhuVucBan()
        {
            InitializeComponent();
        }
        public void MyTextBox(TextBox a)
        {
            a.BorderStyle = System.Windows.Forms.BorderStyle.None;
            a.AutoSize = false;
            a.Controls.Add(new Label()
            { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });

            a.Font=new Font("Microsoft Sans Serif", 9);
        }
        public void loadImagevsGrv()
        {
            imageList1.ImageSize = new Size(30, 35);
            imageList1.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\Add-icon.png"));
            imageList1.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\Delete-icon.png"));
            imageList1.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\save-icon.png"));
            btnAddKhu.Image = imageList1.Images[0];
            btnDeleteKhu.Image = imageList1.Images[1];
            btnSaveKhu.Image = imageList1.Images[2];
            btnAddBan.Image = imageList1.Images[0];
            btnDeleteBan.Image = imageList1.Images[1];
            btnSaveBan.Image = imageList1.Images[2];

            grvKhuVuc.Font = new Font("Microsoft Sans Serif", 9);
            grvBan.Font= new Font("Microsoft Sans Serif", 9);
        }
        private void frmQuanLyQuanCafe_KhuVucBang_Load(object sender, EventArgs e)
        {
            MyTextBox(txtMaKhu);MyTextBox(txtTenKhu);MyTextBox(txtMaBan); MyTextBox(txtTenBan);
            loadImagevsGrv();
            loadcmbTTKhu(); loadcmbTTBan();
            loadcmbKHU();
            loadGrvKhuVuc();
            loadGrvBan(cmbKhu.SelectedValue.ToString());
            ctn = true;
        }
        private void btnBanHang_Click(object sender, EventArgs e)
        {
            bool q = true;
            this.Hide();
            frmQuanLyQuanCafe_BanHang frmNew = new frmQuanLyQuanCafe_BanHang();
            frmNew.ad(q);
            frmNew.ShowDialog();
            this.Close();
        }
        private void btnQLKhachHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_QuanLyKhachHang frmNew = new frmQuanLyQuanCafe_QuanLyKhachHang();
            frmNew.ShowDialog();
            this.Close();
        }
        private void btnLoaiHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_LoaiHang frmNew = new frmQuanLyQuanCafe_LoaiHang();
            frmNew.ShowDialog();
            this.Close();
        }
        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_HangHoa frmNew = new frmQuanLyQuanCafe_HangHoa();
            frmNew.ShowDialog();
            this.Close();
        }
        private void btnKhuVuc_Ban_Click(object sender, EventArgs e)
        {
        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_ThongKe frmNew = new frmQuanLyQuanCafe_ThongKe();
            frmNew.ShowDialog();
            this.Close();
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void loadcmbTTKhu()
        {
            DataTable tb_TT = new DataTable();
            tb_TT.Columns.Add("name", typeof(string));
            tb_TT.Columns.Add("info", typeof(string));
            DataRow row= tb_TT.NewRow();
            row[0] = "T";
            row[1] = "Trống";
            tb_TT.Rows.Add(row);
            DataRow row1 = tb_TT.NewRow();
            row1[0] = "KT";
            row1[1] = "Không Trống";
            tb_TT.Rows.Add(row1);
            DataRow row2 = tb_TT.NewRow();
            row2[0] = "ALL";
            row2[1] = "Tất cả";
            tb_TT.Rows.InsertAt(row2,0);
            cmbTTKhu.DataSource = tb_TT;
            cmbTTKhu.DisplayMember = "info";
            cmbTTKhu.ValueMember = "name";
            cmbTTBan.DataSource = tb_TT;
            cmbTTBan.DisplayMember = "info";
            cmbTTBan.ValueMember = "name";
        }
        public void loadcmbTTBan()
        {
            DataTable tb_TT = new DataTable();
            tb_TT.Columns.Add("name", typeof(string));
            tb_TT.Columns.Add("info", typeof(string));
            DataRow row = tb_TT.NewRow();
            row[0] = "T";
            row[1] = "Trống";
            tb_TT.Rows.Add(row);
            DataRow row1 = tb_TT.NewRow();
            row1[0] = "KT";
            row1[1] = "Không Trống";
            tb_TT.Rows.Add(row1);
            DataRow row2 = tb_TT.NewRow();
            row2[0] = "ALL";
            row2[1] = "Tất cả";
            tb_TT.Rows.InsertAt(row2, 0);
            
            cmbTTBan.DataSource = tb_TT;
            cmbTTBan.DisplayMember = "info";
            cmbTTBan.ValueMember = "name";
        }
        public void loadcmbKHU()
        {
            DataTable tb_khu = db.getDatatable("select *from KHUVUC");
            DataRow newRow=tb_khu.NewRow();
            newRow[0] = "ALL";
            newRow[1] = "Tất cả";
            tb_khu.Rows.InsertAt(newRow,0);
            cmbKhu.DataSource = tb_khu;
            cmbKhu.DisplayMember = "tenKV";
            cmbKhu.ValueMember = "maKV";
        }
        public void loadGrvKhuVuc()
        {
            if(cmbTTKhu.SelectedValue.ToString()=="ALL")
            {
                DataTable tb_khu = db.getDatatable("select *from KHUVUC");
                grvKhuVuc.DataSource = tb_khu;
            }    
            else if(cmbTTKhu.SelectedValue.ToString()=="KT")
            {
                DataTable tb_khu = db.getDatatable("select * from KHUVUC where trangThai='1'");
                grvKhuVuc.DataSource = tb_khu;
            }    
            else
            {
                DataTable tb_khu = db.getDatatable("select * from KHUVUC where trangThai='0'");
                grvKhuVuc.DataSource = tb_khu;
            }    
        }
        public void loadGrvBan(string ma)
        {
            if(cmbKhu.GetItemText(cmbKhu.SelectedValue.ToString())!="ALL")
            {
                if (cmbTTKhu.SelectedValue.ToString() == "ALL")
                {
                    DataTable tb_Ban = db.getDatatable("select *from BANCF");
                    DataView dataView = new DataView(tb_Ban);
                    dataView.RowFilter = "maKV='" + ma + "'";
                    grvBan.DataSource = dataView;
                }
                else if (cmbTTKhu.SelectedValue.ToString() == "KT")
                {
                    DataTable tb_Ban = db.getDatatable("select * from BANCF where thuocTinh='1'");
                    DataView dataView = new DataView(tb_Ban);
                    dataView.RowFilter = "maKV='" + ma + "'";
                    grvBan.DataSource = dataView;
                }
                else
                {
                    DataTable tb_Ban = db.getDatatable("select * from BANCF where thuocTinh='0'");
                    DataView dataView = new DataView(tb_Ban);
                    dataView.RowFilter = "maKV='" + ma + "'";
                    grvBan.DataSource = dataView;
                }
            }   
            else
            {
                if (cmbTTKhu.SelectedValue.ToString() == "ALL")
                {
                    DataTable tb_Ban = db.getDatatable("select *from BANCF");
                    grvBan.DataSource = tb_Ban;
                }
                else if (cmbTTKhu.SelectedValue.ToString() == "KT")
                {
                    DataTable tb_Ban = db.getDatatable("select * from BANCF where thuocTinh='1'");
                    grvBan.DataSource = tb_Ban;
                }
                else
                {
                    DataTable tb_Ban = db.getDatatable("select * from BANCF where thuocTinh='0'");
                    grvBan.DataSource = tb_Ban;
                }
            }    
        }
        private void cmbTTKhu_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrvKhuVuc();
        }
        private void cmbTTBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTTBan.SelectedValue.ToString() == "ALL")
            {
                if(ctn==true)
                {
                    DataTable tb_Ban = db.getDatatable("select *from BANCF where maKV='" + cmbKhu.GetItemText(cmbKhu.SelectedValue.ToString()) + "'");
                    grvBan.DataSource = tb_Ban;
                }
                else
                {
                    DataTable tb_Ban = db.getDatatable("select *from BANCF");
                    grvBan.DataSource = tb_Ban;
                }
            }
            else if (cmbTTBan.SelectedValue.ToString() == "KT")
            {
                string ma = cmbKhu.GetItemText(cmbKhu.SelectedValue.ToString());
                if(ma=="ALL")
                {
                    DataTable tb_Ban = db.getDatatable("select * from BANCF where thuocTinh='1'");
                    grvBan.DataSource = tb_Ban;
                }
                else
                {
                    DataTable tb_Ban = db.getDatatable("select * from BANCF where thuocTinh='1'");
                    DataView dataview = new DataView(tb_Ban);
                    dataview.RowFilter = "maKV='" + ma + "'";
                    grvBan.DataSource = dataview;
                }
            }
            else if(cmbTTBan.SelectedValue.ToString() == "T")
            {
                string ma = cmbKhu.GetItemText(cmbKhu.SelectedValue.ToString());
                if (ma == "ALL")
                {
                    DataTable tb_Ban = db.getDatatable("select * from BANCF where thuocTinh='0'");
                    grvBan.DataSource = tb_Ban;
                }
                else
                {
                    DataTable tb_Ban = db.getDatatable("select * from BANCF where thuocTinh='0'");
                    DataView dataview = new DataView(tb_Ban);
                    dataview.RowFilter = "maKV='" + ma + "'";
                    grvBan.DataSource = dataview;
                }
            }
        }
        private void cmbKhu_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrvBan(cmbKhu.SelectedValue.ToString());
        }
        private void bingdingKhu()
        {
            txtMaKhu.DataBindings.Clear();
            txtMaKhu.DataBindings.Add("Text", grvKhuVuc.DataSource, "maKV");
            txtTenKhu.DataBindings.Clear();
            txtTenKhu.DataBindings.Add("Text", grvKhuVuc.DataSource, "tenKV");
        }
        private void bingdingBan()
        {
            txtMaBan.DataBindings.Clear();
            txtMaBan.DataBindings.Add("Text", grvBan.DataSource, "maBan");
            txtTenBan.DataBindings.Clear();
            txtTenBan.DataBindings.Add("Text", grvBan.DataSource, "tenBan");
        }
        private void grvKhuVuc_Click(object sender, EventArgs e)
        {
            bingdingKhu();
        }

        private void grvBan_Click(object sender, EventArgs e)
        {
            bingdingBan();
        }

        private void btnAddKhu_Click(object sender, EventArgs e)
        {
            if (txtMaKhu.Text.Length > 0)
            {
                try
                {
                    string sql = "select *from KHUVUC";
                    DataTable tb_Khu = db.getDatatable(sql);
                    DataRow nRow = tb_Khu.NewRow();
                    nRow["maKV"] = txtMaKhu.Text;
                    nRow["tenKV"]=txtTenKhu.Text;
                    nRow["trangThai"] = 0;
                    tb_Khu.Rows.Add(nRow);
                    int kq = db.updateDatabase(sql, tb_Khu);
                    loadGrvKhuVuc();
                    loadcmbKHU();
                    if (kq > 0)
                        MessageBox.Show("thêm thành công!");
                    else
                        MessageBox.Show("thêm thất bại!");
                }
                catch (Exception ex) { MessageBox.Show("Thất bại"); }
            }
        }
        private void btnDeleteKhu_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select *from KHUVUC";
                DataTable tb_Khu = db.getDatatable(sql);
                DataColumn[] key = new DataColumn[1];
                key[0] = tb_Khu.Columns["maKV"];
                tb_Khu.PrimaryKey = key;
                DataRow row = tb_Khu.Rows.Find(txtMaKhu.Text);
                if (row != null)
                    row.Delete();
                int kq = db.updateDatabase(sql, tb_Khu);
                loadGrvKhuVuc();
                loadcmbKHU();
                if (kq > 0)
                    MessageBox.Show("xoá thành công!");
                else
                    MessageBox.Show("xoá thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Thất bại"); }
        }
        private void btnSaveKhu_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select *from KHUVUC";
                DataTable tb_Khu = db.getDatatable(sql);
                DataColumn[] key = new DataColumn[1];
                key[0] = tb_Khu.Columns["maKV"];
                tb_Khu.PrimaryKey = key;
                DataRow row = tb_Khu.Rows.Find(txtMaKhu.Text);
                if (row != null)
                {
                    row["tenKV"] = txtTenKhu.Text;
                    DataGridViewRow viewRow = grvKhuVuc.CurrentRow;
                    string tt = viewRow.Cells[2].Value.ToString();
                    row["trangThai"]=tt;
                }    
                int kq = db.updateDatabase(sql, tb_Khu);
                loadGrvKhuVuc();
                loadcmbKHU();
                if (kq > 0)
                    MessageBox.Show("sửa thành công!");
                else
                    MessageBox.Show("sửa thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Thất bại"); }
        }

        private void btnAddBan_Click(object sender, EventArgs e)
        {
            if (txtMaKhu.Text.Length > 0)
            {
                try
                {
                    string sql = "select *from BANCF";
                    DataTable tb_Khu = db.getDatatable(sql);
                    DataRow nRow = tb_Khu.NewRow();
                    nRow["maBan"] = txtMaBan.Text;
                    nRow["tenBan"] = txtTenBan.Text;
                    nRow["thuocTinh"] = 0;
                    if(cmbKhu.SelectedValue.ToString()=="ALL")
                    {
                        MessageBox.Show("Vui lòng chọn khu để thêm Bàn!");
                        return;
                    }    
                    else
                        nRow["maKV"]=cmbKhu.SelectedValue.ToString();
                    tb_Khu.Rows.Add(nRow);
                    int kq = db.updateDatabase(sql, tb_Khu);
                    loadGrvBan(cmbKhu.SelectedValue.ToString());
                    if (kq > 0)
                        MessageBox.Show("thêm thành công!");
                    else
                        MessageBox.Show("thêm thất bại!");
                }
                catch (Exception ex) { MessageBox.Show("Thất bại"); }
            }
        }

        private void btnDeleteBan_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select *from BANCF";
                DataTable tb_Khu = db.getDatatable(sql);
                DataColumn[] key = new DataColumn[1];
                key[0] = tb_Khu.Columns["maBan"];
                tb_Khu.PrimaryKey = key;
                DataRow row = tb_Khu.Rows.Find(txtMaBan.Text);
                if (row != null)
                    row.Delete();
                int kq = db.updateDatabase(sql, tb_Khu);
                loadGrvBan(cmbKhu.SelectedValue.ToString());
                if (kq > 0)
                    MessageBox.Show("xoá thành công!");
                else
                    MessageBox.Show("xoá thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Thất bại"); }
        }

        private void btnSaveBan_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select *from BANCF";
                DataTable tb_Khu = db.getDatatable(sql);
                DataColumn[] key = new DataColumn[1];
                key[0] = tb_Khu.Columns["maBan"];
                tb_Khu.PrimaryKey = key;
                DataRow row = tb_Khu.Rows.Find(txtMaBan.Text);
                if (row != null)
                {
                    row["tenBan"] = txtTenBan.Text;
                    DataGridViewRow viewRow = grvBan.CurrentRow;
                    string tt = viewRow.Cells[3].Value.ToString();
                    row["thuocTinh"] = tt;
                }
                int kq = db.updateDatabase(sql, tb_Khu);
                loadGrvBan(cmbKhu.SelectedValue.ToString());
                if (kq > 0)
                    MessageBox.Show("sửa thành công!");
                else
                    MessageBox.Show("sửa thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Thất bại"); }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogIn frmLogin = new frmLogIn();
            frmLogin.ShowDialog();
            this.Close();
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_QuanLyNhanVien frmNV = new frmQuanLyQuanCafe_QuanLyNhanVien();
            frmNV.ShowDialog();
            this.Close();
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            frmResetPassword frmRS = new frmResetPassword();
            frmRS.Show();
        }
    }
}
