using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CT2_Nhom04_QuanLyQuanCafe
{
    public partial class frmQuanLyQuanCafe_ThongKe : Form
    {
        DBConnect db=new DBConnect();
        int tk=0;
        public frmQuanLyQuanCafe_ThongKe()
        {
            InitializeComponent();
        }
        private void frmQuanLyQuanCafe_ThongKe_Load(object sender, EventArgs e)
        {
            grvThongKeDT.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
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
            this.Hide();
            frmQuanLyQuanCafe_KhuVucBan frmNew = new frmQuanLyQuanCafe_KhuVucBan();
            frmNew.ShowDialog();
            this.Close();
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXTKDoanhThu_Click(object sender, EventArgs e)
        {
            string ngayBD = dtpkNgayBD.Value.ToString();
            string ngayKT = dtpkNgayKT.Value.ToString();
            string sql = "set DATEFORMAT DMY " +
                "select ngayLapHD, CTHD.maHD,maNV,soLuong,tenSP,CTHD.maSP, tongTien,giamGia from CHITIET_HOADON as CTHD,HOADON as HD,SANPHAM as SP " +
                "where CTHD.maHD=HD.maHD and CTHD.maSP=SP.maSP and '" + ngayBD + "'<= ngayLapHD and ngayLapHD <= '" + ngayKT + "'";
            DataTable tb_TK = db.getDatatable(sql);
            grvThongKeDT.DataSource= tb_TK;
            tk = 1;
        }

        private void btnXTKSPBanNhieu_Click(object sender, EventArgs e)
        {
            string ngayBD = dtpkNgayBD.Value.ToString();
            string ngayKT = dtpkNgayKT.Value.ToString();
            string sql = "set DATEFORMAT DMY select CHITIET_HOADON.maSP, tenSP,SUM(soLuong) as SL from CHITIET_HOADON,SANPHAM,HOADON\r\nwhere CHITIET_HOADON.maSP=SANPHAM.maSP and CHITIET_HOADON.maHD=HOADON.maHD and '" + ngayBD+"'<= ngayLapHD and ngayLapHD <= '"+ngayKT+"'\r\ngroup by CHITIET_HOADON.maSP,tenSP\r\nORDER BY SUM(soLuong) DESC";
            DataTable tb_TKSP = db.getDatatable(sql);
            grvThongKeDT.DataSource = tb_TKSP;
            tk = 2;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogIn frmLogin= new frmLogIn();
            frmLogin.ShowDialog();
            this.Close();
        }

        private void btnInThongKE_Click(object sender, EventArgs e)
        {

            string ngayBD = dtpkNgayBD.Value.ToString();
            string ngayKT = dtpkNgayKT.Value.ToString();
            if (tk==2)
            {
                SqlConnection conn = new SqlConnection("Data Source = ADMIN-PC; Initial Catalog = QL_QUANCAPHE; User ID=sa;Password=123");
                String sql = "set DATEFORMAT DMY select tenSP,SUM(soLuong) as soLuong from CHITIET_HOADON,SANPHAM,HOADON\r\nwhere CHITIET_HOADON.maSP=SANPHAM.maSP and CHITIET_HOADON.maHD=HOADON.maHD and '" + ngayBD + "'<= ngayLapHD and ngayLapHD <= '" + ngayKT + "'\r\ngroup by CHITIET_HOADON.maSP,tenSP\r\nORDER BY SUM(soLuong) DESC";
                dsBieuDo bd = new dsBieuDo();
                SqlDataAdapter adp = new SqlDataAdapter(sql,conn);
                adp.Fill(bd, "TKSP");
                rptBieuDoSP r = new rptBieuDoSP();
                r.SetDataSource(bd);
                // Hiển thị
                frmReport f = new frmReport();
                f.crystalReportViewer1.ReportSource = r;
                f.ShowDialog();
            }    
            else if(tk==1)
            {
                SqlConnection conn = new SqlConnection("Data Source = ADMIN-PC; Initial Catalog = QL_QUANCAPHE; User ID=sa;Password=123");
                string sql = "set DATEFORMAT DMY select ngayLapHD, CTHD.maHD,maNV,soLuong,tenSP,CTHD.maSP, tongTien,giamGia from CHITIET_HOADON as CTHD,HOADON as HD,SANPHAM as SP\r\nwhere CTHD.maHD=HD.maHD and CTHD.maSP=SP.maSP and '" + ngayBD+"'<= ngayLapHD and ngayLapHD <= '"+ngayKT+"'";
                dsDoanhThu dt=new dsDoanhThu();
                SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
                adp.Fill(dt, "TKDT");
                rptDoanhThu r = new rptDoanhThu();
                r.SetDataSource(dt);
                //Hiển thị report
                frmReport f = new frmReport();
                f.crystalReportViewer1.ReportSource = r;
                f.ShowDialog();
            }    
            else
            {
                MessageBox.Show("Vui lòng chọn trạng thái muốn in");
            }    
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
