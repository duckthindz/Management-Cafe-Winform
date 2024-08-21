using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT2_Nhom04_QuanLyQuanCafe.Tess
{
    public class tb_BanCf
    {
        public string MaKV {  get; set; }
        public string TenBan {  get; set; }
        public tb_BanCf(string maKV, string tenBan)
        {
            this.MaKV = maKV;
            this.TenBan = tenBan;
        }
    }
}
