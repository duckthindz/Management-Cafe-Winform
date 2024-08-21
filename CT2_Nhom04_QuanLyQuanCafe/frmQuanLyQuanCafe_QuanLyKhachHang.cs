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
    public partial class frmQuanLyQuanCafe_QuanLyKhachHang : Form
    {
        DBConnect db = new DBConnect();
        public delegate void kTrafrmTTload(bool run);
        public kTrafrmTTload isFrmLoad;
        public delegate void truyenDataTT(string maHD, string maNV, string tongT, string maBan);
        public truyenDataTT senderTT;
        bool co = false;
        string maHD,maNV, tongT,maBan;
        DataTable dt = new DataTable();
        public void getRunFrmTT(bool r)
        {
            co = r;
        }
        public void truyenTiep(string mahd, string manv, string tongt, string maban)
        {
            maHD = mahd;
            maNV = manv;
            string q = (string)db.getScalar("select * from NHANVIEN where maNV='" + manv + "'");
            lblQuyen.Text = q;
            tongT=tongt;
            maBan = maban;
        }
        public frmQuanLyQuanCafe_QuanLyKhachHang(DataTable table) : this()
        {
            dt = table;
        }
        public frmQuanLyQuanCafe_QuanLyKhachHang()
        {
            isFrmLoad = new kTrafrmTTload(getRunFrmTT);
            senderTT = new truyenDataTT(truyenTiep);
            dt.Columns.Add("TH", typeof(string));
            dt.Columns.Add("SL", typeof(int));
            dt.Columns.Add("TT", typeof(int));
            dt.Columns.Add("SP", typeof(string));
            InitializeComponent();
        }
        private void frmQuanLyQuanCafe_QuanLyKhachHang_Load(object sender, EventArgs e)
        {
            loadcmbLoaiKH();
            loadImagevsGrv();
            loadGrvKH();
            MyTextBox(txtDiaChi); MyTextBox(txtDiemTL); MyTextBox(txtFind); MyTextBox(txtMaKH);
            MyTextBox(txtSDT); MyTextBox(txtTenKH);
            if(co==true)
            {
                btnQLNhanVien.Enabled = false;
                btnBanHang.Enabled = false;
                btnLoaiHang.Enabled = false;
                btnHangHoa.Enabled = false;
                btnKhuVuc_Ban.Enabled = false;
                btnThongKe.Enabled = false;
            }    
        }
        public void loadcmbLoaiKH()
        {
            DataTable tb_LKH=new DataTable();
            tb_LKH.Columns.Add("name",typeof (string));
            tb_LKH.Columns.Add("info", typeof(string));
            DataRow Row1=tb_LKH.NewRow();
            Row1[0] = "ALL";
            Row1[1] = "Tất Cả";
            tb_LKH.Rows.InsertAt(Row1, 0);
            DataRow Row2 = tb_LKH.NewRow();
            Row2[0] = "KV";
            Row2[1] = "Khách Vip";
            tb_LKH.Rows.InsertAt(Row2, 1);
            DataRow Row3 = tb_LKH.NewRow();
            Row3[0] = "KT";
            Row3[1] = "Khách Thường";
            tb_LKH.Rows.InsertAt(Row3, 2);
            cmbLoaiKH.DataSource= tb_LKH;
            cmbLoaiKH.DisplayMember = "info";
            cmbLoaiKH.ValueMember = "name";
        }
        public void loadGrvKH()
        {
            if(cmbLoaiKH.SelectedValue.ToString()=="ALL")
            {
                string sql = "select maKH,HoTenKH,sdtKH,diaChiKH,diemTL from KHACHHANG";
                DataTable tb_KH = new DataTable();
                tb_KH = db.getDatatable(sql);
                grvKhachHang.DataSource = tb_KH;
            }    
            else if(cmbLoaiKH.SelectedValue.ToString()=="KV")
            {
                string sql = "select maKH,HoTenKH,sdtKH,diaChiKH,diemTL from KHACHHANG where LoaiKH=1";
                DataTable tb_KH = new DataTable();
                tb_KH = db.getDatatable(sql);
                grvKhachHang.DataSource = tb_KH;
            }    
            else
            {
                string sql = "select maKH,HoTenKH,sdtKH,diaChiKH,diemTL from KHACHHANG where LoaiKH=0";
                DataTable tb_KH = new DataTable();
                tb_KH = db.getDatatable(sql);
                grvKhachHang.DataSource = tb_KH;
            }    
        }
        
        public void loadImagevsGrv()
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(249, 106);

            imageList.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\CF.jpg"));
            button2.Image = imageList.Images[0];

            ImageList imge = new ImageList();
            imge.ImageSize = new Size(30, 35);

            imge.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\Add-icon.png"));
            imge.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\Delete-icon.png"));
            imge.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\save-icon.png"));
            btnAdd.Image = imge.Images[0];
            btnDelete.Image = imge.Images[1];
            btnSave.Image = imge.Images[2];
            //

            imageList.ImageSize = new Size(30, 20);
            imageList.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\find.png"));
            btnFind.Image = imageList.Images[0];
            grvLichSuGiaoDich.Font = new Font("Microsoft Sans Serif", 9);
            grvKhachHang.Font = new Font("Microsoft Sans Serif", 9);
        }
        public void MyTextBox(TextBox a)
        {
            a.BorderStyle = System.Windows.Forms.BorderStyle.None;
            a.AutoSize = false;
            a.Controls.Add(new Label()
            { Height=1,Dock=DockStyle.Bottom,BackColor=Color.Black});
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
        }

        private void btnLoaiHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_LoaiHang frmLH = new frmQuanLyQuanCafe_LoaiHang();
            frmLH.ShowDialog();
            this.Close();
        }

        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_HangHoa frmHH = new frmQuanLyQuanCafe_HangHoa();
            frmHH.ShowDialog();
            this.Close();
        }

        private void btnKhuVuc_Ban_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_KhuVucBan frmHH = new frmQuanLyQuanCafe_KhuVucBan();
            frmHH.ShowDialog();
            this.Close();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_ThongKe frmHH = new frmQuanLyQuanCafe_ThongKe();
            frmHH.ShowDialog();
            this.Close();
        }

        private void txtFind_Click(object sender, EventArgs e)
        {
            txtFind.ResetText();
            txtFind.Focus();
        }

        private void cmbLoaiKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrvKH();
        }
        public void binding()
        {
            txtMaKH.DataBindings.Clear();
            txtMaKH.DataBindings.Add("Text", grvKhachHang.DataSource, "maKH");
            txtTenKH.DataBindings.Clear();
            txtTenKH.DataBindings.Add("Text", grvKhachHang.DataSource, "HoTenKH");
            txtSDT.DataBindings.Clear();
            txtSDT.DataBindings.Add("Text", grvKhachHang.DataSource, "sdtKH");
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", grvKhachHang.DataSource, "diaChiKH");
            txtDiemTL.DataBindings.Clear();
            txtDiemTL.DataBindings.Add("Text", grvKhachHang.DataSource, "diemTL");

        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            string find = txtFind.Text;
            string sql = "select *from KHACHHANG where ((HoTenKH LIKE N'%"+find+"%') or sdtKH='"+find+"') ";
            DataTable tb_KH = new DataTable();
            tb_KH = db.getDatatable(sql);
            grvKhachHang.DataSource = tb_KH;
        }
        private void frmQuanLyQuanCafe_QuanLyKhachHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (co == true)
            {
                this.Hide();
                frmQuanLyQuanCafe_ThanhToan frmTT = new frmQuanLyQuanCafe_ThanhToan(dt);
                frmTT.senderKH(txtMaKH.Text, txtDiemTL.Text, txtTenKH.Text, co);
                frmTT.senderTT(maHD, maNV, tongT, maBan);
                frmTT.ShowDialog();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }
        private bool kiemTraMaKH(string ma)
        {
            string sql = "select count(*) from KHACHHANG where maKH='" + ma + "'";
            int kq = (int)db.getScalar(sql);
            if (kq > 0)
                return false;
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text.Length > 0)
            {
                if (kiemTraMaKH(txtMaKH.Text) == true)
                {
                    try
                    {
                        string sql = "select *from KHACHHANG";
                        DataTable tb_KH = new DataTable();
                        tb_KH = db.getDatatable(sql);

                        DataRow newRow = tb_KH.NewRow();
                        newRow["maKH"] = txtMaKH.Text;
                        newRow["HoTenKH"] = txtTenKH.Text;
                        newRow["sdtKH"] = txtSDT.Text;
                        newRow["diaChiKH"] = txtDiaChi.Text;
                        newRow["diemTL"] = txtDiemTL.Text;
                        int diemTL = int.Parse(txtDiemTL.Text);
                        if (diemTL >= 100)
                            newRow["LoaiKH"] = 1;
                        else
                            newRow["LoaiKH"] = 0;
                        tb_KH.Rows.Add(newRow);
                        int kq = db.updateDatabase(sql, tb_KH);
                        loadGrvKH();
                        if (kq > 0)
                            MessageBox.Show("thêm thành công");
                        else
                            MessageBox.Show("Thêm thất bại");
                    }
                    catch (Exception ex) { MessageBox.Show("thất bại"); }
                }
                else
                    MessageBox.Show("Trùng Khoá");
            }
            else
            {
                txtMaKH.Focus();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select *from KHACHHANG";
                DataTable tb_KH = new DataTable();
                tb_KH = db.getDatatable(sql);
                DataColumn[] key = new DataColumn[1];
                key[0] = tb_KH.Columns["maKH"];
                tb_KH.PrimaryKey= key;

                DataRow row = tb_KH.Rows.Find(txtMaKH.Text);
                if (row != null)
                    row.Delete();
                int kq = db.updateDatabase(sql, tb_KH);
                loadGrvKH();
                if (kq > 0)
                    MessageBox.Show("xoá thành công");
                else
                    MessageBox.Show("xoá thất bại");
            }
            catch (Exception ex) { MessageBox.Show("thất bại"); }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select *from KHACHHANG";
                DataTable tb_KH = new DataTable();
                tb_KH = db.getDatatable(sql);
                DataColumn[] key = new DataColumn[1];
                key[0] = tb_KH.Columns["maKH"];
                tb_KH.PrimaryKey = key;

                DataRow row = tb_KH.Rows.Find(txtMaKH.Text);
                if (row != null)
                {
                    row["maKH"] = txtMaKH.Text;
                    row["HoTenKH"] = txtTenKH.Text;
                    row["sdtKH"] = txtSDT.Text;
                    row["diaChiKH"] = txtDiaChi.Text;
                    row["diemTL"] = txtDiemTL.Text;
                    int diemTL = int.Parse(txtDiemTL.Text);
                    if (diemTL >= 100)
                        row["LoaiKH"] = 1;
                    else
                        row["LoaiKH"] = 0;
                }    
                int kq = db.updateDatabase(sql, tb_KH);
                loadGrvKH();
                if (kq > 0)
                    MessageBox.Show("sửa thành công");
                else
                    MessageBox.Show("sửa thất bại");
            }
            catch (Exception ex) { MessageBox.Show("thất bại"); }
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

        public void loadGrvLSDD(string ma)
        {
            string sql = "select maHD,maNV,ngayLapHD,tongTien,HOADON.maKH from HOADON";
            DataTable tb_LSDD = new DataTable();
            tb_LSDD = db.getDatatable(sql);
            DataView dataV = new DataView(tb_LSDD);
            dataV.RowFilter = "maKH='" + ma + "'";
            grvLichSuGiaoDich.DataSource = dataV;
        }
        private void grvKhachHang_Click(object sender, EventArgs e)
        {
            binding();
            DataGridViewRow row = grvKhachHang.CurrentRow;
            string maKH = row.Cells[0].Value.ToString();
            loadGrvLSDD(maKH);
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
