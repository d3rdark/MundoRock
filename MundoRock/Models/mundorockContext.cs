using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MundoRock.Models
{
    public partial class mundorockContext : DbContext
    {
        public mundorockContext()
        {
        }

        public mundorockContext(DbContextOptions<mundorockContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Bandum> Banda { get; set; }
        public virtual DbSet<Integrante> Integrantes { get; set; }
        public virtual DbSet<Reseña> Reseñas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("album");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Lanzamiento).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Bandum>(entity =>
            {
                entity.ToTable("banda");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(600);

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Origen)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Integrante>(entity =>
            {
                entity.ToTable("integrantes");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nacionalidad)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NombrePila)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Reseña>(entity =>
            {
                entity.ToTable("reseñas");

                entity.HasIndex(e => e.Idalbum, "fkalbum_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idalbum).HasColumnName("idalbum");

                entity.Property(e => e.Informacion)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.HasOne(d => d.IdalbumNavigation)
                    .WithMany(p => p.Reseñas)
                    .HasForeignKey(d => d.Idalbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkalbum");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.NombreReal)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
