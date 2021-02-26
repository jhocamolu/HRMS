using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Plantillas.Dominio.Enumerador;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Plantillas.Models
{
    public class PlantillasDbContext : DbContext
    {
        public PlantillasDbContext(DbContextOptions<PlantillasDbContext> options)
            : base(options)
        {
        }

        public DbSet<ComplementoPlantilla> ComplementoPlantillas { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
        public DbSet<GrupoDocumento> GrupoDocumentos { get; set; }
        public DbSet<Plantilla> Plantillas { get; set; }
        public DbSet<ServicioFijo> ServicioFijos { get; set; }
        public DbSet<GrupoDocumentoEtiqueta> GrupoDocumentoEtiquetas { get; set; }



        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.State == EntityState.Added
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted
                );

            foreach (var entityEntry in entries)
            {
                if (entityEntry.Metadata.FindProperty("FechaModificacion") != null)
                {
                    entityEntry.Property("FechaModificacion").CurrentValue = DateTime.Now;

                    if (entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Activo;
                        entityEntry.Property("FechaCreacion").CurrentValue = DateTime.Now;
                    }
                    if (entityEntry.State == EntityState.Deleted)
                    {
                        entityEntry.State = EntityState.Modified;
                        entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Eliminado;
                        entityEntry.Property("FechaEliminacion").CurrentValue = DateTime.Now;
                    }
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e =>
              e.State == EntityState.Added
              || e.State == EntityState.Modified
              || e.State == EntityState.Deleted
              );

            foreach (var entityEntry in entries)
            {
                if (entityEntry.Metadata.FindProperty("FechaModificacion") != null)
                {
                    entityEntry.Property("FechaModificacion").CurrentValue = DateTime.Now;

                    if (entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Activo;
                        entityEntry.Property("FechaCreacion").CurrentValue = DateTime.Now;
                    }
                    if (entityEntry.State == EntityState.Deleted)
                    {
                        entityEntry.State = EntityState.Modified;
                        entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Eliminado;
                        entityEntry.Property("FechaEliminacion").CurrentValue = DateTime.Now;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etiqueta>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ComplementoPlantilla>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Plantilla>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Documento>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ServicioFijo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<GrupoDocumento>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<GrupoDocumentoEtiqueta>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);

            modelBuilder.Entity<GrupoDocumentoEtiqueta>()
               .HasIndex(ac => new { ac.EtiquetaId, ac.GrupoDocumentoId }).IsUnique();

            modelBuilder.Entity<Plantilla>()
                   .HasOne(c => c.ComplementoEncabezado)
                   .WithMany(b => b.Encabezados)
                   .HasForeignKey(s => s.EncabezadoId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Plantilla>()
                   .HasOne(c => c.ComplementoPiePagina)
                   .WithMany(b => b.PiePaginas)
                   .HasForeignKey(s => s.PiePaginaId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Plantilla>()
                 .HasOne(c => c.GrupoDocumento)
                 .WithMany(b => b.Plantilla)
                 .HasForeignKey(s => s.GrupoDocumentoId)
                 .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Etiqueta>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ComplementoPlantilla>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Plantilla>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Documento>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ServicioFijo>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<GrupoDocumento>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<GrupoDocumentoEtiqueta>()
             .Property(t => t.EstadoRegistro)
             .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            base.OnModelCreating(modelBuilder);
        }

    }
}
