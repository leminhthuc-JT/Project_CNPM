using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace G6_Website_BQA.Models
{
    public class DBContext: DbContext
    {
        public DBContext() : base("MyConnectionString")
        {
        }
        public virtual DbSet<LOAISP> LOAISPs { get; set; }
        public virtual DbSet<GIAMGIA> GIAMGIAs { get; set; }
        public virtual DbSet<SUKIEN> SUKIENs { get; set; }
        public virtual DbSet<HOADON> HOADONs { get; set; }
        public virtual DbSet<CHITIETHD> CHITIETHDs { get; set; }
        public virtual DbSet<ANHSUKIEN> ANHSUKIENs { get; set; }
        
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        //public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        //public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        //public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        //public virtual DbSet<QUYEN> QUYENs { get; set; }
        public virtual DbSet<ANHSANPHAM> ANHSANPHAMs { get; set; }
        public virtual DbSet<CHITIETSP> CHITIETSPs { get; set; }
        public virtual DbSet<DANHMUC> DANHMUCs { get; set; }
        public virtual DbSet<NHACUNGCAP> NHACUNGCAPs { get; set; }
        public virtual DbSet<THUONGHIEU> THUONGHIEUs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tắt cascade delete cho GIAMGIA → LOAISP
            modelBuilder.Entity<GIAMGIA>()
                .HasRequired(g => g.LOAISP)
                .WithMany(l => l.GIAMGIAs)
                .HasForeignKey(g => g.MALOAISP)
                .WillCascadeOnDelete(false);

            // SANPHAM → LOAISP: có thể để cascade delete
            modelBuilder.Entity<SANPHAM>()
                .HasRequired(s => s.LOAISP)
                .WithMany(l => l.SANPHAMs)
                .HasForeignKey(s => s.MALOAISP)
                .WillCascadeOnDelete(true);
        }
    }
}