using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;


namespace CT2_Nhom04_QuanLyQuanCafe
{
    public partial class frmQuanLyQuanCafe_LoaiHang : Form
    {
        DBConnect db = new DBConnect();
        public frmQuanLyQuanCafe_LoaiHang()
        {
            InitializeComponent();
        }
        private void frmQuanLyQuanCafe_LoaiHang_Load(object sender, EventArgs e)
        {
            loadImage();
            myTextBox(txtMaLH);myTextBox(txtMoTa);myTextBox(txtTenLH);
            loadGrvLSP();
        }
        public void myTextBox(TextBox a)
        {
            a.BorderStyle=System.Windows.Forms.BorderStyle.None;
            a.AutoSize = false;
            a.Controls.Add(new Label()
            { Height=1,Dock=DockStyle.Bottom,BackColor=Color.Black});
        }
        public void loadImage()
        {
            imageList1.ImageSize = new Size(197, 107);
            // Thay đổi kích thước hình ảnh
            imageList1.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\CF.jpg"));
            button2.Image = imageList1.Images[0];
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
            frmQuanLyQuanCafe_ThongKe frmHH = new frmQuanLyQuanCafe_ThongKe();
            frmHH.ShowDialog();
            this.Close();
        }
        public void loadGrvLSP()
        {
            DataTable tb_LSP = db.getDatatable("select *from LOAISANPHAM");
            grvLoaiHang.DataSource = tb_LSP;
        }
        private void bingding()
        {
            txtMaLH.DataBindings.Clear();
            txtMaLH.DataBindings.Add("Text", grvLoaiHang.DataSource, "maLSP");
            txtMoTa.DataBindings.Clear();
            txtMoTa.DataBindings.Add("Text", grvLoaiHang.DataSource, "moTa");
            txtTenLH.DataBindings.Clear();
            txtTenLH.DataBindings.Add("Text", grvLoaiHang.DataSource, "tenL");
        }

        private void grvLoaiHang_Click(object sender, EventArgs e)
        {
            bingding();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtMaLH.Text.Length>0)
            {
                try
                {
                    string sql = "select *from LOAISANPHAM";
                    DataTable tb_LSP = db.getDatatable(sql);
                    DataRow newRow = tb_LSP.NewRow();
                    newRow["maLSP"] = txtMaLH.Text;
                    newRow["tenL"] = txtTenLH.Text;
                    newRow["moTa"] = txtMoTa.Text;
                    tb_LSP.Rows.Add(newRow);
                    int kq = db.updateDatabase(sql, tb_LSP);
                    loadGrvLSP();
                    if (kq > 0)
                        MessageBox.Show("thêm thành công");
                    else MessageBox.Show("Thêm thất bại");
                }
                catch(Exception ex) { MessageBox.Show("Thất bại"); }
            }    
            else
            {
                MessageBox.Show("vui lòng nhập thông tin muốn thêm");
                txtMaLH.Focus();
            }    
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select *from LOAISANPHAM";
                DataTable tb_LSP=db.getDatatable(sql);
                DataColumn[] key = new DataColumn[1];
                key[0] = tb_LSP.Columns[0];
                tb_LSP.PrimaryKey = key;

                DataRow row = tb_LSP.Rows.Find(txtMaLH.Text);
                if(row != null)
                {
                    row.Delete();
                }
                int kq = db.updateDatabase(sql, tb_LSP);
                loadGrvLSP();
                if (kq > 0)
                    MessageBox.Show("xoá thành công");
                else MessageBox.Show("xoá thất bại");
            }
            catch(Exception ex) { MessageBox.Show("Thất bại"); }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select *from LOAISANPHAM";
                DataTable tb_LSP = db.getDatatable(sql);
                DataColumn[] key = new DataColumn[1];
                key[0] = tb_LSP.Columns[0];
                tb_LSP.PrimaryKey = key;

                DataRow row = tb_LSP.Rows.Find(txtMaLH.Text);
                if (row != null)
                {
                    row["tenL"] = txtTenLH.Text;
                    row["moTa"]=txtMoTa.Text;
                }
                int kq = db.updateDatabase(sql, tb_LSP);
                loadGrvLSP();
                if (kq > 0)
                    MessageBox.Show("sửa thành công");
                else MessageBox.Show("sửa thất bại");
            }
            catch (Exception ex) { MessageBox.Show("Thất bại"); }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
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
