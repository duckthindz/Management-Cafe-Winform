using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CT2_Nhom04_QuanLyQuanCafe
{
    public partial class frmQuanLyQuanCafe_QuanLyNhanVien : Form
    {
        DBConnect db=new DBConnect();
        public frmQuanLyQuanCafe_QuanLyNhanVien()
        {
            InitializeComponent();
        }
        public void myTextBox(TextBox a)
        {
            a.BorderStyle=System.Windows.Forms.BorderStyle.None;
            a.AutoSize = false;
            a.Controls.Add(new Label()
            { Height = 1,Dock=DockStyle.Bottom,BackColor=Color.Black });
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

        private void btnQuit_Click(object sender, EventArgs e)
        {
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
            this.Hide();
            frmQuanLyQuanCafe_KhuVucBan frmNew = new frmQuanLyQuanCafe_KhuVucBan();
            frmNew.ShowDialog();
            this.Close();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_ThongKe frmNew = new frmQuanLyQuanCafe_ThongKe();
            frmNew.ShowDialog();
            this.Close();
        }
        private void loadImage()
        {
            ImageList image=new ImageList();
            image.ImageSize = new Size(170, 90);
            image.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\people.png"));
            pictureBox1.Image = image.Images[0];
        }
        private void frmQuanLyQuanCafe_QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            myTextBox(txtChucVu);myTextBox(txtDiaChi);myTextBox(txtFind);myTextBox(txtMaNV);
            myTextBox(txtMK);myTextBox(txtSDT);myTextBox(txtTenNV);
            loadImage();
            loadGrvNV();
        }
        private void txtFind_Click(object sender, EventArgs e)
        {
            txtFind.ResetText();
            txtFind.Focus();
        }
        public void loadGrvNV()
        {
            string sql = "select maNV,HoTenNV,sdtNV,diaChiNV,chucVu,matKhauNV,gioiTinhNV from NHANVIEN";
            DataTable tb_NV = db.getDatatable(sql);
            grvNhanVien.DataSource= tb_NV;
        }
        private void dataBinDing()
        {
            txtMaNV.DataBindings.Clear();
            txtMaNV.DataBindings.Add("Text", grvNhanVien.DataSource, "maNV");
            txtTenNV.DataBindings.Clear();
            txtTenNV.DataBindings.Add("Text", grvNhanVien.DataSource, "HoTenNV");
            txtSDT.DataBindings.Clear();
            txtSDT.DataBindings.Add("Text", grvNhanVien.DataSource, "sdtNV");
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", grvNhanVien.DataSource, "diaChiNV");
            txtChucVu.DataBindings.Clear();
            txtChucVu.DataBindings.Add("Text", grvNhanVien.DataSource, "chucVu");
            txtMK.DataBindings.Clear();
            txtMK.DataBindings.Add("Text", grvNhanVien.DataSource, "matKhauNV");
            ckbNu.DataBindings.Clear();
            DataGridViewRow viewRow = grvNhanVien.CurrentRow;
            string gt = viewRow.Cells["gioiTinhNV"].Value.ToString();
            if (gt == "Nam")
                ckbNu.Checked = false;
            else ckbNu.Checked = true;
        }

        private void grvNhanVien_Click(object sender, EventArgs e)
        {
            dataBinDing();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string find = txtFind.Text;
            string sql = "select *from NHANVIEN where ((HoTenNV LIKE N'%" + find + "%') or sdtNV='" + find + "') ";
            DataTable tb_KH = new DataTable();
            tb_KH = db.getDatatable(sql);
            grvNhanVien.DataSource = tb_KH;
        }
        private bool kiemTraMaNV(string ma)
        {
            string sql = "select count(*) from NHANVIEN where maNV='" + ma + "'";
            int kq = (int)db.getScalar(sql);
            if (kq > 0)
                return false;
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Length > 0)
            {
                if (kiemTraMaNV(txtMaNV.Text) == true)
                {
                    try
                    {
                        string sql = "select *from NHANVIEN";
                        DataTable tb_NV = db.getDatatable(sql);

                        DataRow newRow = tb_NV.NewRow();
                        newRow["maNV"] = txtMaNV.Text;
                        newRow["HoTenNV"] = txtTenNV.Text;
                        newRow["sdtNV"] = txtSDT.Text;
                        newRow["diaChiNV"] = txtDiaChi.Text;
                        newRow["chucVu"] = txtChucVu.Text;
                        newRow["matKhauNV"] = txtMK.Text;
                        if (ckbNu.Checked == true)
                            newRow["gioiTinhNV"] = "Nu";
                        else
                            newRow["gioiTinhNV"] = "Nam";
                        tb_NV.Rows.Add(newRow);
                        int kq = db.updateDatabase(sql, tb_NV);
                        loadGrvNV();
                        if (kq > 0)
                            MessageBox.Show("Thêm thành công");
                        else
                            MessageBox.Show("Thêm thất bại");
                    }
                    catch(Exception ex) { MessageBox.Show("Thất bại"); }
                }
                else
                    MessageBox.Show("Bị trùng khoá");
            }
            else
                txtMaNV.Focus();    
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtMaNV.Text.Length>0)
            {
                try
                {
                    string sql = "select *from NHANVIEN";
                    DataTable tb_NV = db.getDatatable(sql);
                    DataColumn[] key = new DataColumn[1];
                    key[0] = tb_NV.Columns["maNV"];
                    tb_NV.PrimaryKey = key;

                    DataRow row = tb_NV.Rows.Find(txtMaNV.Text);
                    if (row != null)
                        row.Delete();
                    int kq = db.updateDatabase(sql, tb_NV);
                    loadGrvNV();
                    if (kq > 0)
                        MessageBox.Show("Xoá thành công");
                    else
                        MessageBox.Show("Xoá thất bại");
                }
                catch (Exception ex) { MessageBox.Show("Thất bại"); }
            }    
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Length > 0)
            {
                try
                {
                    string sql = "select *from NHANVIEN";
                    DataTable tb_NV = db.getDatatable(sql);
                    DataColumn[] key = new DataColumn[1];
                    key[0] = tb_NV.Columns["maNV"];
                    tb_NV.PrimaryKey = key;

                    DataRow row = tb_NV.Rows.Find(txtMaNV.Text);
                    if (row != null)
                    {
                        row["HoTenNV"] = txtTenNV.Text;
                        row["sdtNV"] = txtSDT.Text;
                        row["diaChiNV"] = txtDiaChi.Text;
                        row["chucVu"] = txtChucVu.Text;
                        row["matKhauNV"] = txtMK.Text;
                        if (ckbNu.Checked == true)
                            row["gioiTinhNV"] = "Nu";
                        else
                            row["gioiTinhNV"] = "Nam";
                        int kq = db.updateDatabase(sql, tb_NV);
                        loadGrvNV();
                        if (kq > 0)
                            MessageBox.Show("Sửa thành công");
                        else
                            MessageBox.Show("Sửa thất bại");
                    }   
                }
                catch (Exception ex) { MessageBox.Show("Thất bại"); }
            }
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
