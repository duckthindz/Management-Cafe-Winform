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
    
    public partial class BANCF
    {
        public BANCF()
        {
            this.HOADONs = new HashSet<HOADON>();
        }
    
        public string maBan { get; set; }
        public string maKV { get; set; }
        public string tenBan { get; set; }
        public bool thuocTinh { get; set; }
    
        public virtual KHUVUC KHUVUC { get; set; }
        public virtual ICollection<HOADON> HOADONs { get; set; }
    }
}
