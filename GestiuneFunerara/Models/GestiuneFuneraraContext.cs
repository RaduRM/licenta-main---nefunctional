using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestiuneFunerara.Models
{
    public partial class GestiuneFuneraraContext : DbContext
    {
        public GestiuneFuneraraContext()
        {
        }

        public GestiuneFuneraraContext(DbContextOptions<GestiuneFuneraraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ComenziProduse> ComenziProduses { get; set; } = null!;
        public virtual DbSet<ConfirmariComandum> ConfirmariComanda { get; set; } = null!;
        public virtual DbSet<PacheteFunerare> PacheteFunerares { get; set; } = null!;
        public virtual DbSet<Precomenzi> Precomenzis { get; set; } = null!;
        public virtual DbSet<Produse> Produses { get; set; } = null!;
        public virtual DbSet<RapoartePrecomandum> RapoartePrecomanda { get; set; } = null!;
        public virtual DbSet<Utilizatori> Utilizatoris { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GestiuneFunerara;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComenziProduse>(entity =>
            {
                entity.HasKey(e => e.id_comanda_produs)
                    .HasName("PK__ComenziP__F09E28878A106522");

                entity.ToTable("ComenziProduse");

                entity.Property(e => e.data_necesara).HasColumnType("datetime");

                entity.Property(e => e.status_livrare)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('in_asteptare')");

                entity.Property(e => e.tip_livrare).HasMaxLength(50);

                entity.HasOne(d => d.id_precomenziNavigation)
                    .WithMany(p => p.ComenziProduses)
                    .HasForeignKey(d => d.id_precomenzi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComandaProdus_Precomanda");

                entity.HasOne(d => d.id_produsNavigation)
                    .WithMany(p => p.ComenziProduses)
                    .HasForeignKey(d => d.id_produs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComandaProdus_Produs");
            });

            modelBuilder.Entity<ConfirmariComandum>(entity =>
            {
                entity.HasKey(e => e.id_confirmare)
                    .HasName("PK__Confirma__1253275386CB8313");

                entity.Property(e => e.data_confirmare)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.inregistrat_de).HasMaxLength(50);

                entity.Property(e => e.metoda_confirmare).HasMaxLength(50);

                entity.HasOne(d => d.id_precomenziNavigation)
                    .WithMany(p => p.ConfirmariComanda)
                    .HasForeignKey(d => d.id_precomenzi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Confirmare_Precomanda");
            });

            modelBuilder.Entity<PacheteFunerare>(entity =>
            {
                entity.HasKey(e => e.id_pachetefunerare);

                entity.ToTable("PacheteFunerare");

                entity.Property(e => e.Denumire).HasMaxLength(100);

                entity.Property(e => e.ImagineURL).HasMaxLength(255);

                entity.Property(e => e.Pret).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Precomenzi>(entity =>
            {
                entity.HasKey(e => e.id_precomenzi);

                entity.ToTable("Precomenzi");

                entity.Property(e => e.CodConfirmare).HasMaxLength(10);

                entity.Property(e => e.DataCreare)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataDeces).HasColumnType("date");

                entity.Property(e => e.LocatieRidicare).HasMaxLength(200);

                entity.Property(e => e.NumeDefunct).HasMaxLength(100);

                entity.Property(e => e.Stare)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('neconfirmata')");

                entity.HasOne(d => d.id_pachetefunerareNavigation)
                    .WithMany(p => p.Precomenzis)
                    .HasForeignKey(d => d.id_pachetefunerare)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Precomenzi_Pachet");

                entity.HasOne(d => d.id_produsNavigation)
                    .WithMany(p => p.Precomenzis)
                    .HasForeignKey(d => d.id_produs)
                    .HasConstraintName("FK_Precomenzi_Produse");

                entity.HasOne(d => d.id_utilizatorNavigation)
                    .WithMany(p => p.Precomenzis)
                    .HasForeignKey(d => d.id_utilizator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Precomenzi_Utilizator");
            });

            modelBuilder.Entity<Produse>(entity =>
            {
                entity.HasKey(e => e.id_produs)
                    .HasName("PK__Produse__3FCF8259CD299CD8");

                entity.ToTable("Produse");

                entity.Property(e => e.nume).HasMaxLength(100);

                entity.Property(e => e.pret_achizitie).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.pret_vanzare).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<RapoartePrecomandum>(entity =>
            {
                entity.HasKey(e => e.id_rapoarteprecomanda)
                    .HasName("PK__Rapoarte__E3448C205D1DF70F");

                entity.Property(e => e.data_raport)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.locatie_decedat).HasMaxLength(200);

                entity.Property(e => e.locatie_tip).HasMaxLength(50);

                entity.Property(e => e.nume_client).HasMaxLength(100);

                entity.Property(e => e.nume_defunct).HasMaxLength(100);

                entity.Property(e => e.pdf_url).HasMaxLength(255);

                entity.Property(e => e.telefon_client).HasMaxLength(20);

                entity.HasOne(d => d.id_precomenziNavigation)
                    .WithMany(p => p.RapoartePrecomanda)
                    .HasForeignKey(d => d.id_precomenzi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RapoartePrecomanda_Precomanda");
            });

            modelBuilder.Entity<Utilizatori>(entity =>
            {
                entity.HasKey(e => e.id_utilizator);

                entity.ToTable("Utilizatori");

                entity.Property(e => e.CodActivare).HasMaxLength(10);

                entity.Property(e => e.CodExpiraLa).HasColumnType("datetime");

                entity.Property(e => e.CodResetare).HasMaxLength(10);

                entity.Property(e => e.DataInregistrare).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EsteActiv).HasDefaultValueSql("((0))");

                entity.Property(e => e.Fotografie)
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.NumeComplet).HasMaxLength(100);

                entity.Property(e => e.Parola_hashuita).HasMaxLength(255);

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Telefon).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
