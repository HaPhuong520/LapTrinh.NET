using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Bai1.Models
{
    public partial class Bai1Context : DbContext
    {
        public Bai1Context()
        {
        }

        public Bai1Context(DbContextOptions<Bai1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<LopHoc> LopHocs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-KL77QCL\\SQLEXPRESS;Initial Catalog=Bai1;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<LopHoc>(entity =>
            {
                entity.HasKey(e => e.MaLop);

                entity.ToTable("LopHoc");

                entity.Property(e => e.MaLop)
                    .ValueGeneratedNever()
                    .HasColumnName("maLop");

                entity.Property(e => e.GiangVien)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("giangVien");

                entity.Property(e => e.TenLop)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenLop");
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.MaSv);

                entity.ToTable("SinhVien");

                entity.Property(e => e.MaSv)
                    .ValueGeneratedNever()
                    .HasColumnName("maSV");

                entity.Property(e => e.Anh)
                    .HasColumnType("text")
                    .HasColumnName("anh");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("diaChi");

                entity.Property(e => e.Diem).HasColumnName("diem");

                entity.Property(e => e.HoTen)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("hoTen");

                entity.Property(e => e.MaLop).HasColumnName("maLop");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.SinhViens)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SinhVien_SinhVien");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
