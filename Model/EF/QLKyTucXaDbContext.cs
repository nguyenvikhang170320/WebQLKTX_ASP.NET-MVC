using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class QLKyTucXaDbContext : DbContext
    {
        public QLKyTucXaDbContext()
            : base("name=QLKyTucXaDbContext")
        {
        }

        public virtual DbSet<CANBO> CANBOes { get; set; }
        public virtual DbSet<CONGTODIEN> CONGTODIENs { get; set; }
        public virtual DbSet<CONGTONUOC> CONGTONUOCs { get; set; }
        public virtual DbSet<DAYPHONG> DAYPHONGs { get; set; }
        public virtual DbSet<DONGIA> DONGIAs { get; set; }
        public virtual DbSet<HOADON_DIENNUOC> HOADON_DIENNUOC { get; set; }
        public virtual DbSet<HOADON_PHONG> HOADON_PHONG { get; set; }
        public virtual DbSet<HOTRO> HOTROes { get; set; }
        public virtual DbSet<LICH_SU> LICH_SU { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<PHONG> PHONGs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CANBO>()
                .Property(e => e.MACB)
                .IsUnicode(false);

            modelBuilder.Entity<CANBO>()
                .Property(e => e.TAIKHOAN)
                .IsUnicode(false);

            modelBuilder.Entity<CANBO>()
                .Property(e => e.MATKHAU)
                .IsUnicode(false);

            modelBuilder.Entity<CANBO>()
                .Property(e => e.CMND_CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<CANBO>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<CANBO>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<CANBO>()
                .HasMany(e => e.HOADON_DIENNUOC)
                .WithRequired(e => e.CANBO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DAYPHONG>()
                .Property(e => e.MADAYPHONG)
                .IsUnicode(false);

            modelBuilder.Entity<DAYPHONG>()
                .HasMany(e => e.PHONGs)
                .WithRequired(e => e.DAYPHONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DONGIA>()
                .Property(e => e.MADONGIA)
                .IsUnicode(false);

            modelBuilder.Entity<DONGIA>()
                .HasMany(e => e.HOADON_DIENNUOC)
                .WithRequired(e => e.DONGIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.MANV)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.CMND_CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .HasMany(e => e.LICH_SU)
                .WithRequired(e => e.NHANVIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHONG>()
                .Property(e => e.MAPHONG)
                .IsUnicode(false);

            modelBuilder.Entity<PHONG>()
                .Property(e => e.TAIKHOAN)
                .IsUnicode(false);

            modelBuilder.Entity<PHONG>()
                .Property(e => e.MATKHAU)
                .IsUnicode(false);

            modelBuilder.Entity<PHONG>()
                .HasMany(e => e.CONGTODIENs)
                .WithRequired(e => e.PHONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHONG>()
                .HasMany(e => e.CONGTONUOCs)
                .WithRequired(e => e.PHONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHONG>()
                .HasMany(e => e.HOADON_DIENNUOC)
                .WithRequired(e => e.PHONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHONG>()
                .HasMany(e => e.HOADON_PHONG)
                .WithRequired(e => e.PHONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHONG>()
                .HasMany(e => e.HOTROes)
                .WithRequired(e => e.PHONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHONG>()
                .HasMany(e => e.LICH_SU)
                .WithRequired(e => e.PHONG)
                .WillCascadeOnDelete(false);
        }
    }
}
