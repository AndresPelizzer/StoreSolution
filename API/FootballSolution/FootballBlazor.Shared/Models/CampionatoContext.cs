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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
