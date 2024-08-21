using CT2_Nhom04_QuanLyQuanCafe.Tess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CT2_Nhom04_QuanLyQuanCafe
{
    public partial class frmQuanLyQuanCafe_BanHang : Form
    {
        DBConnect db=new DBConnect();
        DataTable dataTable = new DataTable();
        int tongT = 0;
        DataColumn[] key = new DataColumn[1];
        public delegate void truyenInfoQuyen(string maNV, string chucVu);
        public truyenInfoQuyen senderQ;

        public delegate void truyenAdmin(bool q);
        public truyenAdmin ad;
        bool quyen = false;
        public frmQuanLyQuanCafe_BanHang()
        {
            dataTable.Columns.Add("TH", typeof(string));
            dataTable.Columns.Add("SL", typeof(int));
            dataTable.Columns.Add("TT", typeof(int));
            dataTable.Columns.Add("SP", typeof(string));
            InitializeComponent();
            senderQ = new truyenInfoQuyen(setDataNV);
            ad = new truyenAdmin(admin);
            key[0] = dataTable.Columns["SP"];
            dataTable.PrimaryKey = key;
        }
        public void admin(bool q)
        {
            quyen = q;
        }
        public void setDataNV(string maNV, string chucVu)
        {
            lblMaNV.Text = maNV;
            lblQuyen.Text= chucVu;
            if(chucVu=="admin")
                quyen= true;
        }
        private void frmQuanLyQuanCafe_Load(object sender, EventArgs e)
        {
            DateTime now= DateTime.Now;
            lblNgayHD.Text = now.ToString();
            int maHD = (int)db.getScalar("select count(*) from HOADON");
            maHD++;
            lblMaHD.Text = "HD0" + maHD;
            loadListBan();
            myTextBox(txtDonGia);myTextBox(txtMaHang);myTextBox(txtSL);
            loadcmbLH();
            string item = cmbLoaiHang.GetItemText(cmbLoaiHang.SelectedValue);
            if (item == "ALL")
            {
                DataTable loadAll = db.getDatatable("select tenSP,giaSP,maLSP,maSP from SANPHAM");
                grvThucDon.DataSource = loadAll;
            }
            if(quyen==false)
            {
                btnQLNhanVien.Enabled = false;
                btnQLKhachHang.Enabled = false;
                btnLoaiHang.Enabled = false;
                btnHangHoa.Enabled = false;
                btnKhuVuc_Ban.Enabled = false;
                btnThongKe.Enabled= false;
            } 
               
        }
        public void loadcmbLH()
        {
            string sql = "select *from LOAISANPHAM";
            DataTable dtLH=new DataTable();
            dtLH=db.getDatatable(sql);
            DataRow newRow = dtLH.NewRow();
            newRow[0] = "ALL";
            newRow[1] = "Tất Cả";
            dtLH.Rows.InsertAt(newRow,0);
            cmbLoaiHang.DataSource = dtLH;
            cmbLoaiHang.DisplayMember = "tenL";
            cmbLoaiHang.ValueMember = "maLSP";

        }
        public void loadGrvThucDon(string ma)
        {
            DataTable data = db.getDatatable("select tenSP,giaSP,SANPHAM.maLSP,maSP from SANPHAM");
            DataView dataView = new DataView(data);
            dataView.RowFilter = "maLSP='" + ma + "'";
            grvThucDon.DataSource = dataView;
        }
        public void myTextBox(TextBox a)
        {
            a.BorderStyle=System.Windows.Forms.BorderStyle.None;
            a.AutoSize = false;
            a.Controls.Add(new Label()
            { Height=1,Dock=DockStyle.Bottom,BackColor=Color.Black});
        }
        private void loadListBan()
        {
            ImageList cafeImage = new ImageList();
            cafeImage.ImageSize = new Size(21,21);
            using (Icon myIcon = new Icon(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\lycafe.ico"))
            {
                cafeImage.Images.Add(myIcon);
            }
            lvBan.LargeImageList = cafeImage;
            
            SqlDataReader readerKV = db.getDataReader("select *from KHUVUC");
            List<tb_KhuVuc> lstKV = new List<tb_KhuVuc>();
            while(readerKV.Read()) 
            {
                var tb_KhuVuc = new tb_KhuVuc
                {
                    MaKV = readerKV["maKV"] as string,
                    TenKV = readerKV["tenKV"] as string
                };
                lstKV.Add(tb_KhuVuc);
            }
            db.close();
            foreach(var kv in lstKV)
            {
                ListViewGroup gr = new ListViewGroup(kv.TenKV);
                lvBan.Groups.Add(gr);

                SqlDataReader readerBan = db.getDataReader("select *from BANCF where maKV='" + kv.MaKV + "'");
                while(readerBan.Read())
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.ImageIndex = 0;
                    listViewItem.Text = (string)readerBan["tenBan"];
                    bool p = (bool)readerBan["thuocTinh"];
                    if(p==true)
                    {
                        listViewItem.BackColor = Color.Yellow;
                        
                    }    
                    listViewItem.Group = gr;
                    listViewItem.Name = (string)readerBan["maBan"];
                    lvBan.Items.Add(listViewItem);
                }    
               readerBan.Close();
            }
        }
        private void btnQLKhachHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_QuanLyKhachHang frmNew=new frmQuanLyQuanCafe_QuanLyKhachHang();
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
        private void cmbLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = cmbLoaiHang.GetItemText(cmbLoaiHang.SelectedValue);
            if (item == "ALL")
            {
                DataTable loadAll = db.getDatatable("select tenSP,giaSP,maLSP from SANPHAM");
                grvThucDon.DataSource = loadAll;
            }
            else
                loadGrvThucDon(cmbLoaiHang.SelectedValue.ToString());
        }

        private void btnChonMon_Click(object sender, EventArgs e)
        {
            if (txtSL.Text.Length <= 0)
            {
                MessageBox.Show("Bạn phải nhập số luong SP");
                txtSL.ResetText();
                txtSL.Focus();
            }
            else
            {
                DataGridViewRow row = grvThucDon.CurrentRow;
                string tenSP = row.Cells[0].Value.ToString();
                int giaSP = int.Parse(row.Cells[1].Value.ToString());
                int sl = int.Parse(txtSL.Text);
                string maSP = row.Cells[3].Value.ToString();
                int tTien = sl * giaSP;
                dataTable.Rows.Add(tenSP, sl, tTien, maSP);

                tongT += tTien;
                txtTongTien.Text = tongT.ToString() + "VNĐ";
                grvOrder.DataSource = dataTable;
            }    

        }
        private void grvThucDon_Click(object sender, EventArgs e)
        {
            txtSL.ResetText();
            txtSL.Focus();
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length > 0 && !char.IsDigit(ctr.Text[ctr.Text.Length - 1]))
            {
                MessageBox.Show("Bạn phải nhập số");
                txtSL.ResetText();
                txtSL.Focus();
            }
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {

            if(lvBan.FocusedItem != null) 
            {
                ListViewItem item = lvBan.SelectedItems[0];
                if(item.BackColor==Color.Yellow)
                {
                    MessageBox.Show("Bàn này đang ở trạng thái không trống");
                    return;
                }    
                string maBan = item.Name.ToString();
                string sql = "update BANCF set thuocTinh=1 where maBan='" + maBan + "'";
                int kq = db.getNonQuery(sql);
                this.Hide();
                frmQuanLyQuanCafe_ThanhToan frmTT = new frmQuanLyQuanCafe_ThanhToan(dataTable);
                frmTT.senderTT(lblMaHD.Text, lblMaNV.Text, txtTongTien.Text, maBan);
                frmTT.ShowDialog();
                this.Close();
            }  
            else
            {
                MessageBox.Show("Vui lòng chọn bàn muốn thành toán");
            }    
        }
        private void binding()
        {
            txtMaHang.DataBindings.Clear();
            txtMaHang.DataBindings.Add("Text", grvOrder.DataSource, "SP");
            txtDonGia.DataBindings.Clear();
            txtDonGia.DataBindings.Add("Text", grvOrder.DataSource, "TT");
        }
        private void grvOrder_Click(object sender, EventArgs e)
        {
            binding();
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            DataRow row = dataTable.Rows.Find(txtMaHang.Text);
            if (row != null)
                row.Delete();
            grvOrder.DataSource = dataTable;
        }
        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            if (lvBan.FocusedItem != null)
            {
                try
                {
                    ListViewItem item = lvBan.SelectedItems[0];
                    string maBan = item.Name.ToString();
                    string sql = "update BANCF set thuocTinh=0 where maBan='" + maBan + "'";
                    int kq = db.getNonQuery(sql);
                    if (kq > 0)
                        MessageBox.Show("Đổi trạng thái thành công!");
                    else
                        MessageBox.Show("Thất bại");
                    item.BackColor = Color.Silver;
                }
                catch(Exception ex) { MessageBox.Show("Thất bại thạm tệ"); }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bàn muốn đổi trạng thái");
            }
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmQuanLyQuanCafe_BanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Thoát!", "Bạn muốn thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.No == r)
                e.Cancel = true;
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmQuanLyQuanCafe_QuanLyNhanVien frmNew = new frmQuanLyQuanCafe_QuanLyNhanVien();
            frmNew.ShowDialog();
            this.Close();
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            frmResetPassword frmRS=new frmResetPassword();
            frmRS.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogIn frmLogin=new frmLogIn();
            frmLogin.ShowDialog();
            this.Close();
        }
    }
}
