using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreShared.Models.StoreDb;

public partial class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Area { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Dipendente> Dipendente { get; set; }

    public virtual DbSet<Richiesta> Richiesta { get; set; }

    public virtual DbSet<Utente> Utente { get; set; }

    public virtual DbSet<Notifica> Notifica { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetToken { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dipendente>(entity =>
        {
            entity.HasOne(d => d.Area).WithMany(p => p.Dipendente).HasConstraintName("FK_Dipendente_Area");
        });

        modelBuilder.Entity<Richiesta>(entity =>
        {
            entity.HasOne(d => d.Area).WithMany(p => p.Richiesta).HasConstraintName("FK_Richiesta_Area");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Richiesta).HasConstraintName("FK_Richiesta_Cliente");

            entity.HasOne(d => d.Dipendente).WithMany(p => p.Richiesta).HasConstraintName("FK_Richiesta_Dipendente");
        });

        modelBuilder.Entity<Utente>(entity =>
        {
            entity.HasOne(d => d.CodiceClienteNavigation).WithMany(p => p.Utente).HasConstraintName("FK_Utente_Cliente");

            entity.HasOne(d => d.CodiceDipendenteNavigation).WithMany(p => p.Utente).HasConstraintName("FK_Utente_Dipendente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
