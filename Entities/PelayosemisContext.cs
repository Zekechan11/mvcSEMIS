using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace semissssloan.Entities;

public partial class PelayosemisContext : DbContext
{
    public PelayosemisContext()
    {
    }

    public PelayosemisContext(DbContextOptions<PelayosemisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientInfo> ClientInfos { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Transac> Transacs { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LawasUtangananT;TrustServerCertificate=true;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClientIn__3214EC077B97F171");

            entity.ToTable("ClientInfo");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.CivilStatus)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NameOfFather)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NameOfMother)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Occupation)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Religion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Loan__3214EC0728D435B0");

            entity.ToTable("Loan");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.Collectable).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.Collected).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.DateCreated).HasColumnType("date");
            entity.Property(e => e.Deduction).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.Interest).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InterestAmount).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.Receivable).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.TotalPayable).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A387B227D71");

            entity.ToTable("Payment");

            entity.Property(e => e.Collectable).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.Date).HasColumnType("date");
        });

        modelBuilder.Entity<Transac>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transac__3214EC07CE7B7B6F");

            entity.ToTable("Transac");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Date).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserType__3214EC07ED16935B");

            entity.ToTable("UserType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
