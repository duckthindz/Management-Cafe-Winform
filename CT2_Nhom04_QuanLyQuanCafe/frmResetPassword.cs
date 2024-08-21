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
    public partial class frmResetPassword : Form
    {
        DBConnect db = new DBConnect();
        public frmResetPassword()
        {
            InitializeComponent();
        }

        private void btn_finish_Click(object sender, EventArgs e)
        {
            string user = txt_user.Text;
            string OldPass = txt_Opassword.Text;
            string NewPass = txt_Npassword.Text;
            string Confirm = txt_Confirm.Text;


            if (Confirm != NewPass) { MessageBox.Show("Mật khẩu mới không khớp!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            else
            {
                try
                {
                    string sql = "select count(*) from NHANVIEN where maNV='" + user + "' and matKhauNV ='" + OldPass + "'";
                    int kq = (int)db.getScalar(sql);
                    if (kq > 0)
                    {
                        db.open();
                        string str = "update NHANVIEN set matKhauNV ='" + NewPass + "' where maNV='" + user + "'";
                        SqlCommand cmd = new SqlCommand(str, db.Connect);
                        int q = cmd.ExecuteNonQuery();
                        db.close();
                        if (q > 0)
                        {
                            MessageBox.Show("Đổi mật khẩu thành công ", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Đổi mật khẩu không thành công !!!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        MessageBox.Show("Sai tài khẩu hoặc mật khẩu !!!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Thất bại"); }

                
            }    

            
            
        }
        private void txt_Opassword_Click(object sender, EventArgs e)
        {
            txt_Opassword.ResetText();
            txt_Opassword.Focus();
        }

        private void txt_Npassword_Click(object sender, EventArgs e)
        {
            txt_Npassword.ResetText();
            txt_Npassword.Focus();
        }

        private void txt_Confirm_Click(object sender, EventArgs e)
        {
            txt_Confirm.ResetText();
            txt_Confirm.Focus();
        }

    }
}
