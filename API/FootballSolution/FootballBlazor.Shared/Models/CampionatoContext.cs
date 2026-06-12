using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FootballBlazor.Shared.Models;

public partial class CampionatoContext : DbContext
{
    public CampionatoContext()
    {
    }

    public CampionatoContext(DbContextOptions<CampionatoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Giocatori> Giocatori { get; set; }

    public virtual DbSet<Squadre> Squadre { get; set; }

    public virtual DbSet<VistaSquadre> VistaSquadre { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MTSWEBTEST\\SQLTEST;Database=Campionato;User Id=sa;Password=Mts.2010;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Giocatori>(entity =>
        {
            entity.HasKey(e => e.Idgiocatore).HasName("PK__Giocator__A6ABE86BACEFD322");

            entity.Property(e => e.Idgiocatore).HasColumnName("IDGiocatore");
            entity.Property(e => e.Cognome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idsquadra).HasColumnName("IDSquadra");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ruolo)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdsquadraNavigation).WithMany(p => p.Giocatori)
                .HasForeignKey(d => d.Idsquadra)
                .HasConstraintName("FK__Giocatori__IDSqu__3C69FB99");
        });

        modelBuilder.Entity<Squadre>(entity =>
        {
            entity.HasKey(e => e.Idsquadra).HasName("PK__Squadre__61BC2F00321B9445");

            entity.Property(e => e.Idsquadra).HasColumnName("IDSquadra");
            entity.Property(e => e.Allenatore)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Citta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomeSquadra)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VistaSquadre>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaSquadre");

            entity.Property(e => e.Allenatore)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Citta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idsquadra).HasColumnName("IDSquadra");
            entity.Property(e => e.NomeSquadra)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
