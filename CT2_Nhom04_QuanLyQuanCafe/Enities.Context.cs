﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QL_QUANCAPHEEntities1 : DbContext
    {
        public QL_QUANCAPHEEntities1()
            : base("name=QL_QUANCAPHEEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<admin> admins { get; set; }
        public DbSet<BANCF> BANCFs { get; set; }
        public DbSet<CHITIET_HOADON> CHITIET_HOADON { get; set; }
        public DbSet<HOADON> HOADONs { get; set; }
        public DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public DbSet<KHUVUC> KHUVUCs { get; set; }
        public DbSet<LOAISANPHAM> LOAISANPHAMs { get; set; }
        public DbSet<NHANVIEN> NHANVIENs { get; set; }
        public DbSet<SANPHAM> SANPHAMs { get; set; }
    }
}
