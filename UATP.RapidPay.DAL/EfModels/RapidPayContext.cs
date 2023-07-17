using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UATP.RapidPay.DAL.EfModels;

public partial class RapidPayContext : DbContext
{
    public RapidPayContext()
    {
    }

    public RapidPayContext(DbContextOptions<RapidPayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__Cards__55FECD8EB3A4963D");

            entity.Property(e => e.CardId).HasColumnName("CardID");
            entity.Property(e => e.CardNumber).HasMaxLength(15);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Cards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cards_UserID");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__tmp_ms_x__9B556A5824AC59B1");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CardId).HasColumnName("CardID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Fee).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Card).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_CardID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC69571BF7");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E445F9BD7D").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
