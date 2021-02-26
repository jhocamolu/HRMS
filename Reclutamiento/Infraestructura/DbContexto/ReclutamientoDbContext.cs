

using ApiV3.Infraestructura.Enumerador;
using ApiV3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reclutamiento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reclutamiento.Infraestructura.DbContexto
{
    public class ReclutamientoDbContext : IdentityDbContext<UsuarioAplicacion> 
    {
        public ReclutamientoDbContext(DbContextOptions<ReclutamientoDbContext> options)
           : base(options)
        {
        }

        #region Entity
        
        public virtual DbSet<ClaseLibretaMilitar> ClaseLibretaMilitares { get; set; }
        public virtual DbSet<Candidato> Candidatos { get; set; }
        public virtual DbSet<DivisionPoliticaNivel2> DivisionPoliticaNiveles2 { get; set; }
        public virtual DbSet<EstadoCivil> EstadoCiviles { get; set; }
        public virtual DbSet<HojaDeVida> HojaDeVidas { get; set; }
        public virtual DbSet<HojaDeVidaEstudio> HojaDeVidaEstudios { get; set; }
        public virtual DbSet<HojaDeVidaDocumento> HojaDeVidaDocumentos { get; set; }
        public virtual DbSet<HojaDeVidaExperienciaLaboral> HojaDeVidaExperienciaLaborales { get; set; }
        public virtual DbSet<InformacionBasica> InformacionBasicas { get; set; }
        public virtual DbSet<LicenciaConduccion> LicenciaConducciones { get; set; }
        public virtual DbSet<NivelEducativo> NivelEducativos{ get; set; }
        public virtual DbSet<NotificacionPlantilla> NotificacionPlantillas { get; set; }
        public virtual DbSet<Notificacion> Notificaciones { get; set; }
        public virtual DbSet<NotificacionDestinatario> NotificacionDestinatarios { get; set; }
        public virtual DbSet<Ocupacion> Ocupaciones { get; set; }
        public virtual DbSet<Pais> Paises { get; set; }
        public virtual DbSet<Profesion> Profesiones { get; set; }
        public virtual DbSet<RequisicionPersonal> RequisicionPersonales { get; set; }
        public virtual DbSet<TipoSangre> TipoSangres { get; set; }
        public virtual DbSet<TipoSoporte> TipoSoportes { get; set; }
        public virtual DbSet<TipoVivienda> TipoViviendas { get; set; }
        public virtual DbSet<TareaProgramada> TareaProgramadas { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public virtual DbSet<Sexo> Sexos { get; set; }
        #endregion



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
            modelBuilder.Entity<Candidato>()
                .HasIndex(p => new { p.RequisicionPersonalId, p.HojaDeVidaId, p.EstadoRegistro }).IsUnique();
            #region Ignore 
            modelBuilder.Entity<Notificacion>()
                .Ignore(i => i.Pendiente)
                .Ignore(i => i.Enviado)
                .Ignore(i => i.Fallido);

            
            modelBuilder.Entity<HojaDeVida>()
                .Ignore(i => i.Edad);

            modelBuilder.Entity<NominaDetalle>()
                .Ignore(i => i.ValorEditable);

            #endregion

            modelBuilder.Entity<DependenciaJerarquia>()
                   .HasOne(c => c.DependenciaHijo)
                   .WithMany(b => b.SoyHijoDe)
                   .HasForeignKey(s => s.DependenciaHijoId);

            modelBuilder.Entity<DependenciaJerarquia>()
                   .HasOne(c => c.DependenciaPadre)
                   .WithMany(b => b.SoyPadreDe)
                   .HasForeignKey(s => s.DependenciaPadreId);

            modelBuilder.Entity<Sustituto>()
                   .HasOne(c => c.CargoASustituir)
                   .WithMany(b => b.ASustituir)
                   .HasForeignKey(s => s.CargoASustituirId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sustituto>()
                   .HasOne(c => c.CargoSustituto)
                   .WithMany(b => b.Sustituto)
                   .HasForeignKey(s => s.CargoSustitutoId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sustituto>()
                   .HasOne(c => c.CentroOperativoASutituir)
                   .WithMany(b => b.CentroASustituir)
                   .HasForeignKey(s => s.CentroOperativoASutituirId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sustituto>()
                   .HasOne(c => c.CentroOperativoSustituto)
                   .WithMany(b => b.CentroSustituto)
                   .HasForeignKey(s => s.CentroOperativoSustitutoId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CargoReporta>()
                   .HasOne(c => c.CargoDependenciaReporta)
                   .WithMany(c => c.MeReportan)
                   .HasForeignKey(s => s.CargoDependenciaReportaId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Candidato>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoCandidato>());

            modelBuilder.Entity<Candidato>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());


            modelBuilder.Entity<RequisicionPersonal>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());
            
            modelBuilder.Entity<RequisicionPersonal>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoRequisicionPersonal>());

            modelBuilder.Entity<RequisicionPersonal>()
                .Property(t => t.TipoReclutamiento)
                .HasConversion(new EnumToStringConverter<TipoReclutamiento>());

            modelBuilder.HasDefaultSchema("reclutamiento");
            base.OnModelCreating(modelBuilder);
            


        }
        
    }
}
