using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Bai2.Models
{
    public partial class Bai2Context : DbContext
    {
        public Bai2Context()
        {
        }

        public Bai2Context(DbContextOptions<Bai2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<NhomHang> NhomHangs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-KL77QCL\\SQLEXPRESS;Initial Catalog=Bai2;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<NhomHang>(entity =>
            {
                entity.HasKey(e => e.MaNhomHang);

                entity.ToTable("NhomHang");

                entity.Property(e => e.MaNhomHang)
                    .ValueGeneratedNever()
                    .HasColumnName("maNhomHang");

                entity.Property(e => e.TenNhomHang)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenNhomHang");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp)
                    .ValueGeneratedNever()
                    .HasColumnName("maSP");

                entity.Property(e => e.DonGia).HasColumnName("donGia");

                entity.Property(e => e.MaNhomHang).HasColumnName("maNhomHang");

                entity.Property(e => e.SoLuongBan).HasColumnName("soLuongBan");

                entity.Property(e => e.TenSanPham)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenSanPham");

                entity.HasOne(d => d.MaNhomHangNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaNhomHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SanPham_SanPham");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
