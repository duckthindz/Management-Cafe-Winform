using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace CT2_Nhom04_QuanLyQuanCafe
{
    public partial class frmQuanLyQuanCafe_ThanhToan : Form
    {
        DBConnect db=new DBConnect();
        bool run = false;
        private DataTable table=new DataTable();
        public delegate void truyenDataTT(string maHD, string maNV, string tongT, string maBan);
        public truyenDataTT senderTT;
        public delegate void truyenDataKH(string maKH, string diemTL, string tenKH, bool l);
        public truyenDataKH senderKH;
        QL_QUANCAPHEEntities1 rp=new QL_QUANCAPHEEntities1();
        public frmQuanLyQuanCafe_ThanhToan()
        {
            InitializeComponent();
            txtGopY.Focus();
            senderTT = new truyenDataTT(setDataTT);
            senderKH = new truyenDataKH(setDataKH);
        }
        public void setDataKH(string maKH, string diemTL, string tenKH, bool l)
        {
            lblMaKH.Text = maKH.Substring(0,5) +"- "+ tenKH + " - " + diemTL;
            run = l;
        }
        public void setDataTT(string maHD, string maNV, string tongT, string maBan)
        {
            lblMaHD.Text = maHD;
            lblMaNV.Text = maNV;
            lblTongTien.Text = tongT;
            lblMaBan.Text = maBan;
        }
        public void myTextBox(TextBox a)
        {
            a.BorderStyle = System.Windows.Forms.BorderStyle.None;
            a.AutoSize = false;
            a.Controls.Add(new Label()
            { Height=1,Dock=DockStyle.Bottom,BackColor=Color.Black});
        }
        public void loadTenCacLbl()
        {
            if (run==false)
            {
                string tenBan = (string)db.getScalar("select tenBan from BANCF where maBan='" + lblMaBan.Text + "'");
                lblMaBan.Text = lblMaBan.Text.Substring(0, 5) + "- " + tenBan;
                string tenNV = (string)db.getScalar("select HoTenNV from NHANVIEN where maNV='" + lblMaNV.Text + "'");
                lblMaNV.Text = lblMaNV.Text + " - " + tenNV;
            }
            else
                return;
        }
        public frmQuanLyQuanCafe_ThanhToan(DataTable dt) : this()
        {
            table = dt;
            grvOrder.DataSource= table;
        }
        private void frmQuanLyQuanCafe_ThanhToan_Load(object sender, EventArgs e)
        {
            myTextBox(txt_giamgia); myTextBox(txt_trudiem);myTextBox(txt_chiphikhac);
            grvOrder.Font = new Font("Microsoft Sans Serif", 9,FontStyle.Bold);
            loadTenCacLbl();
        }
        
        private void frmQuanLyQuanCafe_ThanhToan_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (run == false)
            {
                this.Hide();
                frmQuanLyQuanCafe_BanHang frmHH = new frmQuanLyQuanCafe_BanHang();
                frmHH.ShowDialog();
                this.Close();
            }
            else
            {
                //DialogResult r = MessageBox.Show("Thoát!", "Bạn muốn thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (DialogResult.No == r)
                //    e.Cancel = true;
                this.Close();
            }
        }

        private void btnLayTTkH_Click(object sender, EventArgs e)
        {
            this.Hide();
            run = true;
            frmQuanLyQuanCafe_QuanLyKhachHang frmKH = new frmQuanLyQuanCafe_QuanLyKhachHang(table);
            frmKH.isFrmLoad(run);
            frmKH.senderTT(lblMaHD.Text, lblMaNV.Text, lblTongTien.Text, lblMaBan.Text);
            frmKH.ShowDialog();
            this.Close();
        }
        private bool kiemTraMaHD(string ma )
        {
            int cnt = (int)db.getScalar("select count(*) from HOADON where maHD='"+ma+"'");
            if(cnt > 0)
            {
                return false;
            }
            return true;
        }
        private void btnInHD_Click(object sender, EventArgs e)
        {
            try
            {
                if (kiemTraMaHD(lblMaHD.Text.Substring(0, 4)))
                {
                    DateTime homnay = DateTime.Now;
                    string strDate = homnay.ToString("dd") + homnay.ToString("MM") + homnay.ToString("yyyy");
                    string strNgay = homnay.ToString("yyyy-MM-dd");
                    string maHD = lblMaHD.Text.Substring(0, 4).ToString();
                    string maNV = lblMaNV.Text.Substring(0, 4).ToString();
                    string maKH = lblMaKH.Text.Substring(0, 4);
                    string maBan = lblMaBan.Text.Substring(0, 4);
                    int diemTL = 0;
                    if (lblMaKH.Text != ".....")
                        diemTL = int.Parse(lblMaKH.Text.Substring(lblMaKH.Text.Length - 3));
                    else
                        diemTL = 0;
                    int giamG = int.Parse(txt_giamgia.Text);
                    int chiPK = int.Parse(txt_chiphikhac.Text);
                    int tongT = int.Parse(lblTongTien.Text.Substring(0, lblTongTien.Text.Length - 3));
                    string sql = "insert into HOADON values('" + maHD + "','" + maNV + "','" + maKH + "','" + maBan + "','" + strNgay + "','" + diemTL + "','" + giamG + "','" + chiPK + "','" + tongT + "')";
                    int kq = db.getNonQuery(sql);
                    if (kq > 0)
                        MessageBox.Show("Thêm hoá đơn thành công");
                    else
                        MessageBox.Show("Thất bại");
                }
                else
                {
                    MessageBox.Show("Bị trùng mã khoá");
                }
            }
            catch (Exception ex) { MessageBox.Show("Thêm HD thất bại"); }
            try
            {
                string maHD = lblMaHD.Text.Substring(0, 4).ToString();
                int kq = 0;
                foreach (DataGridViewRow row in grvOrder.Rows)
                {
                    kq = 0;
                    if (row.Cells[0].Value.ToString() != "")
                    {
                        string maSP = row.Cells[3].Value.ToString();
                        int sl = int.Parse(row.Cells[1].Value.ToString());
                        string gopY = txtGopY.Text;
                        string sql = "select *from CHITIET_HOADON insert into CHITIET_HOADON " +
                            "values('" + maHD + "','" + maSP + "','2',N'" + gopY + "')";
                        kq = db.getNonQuery(sql);
                        if (kq == 0)
                        {
                            break;
                        }
                    }
                }
                if (kq > 0)
                {
                    MessageBox.Show("Thêm chi tiết hoá đơn thành công");
                    reportHD(lblMaHD.Text);
                }
                else
                    MessageBox.Show("Thất bại CTHĐ");
            }
            catch (Exception ex) { MessageBox.Show("Thêm CTHD thất bại"); }
        }
        public  void reportHD(string m)
        {
            int thanhTien;
            var hd = rp.CHITIET_HOADON.Where(ct => ct.maHD == m)
                .Select(ct => new
                {
                    ct.HOADON.ngayLapHD,
                    ct.HOADON.maHD,
                    ct.HOADON.BANCF.tenBan,
                    ct.HOADON.NHANVIEN.HoTenNV,
                    ct.HOADON.KHACHHANG.HoTenKH,
                    ct.HOADON.diemTL,
                    ct.SANPHAM.tenSP,
                    ct.soLuong,
                    ct.SANPHAM.giaSP,
                    ct.HOADON.giamGia,
                    ct.HOADON.chiPhiKhac,
                    ct.HOADON.tongTien,
                    thanhTien = ct.soLuong * ct.SANPHAM.giaSP,
                }).ToList();
            rpHoaDon r = new rpHoaDon();
            r.SetDataSource(hd);
            // Hiển thị
            frmReport f = new frmReport();
            f.crystalReportViewer1.ReportSource = r;
            f.ShowDialog();
        }
        private void tinhTongT()
        {
            int giamG = int.Parse(txt_giamgia.Text);
            int chiPK = int.Parse(txt_chiphikhac.Text);
            int tongT = int.Parse(lblTongTien.Text.Substring(0, lblTongTien.Text.Length - 3));
            tongT = tongT - (giamG + chiPK);
            lblTongTien.Text = tongT.ToString() + "VNĐ";
        }

        private void txt_trudiem_Leave(object sender, EventArgs e)
        {
            if (lblMaKH.Text != "....." && txt_trudiem.Text!=null)
            {
                int diemTru = int.Parse(txt_trudiem.Text);
                int giamG = diemTru * 1000;
                txt_giamgia.Text = giamG.ToString();
                tinhTongT();
                int diemTL = int.Parse(lblMaKH.Text.Substring(lblMaKH.Text.Length - 3));
                int diemCL = diemTL - diemTru;
                lblMaKH.Text = lblMaKH.Text.Substring(0, lblMaKH.Text.Length - 3)+" " + diemCL.ToString();
            }
            else
                return;
        }
        private void txt_chiphikhac_Leave(object sender, EventArgs e)
        {
            tinhTongT();
        }

    }
}
