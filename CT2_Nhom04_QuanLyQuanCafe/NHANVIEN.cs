//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CT2_Nhom04_QuanLyQuanCafe
{
    using System;
    using System.Collections.Generic;
    
    public partial class NHANVIEN
    {
        public NHANVIEN()
        {
            this.HOADONs = new HashSet<HOADON>();
        }
    
        public string maNV { get; set; }
        public string HoTenNV { get; set; }
        public System.DateTime namSinhNV { get; set; }
        public string diaChiNV { get; set; }
        public string emailNV { get; set; }
        public string gioiTinhNV { get; set; }
        public string sdtNV { get; set; }
        public string chucVu { get; set; }
        public string matKhauNV { get; set; }
    
        public virtual ICollection<HOADON> HOADONs { get; set; }
    }
}
