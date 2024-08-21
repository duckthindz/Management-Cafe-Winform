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

namespace CT2_Nhom04_QuanLyQuanCafe
{
    public partial class frmLogIn : Form
    {
        DBConnect db = new DBConnect();
        QL_QUANCAPHEEntities1 rp= new QL_QUANCAPHEEntities1();
        public frmLogIn()
        {
            InitializeComponent();    
        }
        public void loadImage()
        {
            ImageList image=new ImageList();
            image.ImageSize = new Size(30, 30);
            image.Images.Add(new Bitmap(@"D:\Đức Thịnh\C#\CT2_Nhom04_QuanLyQuanCafe\Icons\BackIcon.png"));
            btn_sigup.Image = image.Images[0];
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmResetPassword frm = new frmResetPassword();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txt_user.Text;
            string password = txt_password.Text;

            try
            {
                string sql = "select count(*) from NHANVIEN where maNV='" + username + "' and matKhauNV ='" + password + "'";
                int kq = (int)db.getScalar(sql);
                if (kq > 0)
                {
                    string tr=(string)db.getScalar("select chucVu from NHANVIEN where maNV='"+username+"'");
                    this.Hide();
                    frmQuanLyQuanCafe_BanHang frm = new frmQuanLyQuanCafe_BanHang();
                    frm.senderQ(username, tr);
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khẩu hoặc mật khẩu !!!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                }
            }
            catch(Exception ex) { MessageBox.Show("Thất bại"); }
        }

        private void btn_sigup_Click(object sender, EventArgs e)
        {
            txt_password.ResetText();
            txt_user.ResetText();
            txt_user.Focus();
        }

        private void chk_remeber_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_remeber.Checked == true)
            {
                txt_password.UseSystemPasswordChar = true;
            }
            else
            {
                txt_password.UseSystemPasswordChar = false;
            }

        }

        private void txt_password_Click(object sender, EventArgs e)
        {
            //txt_password.ResetText();
            //txt_password.Focus();
        }

        private void txt_user_Click(object sender, EventArgs e)
        {
            //txt_user.ResetText();
            //txt_user.Focus();
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
            loadImage();
        }

    }
}



