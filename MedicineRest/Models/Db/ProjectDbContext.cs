using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MedicineRest.Models.Db;

public partial class ProjectDbContext : DbContext
{
    public ProjectDbContext()
    {
    }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorydetail> Categorydetails { get; set; }

    public virtual DbSet<Logindetail> Logindetails { get; set; }

    public virtual DbSet<Medicinedetail> Medicinedetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ProjectDb;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorydetail>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("PK__category__23CDE5903CB6B523");

            entity.ToTable("categorydetails");

            entity.Property(e => e.Categoryid)
                .ValueGeneratedNever()
                .HasColumnName("categoryid");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("categoryname");
        });

        modelBuilder.Entity<Logindetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("logindetails");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Medicinedetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__medicine__3213E83F20F07C44");

            entity.ToTable("medicinedetails");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Expirydate)
                .HasColumnType("date")
                .HasColumnName("expirydate");
            entity.Property(e => e.Mediname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("mediname");
            entity.Property(e => e.Prize).HasColumnName("prize");
            entity.Property(e => e.Stocklvl).HasColumnName("stocklvl");

            entity.HasOne(d => d.Category).WithMany(p => p.Medicinedetails)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
