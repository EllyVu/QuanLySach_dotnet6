using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLySach.Models;

public partial class QlsContext : DbContext
{
    public QlsContext()
    {
    }

    public QlsContext(DbContextOptions<QlsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LoaiSach> LoaiSaches { get; set; }

    public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoaiSach>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("LoaiSach");

            entity.Property(e => e.TenLoai).HasMaxLength(30);
        });

        modelBuilder.Entity<NhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.MaXb).HasName("PK__NhaXuatB__272520CA2C1F079C");

            entity.ToTable("NhaXuatBan");

            entity.Property(e => e.DiaChi).HasMaxLength(30);
            entity.Property(e => e.GhiChu).HasMaxLength(30);
            entity.Property(e => e.Tenxb).HasMaxLength(30);
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Masach).HasName("PK__Sach__9F923C88F7AD74A8");

            entity.ToTable("Sach");

            entity.Property(e => e.Tacgia).HasMaxLength(30);
            entity.Property(e => e.Tensach).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
