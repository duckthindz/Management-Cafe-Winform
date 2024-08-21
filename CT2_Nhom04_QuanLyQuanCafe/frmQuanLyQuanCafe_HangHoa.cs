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
    public partial class frmQuanLyQuanCafe_HangHoa : Form
    {
        DBConnect db=new DBConnect();
        public frmQuanLyQuanCafe_HangHoa()
        {
            InitializeComponent();
        }

        private void frmQuanLyQuanCafe_HangHoa_Load(object sender, EventArgs e)
        {
            loadImage();
            myTextBox(txtMaHH);myTextBox(txtGia);myTextBox(txtTenHH);
            loadcmbTLH();
            string item = cmbTenLoaihang.GetItemText(cmbTenLoaihang.SelectedValue);
            if(item=="ALL")
            {
                DataTable tb_SP = db.getDatatable("select * from SANPHAM");
                grvLoaiHang.DataSource = tb_SP;
            }
        }
        public void loadcmbTLH()
        {
            string sql = "select *from LOAISANPHAM";
            DataTable dtLH = new DataTable();
            dtLH = db.getDatatable(sql);
            DataRow newRow = dtLH.NewRow();
            newRow[0] = "ALL";
            newRow[1] = "Tất Cả";
            dtLH.Rows.InsertAt(newRow, 0);
            cmbTenLoaihang.DataSource = dtLH;
            cmbTenLoaihang.DisplayMember = "tenL";
            cmbTenLoaihang.ValueMember = "maLSP";
        }
        public void myTextBox(TextBox a)
        {
            a.BorderStyle=System.Windows.Forms.BorderStyle.None;
            a.AutoSize = false;
            a.Controls.Add(new Label()
            { Height =1,Dock=DockStyle.Bottom,BackColor=Color.Black});
        }
        public void loadImage()
        {
            imageList1.ImageSize = new Size(30, 35);
            // Thay đổi kích thước hình ảnh
            imageList1.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\Add-icon.png"));
            imageList1.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\Delete-icon.png"));
            imageList1.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\save-icon.png"));
            btnAdd.Image = imageList1.Images[0];
            btnDelete.Image = imageList1.Images[1];
            btnSave.Image = imageList1.Images[2];
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

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void loadGrvLH(string ma)
        {
            DataTable tb_SP = db.getDatatable("select * from SANPHAM");
            DataView dataView = new DataView(tb_SP);
            dataView.RowFilter = "maLSP='"+ma+"'";
            grvLoaiHang.DataSource = dataView;
        }
        private void cmbTenLoaihang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = cmbTenLoaihang.GetItemText(cmbTenLoaihang.SelectedValue);
            if (item == "ALL")
            {
                DataTable tb_SP = db.getDatatable("select * from SANPHAM");
                grvLoaiHang.DataSource = tb_SP;
            }
            else
                loadGrvLH(cmbTenLoaihang.SelectedValue.ToString());
        }
        private void binding()
        {
            txtMaHH.DataBindings.Clear();
            txtMaHH.DataBindings.Add("Text", grvLoaiHang.DataSource, "maSP");
            txtTenHH.DataBindings.Clear();
            txtTenHH.DataBindings.Add("Text", grvLoaiHang.DataSource, "tenSP");
            txtGia.DataBindings.Clear();
            txtGia.DataBindings.Add("Text", grvLoaiHang.DataSource, "giaSP");
        }

        private void grvLoaiHang_Click(object sender, EventArgs e)
        {
            binding();
            DataGridViewRow row = grvLoaiHang.CurrentRow;
            if (row.Cells[3].Value.ToString().Length > 0)
            {
                byte[] imgBytes = row.Cells[4].Value as byte[];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imgBytes);
                ImageList image = new ImageList();
                image.ImageSize = new Size(233, 121);
                image.Images.Add(Image.FromStream(ms));
                picBox1.Image = image.Images[0];
            }
            else
            {
                ImageList image = new ImageList();
                image.ImageSize = new Size(1, 1);
                image.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\find.png"));
                picBox1.Image = image.Images[0];
            }    
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtMaHH.Text.Length > 0)
            {
                try
                {
                    string sql = "select * from SANPHAM";
                    DataTable tb_SP=db.getDatatable(sql);
                    DataRow newRow=tb_SP.NewRow();
                    newRow["maSP"]=txtMaHH.Text;
                    newRow["tenSP"] = txtTenHH.Text;
                    newRow["giaSP"] = txtGia.Text;
                    if (cmbTenLoaihang.SelectedValue == "ALL")
                        MessageBox.Show("Vui lòng  chọn loại SP");
                    else
                    {
                        newRow["maLSP"] = cmbTenLoaihang.SelectedValue.ToString();
                        tb_SP.Rows.Add(newRow);
                        int kq = db.updateDatabase(sql, tb_SP);
                        if (kq > 0) { MessageBox.Show("Thêm thành công"); }
                        else MessageBox.Show("Thêm thất bại");
                        loadGrvLH(cmbTenLoaihang.SelectedValue.ToString());
                    }    
                }
                catch (Exception ex) { MessageBox.Show("Thất bại!"); }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = "select *from SANPHAM";
            DataTable tb_SP = new DataTable();
            tb_SP = db.getDatatable(sql);
            DataColumn[] key = new DataColumn[1];
            key[0] = tb_SP.Columns["maSP"];
            tb_SP.PrimaryKey = key;

            DataGridViewRow dataViewRow = grvLoaiHang.CurrentRow;
            string maSP = dataViewRow.Cells[0].Value.ToString();
            try
            {
                DataRow row = tb_SP.Rows.Find(maSP);
                if (row != null)
                {
                    row.Delete();
                }
                int kq = db.updateDatabase(sql, tb_SP);
                loadGrvLH(cmbTenLoaihang.SelectedValue.ToString());
                if (kq > 0)
                    MessageBox.Show("xoá thành công");
                else
                    MessageBox.Show("Xoá thất bại");
                
            }
            catch(Exception ex) { MessageBox.Show("thất bại"); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sql = "select *from SANPHAM";
            DataTable tb_SP = new DataTable();
            tb_SP = db.getDatatable(sql);
            DataColumn[] key = new DataColumn[1];
            key[0] = tb_SP.Columns["maSP"];
            tb_SP.PrimaryKey = key;

            DataGridViewRow dataViewRow = grvLoaiHang.CurrentRow;
            string maSP = dataViewRow.Cells[0].Value.ToString();
            try
            {
                DataRow row = tb_SP.Rows.Find(maSP);
                if (row != null)
                {
                    row["tenSP"] = txtTenHH.Text;
                    row["giaSP"] = txtGia.Text;
                }
                int kq = db.updateDatabase("select *from SANPHAM", tb_SP);
                if (kq > 0)
                    MessageBox.Show("sửa thành công");
                else
                    MessageBox.Show("sửa thất bại");
                loadGrvLH(cmbTenLoaihang.SelectedValue.ToString());
            }
            catch (Exception ex) { MessageBox.Show("thất bại"); }
        }

        private void grvLoaiHang_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuGrv.Show();
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogIn frmLogIn = new frmLogIn();
            frmLogIn.ShowDialog();
            this.Close();
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_QuanLyNhanVien frmNV=new frmQuanLyQuanCafe_QuanLyNhanVien();
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
