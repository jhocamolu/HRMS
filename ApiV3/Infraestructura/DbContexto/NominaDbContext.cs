using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using ApiV3.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TipoHoraExtra = ApiV3.Models.TipoHoraExtra;

namespace ApiV3.Infraestructura.DbContexto
{
    public class NominaDbContext : DbContext
    {
        private readonly MongoService mongoService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public NominaDbContext(DbContextOptions<NominaDbContext> options, MongoService mongoService, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.mongoService = mongoService;
            this.httpContextAccessor = httpContextAccessor;
        }

        #region Entity
        public virtual DbSet<Actividad> Actividades { get; set; }
        public virtual DbSet<ActividadCentroCosto> ActividadCentroCostos { get; set; }
        public virtual DbSet<ActividadEconomica> ActividadEconomicas { get; set; }
        public virtual DbSet<ActividadFuncionario> ActividadFuncionarios { get; set; }
        public virtual DbSet<ActividadFuncionarioDatoActual> ActividadFuncionarioDatoActuales { get; set; }
        public virtual DbSet<ActividadFuncionarioCentroCosto> ActividadFuncionarioCentroCostos { get; set; }
        public virtual DbSet<Administradora> Administradoras { get; set; }
        public virtual DbSet<AplicacionExterna> AplicacionExternas { get; set; }
        public virtual DbSet<AplicacionExternaCargo> AplicacionExternaCargos { get; set; }
        public virtual DbSet<AplicacionExternaCargoDependiente> AplicacionExternaCargoDependientes { get; set; }
        public virtual DbSet<AusentismoFuncionario> AusentismoFuncionarios { get; set; }
        public virtual DbSet<AnnoVigencia> AnnoVigencias { get; set; }
        public virtual DbSet<Beneficio> Beneficios { get; set; }
        public virtual DbSet<BeneficioSubperiodo> BeneficioSubperiodos { get; set; }
        public virtual DbSet<BeneficioAdjunto> BeneficioAdjuntos { get; set; }
        public virtual DbSet<Candidato> Candidatos { get; set; }
        public virtual DbSet<Calendario> Calendarios { get; set; }
        public virtual DbSet<CausalTerminacion> CausalTerminaciones { get; set; }
        public virtual DbSet<Cargo> Cargos { get; set; }
        public virtual DbSet<CargoCentroCosto> CargoCentroCostos { get; set; }
        public virtual DbSet<CargoDependencia> CargoDependencias { get; set; }
        public virtual DbSet<CargoGrado> CargoGrados { get; set; }
        public virtual DbSet<CargoGrupo> CargoGrupos { get; set; }
        public virtual DbSet<CargoPresupuesto> CargoPresupuestos { get; set; }
        public virtual DbSet<CargoReporta> CargoReportas { get; set; }
        public virtual DbSet<CategoriaNovedad> CategoriaNovedades { get; set; }
        public virtual DbSet<CategoriaParametro> CategoriaParametros { get; set; }
        public virtual DbSet<CentroCosto> CentroCostos { get; set; }
        public virtual DbSet<CentroOperativo> CentroOperativos { get; set; }
        public virtual DbSet<CentroTrabajo> CentroTrabajos { get; set; }
        public virtual DbSet<ClaseAusentismo> ClaseAusentismos { get; set; }
        public virtual DbSet<ClaseAportante> ClaseAportantes { get; set; }
        public virtual DbSet<ClaseAportanteTipoAportante> ClaseAportanteTipoAportantes { get; set; }
        public virtual DbSet<ClaseAportanteTipoCotizante> ClaseAportanteTipoCotizantes { get; set; }
        public virtual DbSet<ClaseLibretaMilitar> ClaseLibretaMilitares { get; set; }
        public virtual DbSet<ConceptoBase> ConceptoBases { get; set; }
        public virtual DbSet<ConceptoNomina> ConceptoNominas { get; set; }
        public virtual DbSet<ConceptoNominaTipoAdministradora> ConceptoNominaTipoAdministradoras { get; set; }
        public virtual DbSet<ContratoAdministradoraCambio> ContratoAdministradoraCambios { get; set; }
        public virtual DbSet<ContratoCentroTrabajoCambio> ContratoCentroTrabajoCambios { get; set; }
        public virtual DbSet<ConceptoNominaElementoFormula> ConceptoNominaElementoFormulas { get; set; }
        public virtual DbSet<TipoAusentismoConceptoNomina> TipoAusentismoConceptoNominas { get; set; }
        public virtual DbSet<ConceptoNominaCuentaContable> ConceptoNominaCuentaContables { get; set; }
        public virtual DbSet<Contrato> Contratos { get; set; }
        public virtual DbSet<ContratoCentroTrabajo> ContratoCentroTrabajos { get; set; }
        public virtual DbSet<ContratoAdministradora> ContratoAdministradoras { get; set; }
        public virtual DbSet<CuentaContable> CuentaContables { get; set; }
        public virtual DbSet<CuentaBancaria> CuentaBancarias { get; set; }
        public virtual DbSet<DiagnosticoCie> DiagnosticoCies { get; set; }
        public virtual DbSet<Dependencia> Dependencias { get; set; }
        public virtual DbSet<DependenciaJerarquia> DependenciaJerarquias { get; set; }
        public virtual DbSet<DivisionPoliticaNivel1> DivisionPoliticaNiveles1 { get; set; }
        public virtual DbSet<DivisionPoliticaNivel2> DivisionPoliticaNiveles2 { get; set; }
        public virtual DbSet<DocumentoFuncionario> DocumentoFuncionarios { get; set; }
        public virtual DbSet<DeduccionRetefuente> DeduccionRetefuentes { get; set; }
        public virtual DbSet<Embargo> Embargos { get; set; }
        public virtual DbSet<EmbargoConceptoNomina> EmbargoConceptoNominas { get; set; }
        public virtual DbSet<EmbargoSubperiodo> EmbargoSubperiodos { get; set; }
        public virtual DbSet<EntidadFinanciera> EntidadFinancieras { get; set; }
        public virtual DbSet<EstadoCivil> EstadoCiviles { get; set; }
        public virtual DbSet<ExperienciaLaboral> ExperienciaLaborales { get; set; }
        public virtual DbSet<FormaPago> FormaPagos { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<FuncionarioCentroCosto> FuncionarioCentroCostos { get; set; }
        public virtual DbSet<FuncionarioDatoActual> FuncionarioDatoActuales { get; set; }
        public virtual DbSet<FuncionNomina> FuncionNominas { get; set; }
        public virtual DbSet<FuncionarioEstudio> FuncionarioEstudios { get; set; }
        public virtual DbSet<GastoViaje> GastoViajes { get; set; }
        public virtual DbSet<Grupo> Grupos { get; set; }
        public virtual DbSet<GrupoNomina> GrupoNominas { get; set; }
        public virtual DbSet<HojaDeVida> HojaDeVidas { get; set; }
        public virtual DbSet<HojaDeVidaDocumento> HojaDeVidaDocumentos { get; set; }
        public virtual DbSet<HojaDeVidaEstudio> HojaDeVidaEstudios { get; set; }
        public virtual DbSet<HojaDeVidaExperienciaLaboral> HojaDeVidaExperienciaLaborales { get; set; }
        public virtual DbSet<HoraExtra> HoraExtras { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<InformacionBasica> InformacionBasicas { get; set; }
        public virtual DbSet<InformacionFamiliar> InformacionFamiliares { get; set; }
        public virtual DbSet<Juzgado> Juzgados { get; set; }
        public virtual DbSet<JornadaLaboral> JornadaLaborales { get; set; }
        public virtual DbSet<JornadaLaboralDia> JornadaLaboralDias { get; set; }
        public virtual DbSet<MotivoSolicitudCesantia> MotivoSolicitudCesantias { get; set; }
        public virtual DbSet<MotivoVacante> MotivoVacantes { get; set; }
        public virtual DbSet<NivelCargo> NivelCargos { get; set; }
        public virtual DbSet<NivelEducativo> NivelEducativos { get; set; }
        public virtual DbSet<NaturalezaJuridica> NaturalezaJuridicas { get; set; }
        public virtual DbSet<Nomina> Nominas { get; set; }
        public virtual DbSet<NominaContabilidad> NominaContabilidades { get; set; }
        public virtual DbSet<NominaCentroCosto> NominaCentroCostos { get; set; }
        public virtual DbSet<NominaFuncionario> NominaFuncionarios { get; set; }
        public virtual DbSet<NominaFuenteNovedad> NominaFuenteNovedades { get; set; }
        public virtual DbSet<NominaDetalle> NominaDetalles { get; set; }
        public virtual DbSet<NomenclaturaDian> NomenclaturaDians { get; set; }
        public virtual DbSet<Notificacion> Notificaciones { get; set; }
        public virtual DbSet<NotificacionDestinatario> NotificacionDestinatarios { get; set; }
        public virtual DbSet<NotificacionDestinatarioLog> NotificacionDestinatarioLogs { get; set; }
        public virtual DbSet<Novedad> Novedades { get; set; }
        public virtual DbSet<NovedadSubperiodo> NovedadSubperiodos { get; set; }
        public virtual DbSet<Libranza> Libranzas { get; set; }
        public virtual DbSet<LibranzaSubperiodo> LibranzaSubperiodos { get; set; }
        public virtual DbSet<LibroVacacion> LibroVacaciones { get; set; }
        public virtual DbSet<LibroVacacionesConsolidado> LibroVacacionesConsolidados { get; set; }
        public virtual DbSet<LicenciaConduccion> LicenciaConducciones { get; set; }
        public virtual DbSet<Ocupacion> Ocupaciones { get; set; }
        public virtual DbSet<OperadorPago> OperadorPagos { get; set; }
        public virtual DbSet<ContratoOtroSi> ContratoOtroSis { get; set; }
        public virtual DbSet<Pais> Paises { get; set; }
        public virtual DbSet<ParametroGeneral> ParametroGenerales { get; set; }
        public virtual DbSet<Parentesco> Parentescos { get; set; }
        public virtual DbSet<NotificacionPlantilla> NotificacionPlantillas { get; set; }
        public virtual DbSet<PeriodoContable> PeriodoContables { get; set; }
        public virtual DbSet<Profesion> Profesiones { get; set; }
        public virtual DbSet<ProrrogaAusentismo> ProrrogaAusentismos { get; set; }
        public virtual DbSet<RangoUvt> RangoUvts { get; set; }
        public virtual DbSet<RepresentanteEmpresa> RepresentanteEmpresas { get; set; }
        public virtual DbSet<RequisicionPersonal> RequisicionPersonales { get; set; }
        public virtual DbSet<Sexo> Sexos { get; set; }
        public virtual DbSet<SolicitudCesantia> SolicitudCesantias { get; set; }
        public virtual DbSet<SolicitudPermiso> SolicitudPermisos { get; set; }
        public virtual DbSet<SolicitudVacacion> SolicitudVacaciones { get; set; }
        public virtual DbSet<SolicitudVacacionesInterrupcion> SolicitudVacacionesInterrupciones { get; set; }
        public virtual DbSet<SoporteSolicitudPermiso> SoporteSolicitudPermisos { get; set; }
        public virtual DbSet<SubPeriodo> SubPeriodos { get; set; }
        public virtual DbSet<SubtipoCotizante> SubtipoCotizantes { get; set; }
        public virtual DbSet<Sustituto> Sustitutos { get; set; }
        public virtual DbSet<TareaProgramada> TareaProgramadas { get; set; }
        public virtual DbSet<TareaProgramadaLog> TareasProgramadasLogs { get; set; }
        public virtual DbSet<Tercero> Terceros { get; set; }
        public virtual DbSet<TipoAccion> TipoAcciones { get; set; }
        public virtual DbSet<TipoAdministradora> TipoAdministradoras { get; set; }
        public virtual DbSet<TipoAusentismo> TipoAusentismos { get; set; }
        public virtual DbSet<TipoAportante> TipoAportantes { get; set; }
        public virtual DbSet<TipoAportanteTipoCotizante> TipoAportanteTipoCotizantes { get; set; }
        public virtual DbSet<TipoAportanteTipoPlanilla> TipoAportanteTipoPlanillas { get; set; }
        public virtual DbSet<TipoBeneficio> TipoBeneficios { get; set; }
        public virtual DbSet<TipoBeneficioRequisito> TipoBeneficioRequisitos { get; set; }
        public virtual DbSet<TipoContrato> TipoContratos { get; set; }
        public virtual DbSet<TipoContribuyente> TipoContribuyentes { get; set; }
        public virtual DbSet<TipoCotizante> TipoCotizantes { get; set; }
        public virtual DbSet<TipoCotizanteSubtipoCotizante> TipoCotizanteSubtipoCotizantes { get; set; }
        public virtual DbSet<TipoCotizanteTipoPlanilla> TipoCotizanteTipoPlanillas { get; set; }
        public virtual DbSet<TipoCuenta> TipoCuentas { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public virtual DbSet<TipoEmbargo> TipoEmbargos { get; set; }
        public virtual DbSet<TipoEmbargoConceptoNomina> TipoEmbargoConceptoNominas { get; set; }
        public virtual DbSet<TipoGastoViaje> TipoGastoViajes { get; set; }
        public virtual DbSet<TipoHoraExtra> TipoHoraExtras { get; set; }
        public virtual DbSet<TipoLiquidacion> TipoLiquidaciones { get; set; }
        public virtual DbSet<TipoLiquidacionConcepto> TipoLiquidacionConceptos { get; set; }
        public virtual DbSet<TipoLiquidacionComprobante> TipoLiquidacionComprobantes { get; set; }
        public virtual DbSet<TipoLiquidacionEstado> TipoLiquidacionEstados { get; set; }
        public virtual DbSet<TipoLiquidacionModulo> TipoLiquidacionModulos { get; set; }
        public virtual DbSet<TipoOtroSi> TipoOtroSis { get; set; }
        public virtual DbSet<TipoSangre> TipoSangres { get; set; }
        public virtual DbSet<TipoVivienda> TipoViviendas { get; set; }
        public virtual DbSet<TipoSoporte> TipoSoportes { get; set; }
        public virtual DbSet<TipoMoneda> TipoMonedas { get; set; }
        public virtual DbSet<TipoPeriodo> TipoPeriodos { get; set; }
        public virtual DbSet<TipoPersona> TipoPersonas { get; set; }
        public virtual DbSet<TipoPlanilla> TipoPlanillas { get; set; }
        public virtual DbSet<VariableNomina> VariableNominas { get; set; }
        public virtual DbSet<FuncionVariable> FuncionVariables { get; set; }
        public virtual DbSet<_LogConfiguracion> _LogConfiguraciones { get; set; }
        public virtual DbSet<_EnlaceExterno> _EnlaceExternos { get; set; }
        public virtual DbSet<_MenuFavorito> _MenuFavoritos { get; set; }

        #endregion

        #region EntityQueryables
        public IQueryable<Dashboard> Dashboards(int FuncionarioId, string Permisos) =>
           Set<Dashboard>().FromSqlInterpolated($@"
            DECLARE @FuncionarioId int = {FuncionarioId};
            DECLARE @Permisos nvarchar(max) = {Permisos};
            DECLARE @Resultado int
            EXECUTE [util].[USP_ObtenerDashboard] 
                @FuncionarioId
                ,@Permisos
                ,@Resultado OUTPUT");
        #endregion
            
        #region InsertLog
        private void InsertLog(string model, dynamic id, dynamic elementoAnterior, dynamic elementoActual, string accion, string usuario)
        {
            try
            {
                var config = _LogConfiguraciones.Where(c => c.Model == model && c.Activo == true).SingleOrDefault();

                if (config != null)
                {
                    string jsonElementoActual = JsonConvert.SerializeObject(elementoActual);
                    string identificacion = id.ToString();
                    string jsonElementoAnterior = JsonConvert.SerializeObject(elementoAnterior);

                    mongoService.Create(jsonElementoAnterior, identificacion, jsonElementoActual, config.Tabla, accion, usuario);
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region SaveChanges
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.State == EntityState.Added
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted
                );

            string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "null";

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("FechaModificacion").CurrentValue = DateTime.Now;
                    entityEntry.Property("ModificadoPor").CurrentValue = usuario;
                }
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Activo;
                    entityEntry.Property("FechaCreacion").CurrentValue = DateTime.Now;
                    entityEntry.Property("CreadoPor").CurrentValue = usuario;
                }
                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Eliminado;
                    entityEntry.Property("FechaEliminacion").CurrentValue = DateTime.Now;
                    entityEntry.Property("EliminadoPor").CurrentValue = usuario;
                }
            }

            List<EntityEntry> copy = new List<EntityEntry>();
            string estado = null;
            dynamic id = null;
            dynamic elementoAnterior = null;
            foreach (var entityEntry in entries)
            {
                copy.Add(entityEntry);
                estado = entityEntry.State.ToString();
                if (estado != "Added")
                {
                    id = entityEntry.GetDatabaseValues().GetValue<dynamic>("Id");
                    elementoAnterior = entityEntry.GetDatabaseValues().ToObject() == null ? null : entityEntry.GetDatabaseValues().ToObject();
                }
            }

            var guardado = base.SaveChanges();

            foreach (var entityEntry in copy)
            {
                if (estado == "Added")
                {
                    id = entityEntry.GetDatabaseValues().GetValue<dynamic>("Id");
                }
                var elementoActual = entityEntry.GetDatabaseValues() == null ? null : entityEntry.GetDatabaseValues().ToObject();
                this.InsertLog(entityEntry.Metadata.Name, id, elementoAnterior, elementoActual, estado, usuario);
            }
            return guardado;
        }
        #endregion

        #region SaveChangesAsync
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e =>
              e.State == EntityState.Added
              || e.State == EntityState.Modified
              || e.State == EntityState.Deleted
              );

            string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "null";

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("FechaModificacion").CurrentValue = DateTime.Now;
                    entityEntry.Property("ModificadoPor").CurrentValue = usuario;
                }
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Activo;
                    entityEntry.Property("FechaCreacion").CurrentValue = DateTime.Now;
                    entityEntry.Property("CreadoPor").CurrentValue = usuario;
                }
                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    entityEntry.Property("EstadoRegistro").CurrentValue = EstadoRegistro.Eliminado;
                    entityEntry.Property("FechaEliminacion").CurrentValue = DateTime.Now;
                    entityEntry.Property("EliminadoPor").CurrentValue = usuario;
                }
            }
            List<EntityEntry> copy = new List<EntityEntry>();
            string estado = null;
            dynamic id = null;
            dynamic elementoAnterior = null;
            foreach (var entityEntry in entries)
            {
                copy.Add(entityEntry);
                estado = entityEntry.State.ToString();
                if (estado != "Added")
                {
                    id = entityEntry.GetDatabaseValues().GetValue<dynamic>("Id");
                    elementoAnterior = entityEntry.GetDatabaseValues().ToObject() == null ? null : entityEntry.GetDatabaseValues().ToObject();
                }
            }

            var guardado = await base.SaveChangesAsync(cancellationToken);

            foreach (var entityEntry in copy)
            {
                if (estado == "Added")
                {
                    id = entityEntry.GetDatabaseValues().GetValue<dynamic>("Id");
                }
                var elementoActual = entityEntry.GetDatabaseValues() == null ? null : entityEntry.GetDatabaseValues().ToObject();
                this.InsertLog(entityEntry.Metadata.Name, id, elementoAnterior, elementoActual, estado, usuario);
            }
            return guardado;

        }
        #endregion

        #region ModelCreate
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Ignore 
            modelBuilder.Entity<Notificacion>()
                .Ignore(i => i.Pendiente)
                .Ignore(i => i.Enviado)
                .Ignore(i => i.Fallido);

            modelBuilder.Entity<LibroVacacion>()
                .Ignore(i => i.FuncionarioId);

            modelBuilder.Entity<HojaDeVida>()
                .Ignore(i => i.Edad);

            modelBuilder.Entity<NominaDetalle>()
                .Ignore(i => i.ValorEditable);

            modelBuilder.Entity<AusentismoFuncionario>()
                .Ignore(i => i.NumeroDeDias);

            modelBuilder.Entity<AusentismoFuncionario>()
                .Ignore(i => i.ValidaFechaFinal);

            modelBuilder.Entity<AusentismoFuncionario>()
                .Ignore(i => i.ValidaTodo);

            #endregion

            #region View
            modelBuilder.Entity<FuncionarioDatoActual>(eb =>
            {
                eb.ToView("VW_FuncionarioDatoActual");
            });

            modelBuilder.Entity<ContratoAdministradoraCambio>(eb =>
            {
                eb.ToView("VW_ContratoAdministradoraCambios");
            });

            modelBuilder.Entity<ContratoCentroTrabajoCambio>(eb =>
            {
                eb.ToView("VW_ContratoCentroTrabajoCambios");
            });

            modelBuilder.Entity<ActividadFuncionarioDatoActual>(eb =>
            {
                eb.ToView("VW_ActividadFuncionarioDatoActual");
            });

            modelBuilder.Entity<LibroVacacionesConsolidado>(eb =>
            {
                eb.ToView("VW_LibroVacacionesConsolidado");
            });
            

            modelBuilder.Entity<Dashboard>().HasNoKey().ToView(null);
            #endregion

            #region Indexes
            modelBuilder.Entity<Candidato>()
                .HasIndex(p => new { p.RequisicionPersonalId, p.HojaDeVidaId, p.EstadoRegistro }).IsUnique();

            modelBuilder.Entity<CuentaBancaria>()
                .HasIndex(p => p.Numero).IsUnique();
            modelBuilder.Entity<CuentaBancaria>()
                .HasIndex(p => p.Nombre).IsUnique();

            modelBuilder.Entity<TipoPersona>()
                .HasIndex(p => p.Codigo).IsUnique();
            modelBuilder.Entity<TipoPersona>()
                .HasIndex(p => p.Codigo).IsUnique();
            modelBuilder.Entity<TipoPersona>()
                .HasIndex(p => p.Nombre).IsUnique();

            modelBuilder.Entity<TipoPlanilla>()
                .HasIndex(p => p.Codigo).IsUnique();
            modelBuilder.Entity<TipoPlanilla>()
                .HasIndex(p => p.Nombre).IsUnique();

            modelBuilder.Entity<TipoCotizante>()
                .HasIndex(p => p.Codigo).IsUnique();
            modelBuilder.Entity<TipoCotizante>()
                .HasIndex(p => p.Nombre).IsUnique();

            modelBuilder.Entity<TipoAportante>()
               .HasIndex(p => p.Codigo).IsUnique();
            modelBuilder.Entity<TipoAportante>()
                .HasIndex(p => p.Nombre).IsUnique();

            modelBuilder.Entity<TipoCuenta>()
                .HasIndex(p => p.Codigo).IsUnique();

            modelBuilder.Entity<TipoDocumento>()
                .HasIndex(p => p.EquivalenteBancario).IsUnique();

            modelBuilder.Entity<ClaseAportante>()
               .HasIndex(p => p.Codigo).IsUnique();
            modelBuilder.Entity<ClaseAportante>()
                .HasIndex(p => p.Nombre).IsUnique();

            modelBuilder.Entity<SubtipoCotizante>()
               .HasIndex(p => p.Codigo).IsUnique();
            modelBuilder.Entity<SubtipoCotizante>()
                .HasIndex(p => p.Nombre).IsUnique();

            modelBuilder.Entity<ClaseAusentismo>()
                .HasIndex(p => p.Codigo).IsUnique();

            modelBuilder.Entity<ConceptoNomina>()
                .HasIndex(b => b.Alias);

            modelBuilder.Entity<NominaFuncionario>()
                .HasOne(c => c.Funcionario)
                .WithMany()
                .HasForeignKey(c => c.FuncionarioId);

            modelBuilder.Entity<NominaFuncionario>()
                .HasIndex(p => new { p.NominaId, p.FuncionarioId }).IsUnique();

            modelBuilder.Entity<TareaProgramada>()
                .HasIndex(p => p.Alias).IsUnique();

            modelBuilder.Entity<TipoAusentismo>()
                .HasIndex(p => p.Codigo).IsUnique();

            modelBuilder.Entity<Actividad>()
                .HasIndex(p => p.Codigo).IsUnique();

            #endregion

            #region Restrict

            modelBuilder.Entity<NominaCentroCosto>()
               .HasOne(t => t.ConceptoNomina)
               .WithMany()
               .HasForeignKey(s => s.ConceptoNominaId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaCentroCosto>()
               .HasOne(t => t.NominaFuncionario)
               .WithMany()
               .HasForeignKey(s => s.NominaFuncionarioId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaCentroCosto>()
               .HasOne(t => t.CentroCosto)
               .WithMany()
               .HasForeignKey(s => s.CentroCostoId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaContabilidad>()
               .HasOne(t => t.ConceptoNomina)
               .WithMany()
               .HasForeignKey(s => s.ConceptoNominaId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaContabilidad>()
               .HasOne(t => t.NominaFuncionario)
               .WithMany()
               .HasForeignKey(s => s.NominaFuncionarioId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaContabilidad>()
               .HasOne(t => t.CentroCosto)
               .WithMany()
               .HasForeignKey(s => s.CentroCostoId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaContabilidad>()
               .HasOne(t => t.CuentaContable)
               .WithMany()
               .HasForeignKey(s => s.CuentaContableId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConceptoNominaTipoAdministradora>()
               .HasOne(t => t.ConceptoNomina)
               .WithMany()
               .HasForeignKey(s => s.ConceptoNominaId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConceptoNominaTipoAdministradora>()
               .HasOne(t => t.TipoAdministradora)
               .WithMany()
               .HasForeignKey(s => s.TipoAdministradoraId)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Contrato>()
               .HasOne(t => t.CausalTerminacion)
               .WithMany()
               .HasForeignKey(s => s.CausalTerminacionId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GastoViaje>()
               .HasOne(t => t.Funcionario)
               .WithMany()
               .HasForeignKey(s => s.FuncionarioId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GastoViaje>()
               .HasOne(t => t.TipoGastoViaje)
               .WithMany()
               .HasForeignKey(s => s.TipoGastoViajeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoGastoViaje>()
               .HasOne(t => t.ConceptoNomina)
               .WithMany()
               .HasForeignKey(s => s.ConceptoNominaId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InformacionBasica>()
                .HasOne(t => t.TipoDocumento)
                .WithMany()
                .HasForeignKey(s => s.TipoDocumentoId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<InformacionBasica>()
                .HasOne(t => t.NaturalezaJuridica)
                .WithMany()
                .HasForeignKey(s => s.NaturalezaJuridicaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InformacionBasica>()
                .HasOne(t => t.TipoPersona)
                .WithMany()
                .HasForeignKey(s => s.TipoPersonaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InformacionBasica>()
                .HasOne(t => t.ClaseAportanteTipoAportante)
                .WithMany()
                .HasForeignKey(s => s.ClaseAportanteTipoAportanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InformacionBasica>()
                .HasOne(t => t.Cargo)
                .WithMany()
                .HasForeignKey(s => s.CargoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HojaDeVida>()
                   .HasOne(c => c.DivisionPoliticaNivel2ExpedicionDocumento)
                   .WithMany()
                   .HasForeignKey(s => s.DivisionPoliticaNivel2ExpedicionDocumentoId)
                   .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<HojaDeVida>()
                   .HasOne(c => c.DivisionPoliticaNivel2Origen)
                   .WithMany()
                   .HasForeignKey(s => s.DivisionPoliticaNivel2OrigenId)
                   .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<HojaDeVida>()
                   .HasOne(c => c.DivisionPoliticaNivel2Residencia)
                   .WithMany()
                   .HasForeignKey(s => s.DivisionPoliticaNivel2ResidenciaId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoCotizanteSubtipoCotizante>()
                .HasOne(t => t.SubtipoCotizante)
                .WithMany()
                .HasForeignKey(s => s.SubtipoCotizanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoCotizanteSubtipoCotizante>()
                .HasOne(t => t.TipoCotizante)
                .WithMany()
                .HasForeignKey(s => s.TipoCotizanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoAportanteTipoPlanilla>()
                .HasOne(t => t.TipoAportante)
                .WithMany()
                .HasForeignKey(s => s.TipoAportanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoAportanteTipoPlanilla>()
                .HasOne(t => t.TipoPlanilla)
                .WithMany()
                .HasForeignKey(s => s.TipoPlanillaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoAportanteTipoCotizante>()
                .HasOne(t => t.TipoAportante)
                .WithMany()
                .HasForeignKey(s => s.TipoAportanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoAportanteTipoCotizante>()
                .HasOne(t => t.TipoCotizante)
                .WithMany()
                .HasForeignKey(s => s.TipoCotizanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClaseAportanteTipoCotizante>()
                .HasOne(t => t.TipoCotizante)
                .WithMany()
                .HasForeignKey(s => s.TipoCotizanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClaseAportanteTipoCotizante>()
                .HasOne(t => t.ClaseAportante)
                .WithMany()
                .HasForeignKey(s => s.ClaseAportanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ParametroGeneral>()
                .HasOne(t => t.AnnoVigencia)
                .WithMany()
                .HasForeignKey(s => s.AnnoVigenciaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActividadCentroCosto>()
                .HasOne(t => t.Actividad)
                .WithMany()
                .HasForeignKey(s => s.ActividadId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActividadCentroCosto>()
                .HasOne(t => t.CentroCosto)
                .WithMany()
                .HasForeignKey(s => s.CentroCostoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActividadFuncionarioCentroCosto>()
                .HasOne(t => t.ActividadFuncionario)
                .WithMany()
                .HasForeignKey(s => s.ActividadFuncionarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActividadFuncionarioCentroCosto>()
                .HasOne(t => t.FuncionarioCentroCosto)
                .WithMany()
                .HasForeignKey(s => s.FuncionarioCentroCostoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActividadCentroCosto>()
                .HasOne(t => t.Municipio)
                .WithMany()
                .HasForeignKey(s => s.MunicipioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActividadFuncionario>()
               .HasOne(t => t.Municipio)
               .WithMany()
               .HasForeignKey(s => s.MunicipioId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActividadFuncionario>()
               .HasOne(t => t.Funcionario)
               .WithMany()
               .HasForeignKey(s => s.FuncionarioId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActividadFuncionario>()
               .HasOne(t => t.Actividad)
               .WithMany()
               .HasForeignKey(s => s.ActividadId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AplicacionExternaCargo>()
                .HasOne(t => t.AplicacionExterna)
                .WithMany()
                .HasForeignKey(s => s.AplicacionExternaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AplicacionExternaCargo>()
                .HasOne(t => t.CargoDependenciaIndependiente)
                .WithMany()
                .HasForeignKey(s => s.CargoDependenciaIndependienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AplicacionExternaCargo>()
                .HasOne(t => t.CentroOperativoDependiente)
                .WithMany()
                .HasForeignKey(s => s.CentroOperativoDependienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AplicacionExternaCargo>()
                .HasOne(t => t.CentroOperativoIndependiente)
                .WithMany()
                .HasForeignKey(s => s.CentroOperativoIndependienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AplicacionExternaCargoDependiente>()
                .HasOne(t => t.CargoDependencia)
                .WithMany()
                .HasForeignKey(s => s.CargoDependenciaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AplicacionExternaCargoDependiente>()
                .HasOne(t => t.AplicacionExternaCargo)
                .WithMany()
                .HasForeignKey(s => s.AplicacionExternaCargoId);


            modelBuilder.Entity<CargoGrupo>()
                .HasOne(t => t.Cargo)
                .WithMany()
                .HasForeignKey(s => s.CargoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CargoGrupo>()
                 .HasOne(t => t.Grupo)
                 .WithMany()
                 .HasForeignKey(s => s.GrupoId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CargoPresupuesto>()
                 .HasOne(t => t.AnnoVigencia)
                 .WithMany()
                 .HasForeignKey(s => s.AnnoVigenciaId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BeneficioAdjunto>()
                .HasOne(t => t.TipoBeneficioRequisito)
                .WithMany()
                .HasForeignKey(s => s.TipoBeneficioRequisitoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaDetalle>()
                .HasOne(t => t.NominaFuncionario)
                .WithMany()
                .HasForeignKey(t => t.NominaFuncionarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaDetalle>()
                .HasOne(t => t.ConceptoNomina)
                .WithMany()
                .HasForeignKey(t => t.ConceptoNominaId);

            modelBuilder.Entity<AusentismoFuncionario>()
                .HasOne(t => t.Funcionario)
                .WithMany()
                .HasForeignKey(t => t.FuncionarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AusentismoFuncionario>()
                .HasOne(t => t.TipoAusentismo)
                .WithMany()
                .HasForeignKey(t => t.TipoAusentismoId)

                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoLiquidacionConcepto>()
                .HasOne(t => t.TipoLiquidacion)
                .WithMany()
                .HasForeignKey(t => t.TipoliquidacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Juzgado>()
                .HasOne(c => c.DivisionPoliticaNivel2)
                .WithMany()
                .HasForeignKey(s => s.DivisionPoliticaNivel2Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProrrogaAusentismo>()
                   .HasOne(c => c.Prorroga)
                   .WithMany(b => b.ProrrogaDe)
                   .HasForeignKey(s => s.ProrrogaId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProrrogaAusentismo>()
                   .HasOne(c => c.Ausentismo)
                   .WithMany(b => b.AusentismoDe)
                   .HasForeignKey(s => s.AusentismoId)
                   .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ConceptoNominaCuentaContable>()
                 .HasOne(c => c.ConceptoNomina)
                 .WithMany()
                 .HasForeignKey(c => c.ConceptoNominaId)
                 .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Nomina>()
                 .HasOne(c => c.TipoLiquidacion)
                 .WithMany()
                 .HasForeignKey(c => c.TipoLiquidacionId)
                 .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Funcionario>()
                   .HasOne(c => c.DivisionPoliticaNivel2ExpedicionDocumento)
                   .WithMany()
                   .HasForeignKey(s => s.DivisionPoliticaNivel2ExpedicionDocumentoId)
                   .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Funcionario>()
                   .HasOne(c => c.DivisionPoliticaNivel2Origen)
                   .WithMany()
                   .HasForeignKey(s => s.DivisionPoliticaNivel2OrigenId)
                   .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Funcionario>()
                   .HasOne(c => c.DivisionPoliticaNivel2Residencia)
                   .WithMany()
                   .HasForeignKey(s => s.DivisionPoliticaNivel2ResidenciaId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FuncionarioCentroCosto>()
                  .HasOne(c => c.Funcionario)
                  .WithMany()
                  .HasForeignKey(s => s.FuncionarioId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<FuncionarioCentroCosto>()
                  .HasOne(c => c.ActividadCentroCosto)
                  .WithMany()
                  .HasForeignKey(s => s.ActividadCentroCostoId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaFuncionario>()
                   .HasOne(c => c.Funcionario)
                   .WithMany()
                   .HasForeignKey(s => s.FuncionarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InformacionFamiliar>()
                   .HasOne(c => c.Sexo)
                   .WithMany()
                   .HasForeignKey(s => s.SexoId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InformacionFamiliar>()
                   .HasOne(c => c.TipoDocumento)
                   .WithMany()
                   .HasForeignKey(s => s.TipoDocumentoId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InformacionFamiliar>()
                   .HasOne(c => c.DivisionPoliticaNivel2)
                   .WithMany()
                   .HasForeignKey(c => c.DivisionPoliticaNivel2Id)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContratoOtroSi>()
                .HasOne(c => c.Contrato)
                .WithMany()
                .HasForeignKey(s => s.ContratoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tercero>()
                .HasOne(c => c.EntidadFinanciera)
                .WithMany()
                .HasForeignKey(s => s.EntidadFinancieraId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoBeneficio>()
                .HasOne(c => c.ConceptoNominaDevengo)
                .WithMany()
                .HasForeignKey(s => s.ConceptoNominaDevengoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoBeneficio>()
                .HasOne(c => c.ConceptoNominaDeduccion)
                .WithMany()
                .HasForeignKey(s => s.ConceptoNominaDeduccionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoBeneficio>()
                .HasOne(c => c.ConceptoNominaCalculo)
                .WithMany()
                .HasForeignKey(s => s.ConceptoNominaCalculoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudVacacion>()
                .HasOne(c => c.Funcionario)
                .WithMany()
                .HasForeignKey(s => s.FuncionarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudVacacionesInterrupcion>()
                .HasOne(c => c.SolicitudVacacion)
                .WithMany()
                .HasForeignKey(s => s.SolicitudVacacionesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudVacacionesInterrupcion>()
                .HasOne(c => c.AusentismoFuncionario)
                .WithMany()
                .HasForeignKey(s => s.FuncionarioAusentismoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LibroVacacion>()
                .HasOne(c => c.Contrato)
                .WithMany()
                .HasForeignKey(s => s.ContratoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequisicionPersonal>()
               .HasOne(c => c.CargoDependenciaSolicitado)
               .WithMany()
               .HasForeignKey(s => s.CargoDependenciaSolicitadoId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequisicionPersonal>()
                .HasOne(c => c.CargoDependenciaSolicitante)
                .WithMany()
                .HasForeignKey(s => s.CargoDependenciaSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequisicionPersonal>()
                .HasOne(c => c.CentroOperativoSolicitado)
                .WithMany()
                .HasForeignKey(s => s.CentroOperativoSolicitadoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequisicionPersonal>()
                .HasOne(c => c.CentroOperativoSolicitante)
                .WithMany()
                .HasForeignKey(s => s.CentroOperativoSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequisicionPersonal>()
                .HasOne(c => c.FuncionarioAQuienReemplaza)
                .WithMany()
                .HasForeignKey(s => s.FuncionarioAQuienReemplazaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequisicionPersonal>()
                .HasOne(c => c.FuncionarioSolicitante)
                .WithMany()
                .HasForeignKey(s => s.FuncionarioSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudCesantia>()
                .HasOne(t => t.Funcionario)
                .WithMany()
                .HasForeignKey(s => s.FuncionarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitudCesantia>()
                .HasOne(t => t.MotivoSolicitudCesantia)
                .WithMany()
                .HasForeignKey(s => s.MotivoSolicitudCesantiaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sustituto>()
                .HasOne(c => c.CargoASustituir)
                .WithMany()
                .HasForeignKey(s => s.CargoASustituirId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sustituto>()
                .HasOne(c => c.CargoSustituto)
                .WithMany()
                .HasForeignKey(s => s.CargoSustitutoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sustituto>()
                .HasOne(c => c.CentroOperativoASutituir)
                .WithMany()
                .HasForeignKey(s => s.CentroOperativoASutituirId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sustituto>()
                .HasOne(c => c.CentroOperativoSustituto)
                .WithMany()
                .HasForeignKey(s => s.CentroOperativoSustitutoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoLiquidacion>()
                .HasOne(c => c.ConceptoNominaAgrupador)
                .WithMany()
                .HasForeignKey(s => s.ConceptoNominaAgrupadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoLiquidacionComprobante>()
                .HasOne(c => c.CentroCosto)
                .WithMany()
                .HasForeignKey(s => s.CentroCostoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoLiquidacionComprobante>()
                .HasOne(c => c.CuentaContable)
                .WithMany()
                .HasForeignKey(s => s.CuentaContableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoLiquidacionComprobante>()
                .HasOne(c => c.TipoLiquidacion)
                .WithMany()
                .HasForeignKey(s => s.TipoLiquidacionId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region RelacionMuchosAmuchos

            modelBuilder.Entity<NominaFuncionario>()
                   .HasOne(c => c.Nomina)
                   .WithMany()
                   .HasForeignKey(s => s.NominaId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NominaFuncionario>()
                   .HasOne(c => c.Funcionario)
                   .WithMany()
                   .HasForeignKey(s => s.FuncionarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConceptoBase>()
                   .HasOne(c => c.ConceptoNominaAgrupador)
                   .WithMany()
                   .HasForeignKey(s => s.ConceptoNominaAgrupadorId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CargoPresupuesto>()
                     .HasOne(c => c.Cargo)
                     .WithMany()
                     .HasForeignKey(s => s.CargoId)
                     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConceptoBase>()
                   .HasOne(c => c.ConceptoNomina)
                   .WithMany()
                   .HasForeignKey(s => s.ConceptoNominaId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoriaNovedad>()
                   .HasOne(c => c.ConceptoNomina)
                   .WithMany()
                   .HasForeignKey(s => s.ConceptoNominaId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DependenciaJerarquia>()
                   .HasOne(c => c.DependenciaHijo)
                   .WithMany(b => b.SoyHijoDe)
                   .HasForeignKey(s => s.DependenciaHijoId);

            modelBuilder.Entity<DependenciaJerarquia>()
                   .HasOne(c => c.DependenciaPadre)
                   .WithMany(b => b.SoyPadreDe)
                   .HasForeignKey(s => s.DependenciaPadreId);

            modelBuilder.Entity<CargoReporta>()
                   .HasOne(c => c.CargoDependencia)
                   .WithMany()
                   .HasForeignKey(s => s.CargoDependenciaId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CargoReporta>()
                   .HasOne(c => c.CargoDependenciaReporta)
                   .WithMany(c => c.MeReportan)
                   .HasForeignKey(s => s.CargoDependenciaReportaId)
                   .OnDelete(DeleteBehavior.Restrict);

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


            #endregion

            #region CheckConstraint EstadoRegistro
            modelBuilder.Entity<ActividadEconomica>().HasCheckConstraint("CK_ActividadEconomica_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Administradora>().HasCheckConstraint("CK_Administradora_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<AusentismoFuncionario>().HasCheckConstraint("CK_AusentismoFuncionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Beneficio>().HasCheckConstraint("CK_Beneficio_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<BeneficioAdjunto>().HasCheckConstraint("CK_BeneficioAdjunto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<BeneficioSubperiodo>().HasCheckConstraint("CK_BeneficioSubperiodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Candidato>().HasCheckConstraint("CK_Candidato_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Calendario>().HasCheckConstraint("CK_Calendario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Cargo>().HasCheckConstraint("CK_Cargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CargoCentroCosto>().HasCheckConstraint("CK_CargoCentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CargoDependencia>().HasCheckConstraint("CK_CargoDependencia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CargoGrado>().HasCheckConstraint("CK_CargoGrado_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CargoReporta>().HasCheckConstraint("CK_CargoReporta_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CategoriaParametro>().HasCheckConstraint("CK_CategoriaParametro_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CentroCosto>().HasCheckConstraint("CK_CentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CentroOperativo>().HasCheckConstraint("CK_CentroOperativo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CentroTrabajo>().HasCheckConstraint("CK_CentroTrabajo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ClaseAusentismo>().HasCheckConstraint("CK_ClaseAusentismo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ClaseAportanteTipoAportante>().HasCheckConstraint("CK_ClaseAportanteTipoAportante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ClaseLibretaMilitar>().HasCheckConstraint("CK_ClaseLibretaMilitar_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ConceptoBase>().HasCheckConstraint("CK_ConceptoBase_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ConceptoNomina>().HasCheckConstraint("CK_ConceptoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ConceptoNominaCuentaContable>().HasCheckConstraint("CK_ConceptoNominaCuentaContable_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Contrato>().HasCheckConstraint("CK_Contrato_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ContratoCentroTrabajo>().HasCheckConstraint("CK_ContratoCentroTrabajo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ContratoAdministradora>().HasCheckConstraint("CK_ContratoAdministradora_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ContratoOtroSi>().HasCheckConstraint("CK_ContratoOtroSi_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CuentaContable>().HasCheckConstraint("CK_CuentaContable_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CuentaBancaria>().HasCheckConstraint("CK_CuentaBancaria_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<DeduccionRetefuente>().HasCheckConstraint("CK_DeduccionRetefuente_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Dependencia>().HasCheckConstraint("CK_Dependencia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<DependenciaJerarquia>().HasCheckConstraint("CK_DependenciaJerarquia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<DiagnosticoCie>().HasCheckConstraint("CK_DiagnosticoCie_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<DivisionPoliticaNivel1>().HasCheckConstraint("CK_DivisionPoliticaNivel1_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<DivisionPoliticaNivel2>().HasCheckConstraint("CK_DivisionPoliticaNivel2_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<DocumentoFuncionario>().HasCheckConstraint("CK_DocumentoFuncionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Embargo>().HasCheckConstraint("CK_Embargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<EmbargoConceptoNomina>().HasCheckConstraint("CK_EmbargoConceptoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<EmbargoSubperiodo>().HasCheckConstraint("CK_EmbargoSubperiodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<EntidadFinanciera>().HasCheckConstraint("CK_EntidadFinanciera_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<EstadoCivil>().HasCheckConstraint("CK_EstadoCivil_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ExperienciaLaboral>().HasCheckConstraint("CK_ExperienciaLaboral_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<FormaPago>().HasCheckConstraint("CK_FormaPago_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Funcionario>().HasCheckConstraint("CK_Funcionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<FuncionarioEstudio>().HasCheckConstraint("CK_FuncionarioEstudio_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<FuncionNomina>().HasCheckConstraint("CK_FuncionNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<FuncionVariable>().HasCheckConstraint("CK_FuncionVariable_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<GrupoNomina>().HasCheckConstraint("CK_GrupoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<HojaDeVida>().HasCheckConstraint("CK_HojaDeVida_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<HojaDeVidaDocumento>().HasCheckConstraint("CK_HojaDeVidaDocumento_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<HojaDeVidaEstudio>().HasCheckConstraint("CK_HojaDeVidaEstudio_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<HojaDeVidaExperienciaLaboral>().HasCheckConstraint("CK_HojaDeVidaExperienciaLaboral_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Idioma>().HasCheckConstraint("CK_Idioma_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<InformacionBasica>().HasCheckConstraint("CK_InformacionBasica_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<InformacionFamiliar>().HasCheckConstraint("CK_InformacionFamiliar_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<JornadaLaboral>().HasCheckConstraint("CK_JornadaLaboral_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<JornadaLaboralDia>().HasCheckConstraint("CK_JornadaLaboralDia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Juzgado>().HasCheckConstraint("CK_Juzgado_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Libranza>().HasCheckConstraint("CK_Libranza_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<LibranzaSubperiodo>().HasCheckConstraint("CK_LibranzaSubperiodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<LicenciaConduccion>().HasCheckConstraint("CK_LicenciaConduccion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NivelCargo>().HasCheckConstraint("CK_NivelCargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NivelEducativo>().HasCheckConstraint("CK_NivelEducativo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<MotivoVacante>().HasCheckConstraint("CK_MotivoVacante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NomenclaturaDian>().HasCheckConstraint("CK_NomenclaturaDian_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NaturalezaJuridica>().HasCheckConstraint("CK_NaturalezaJuridica_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Nomina>().HasCheckConstraint("CK_Nomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NominaDetalle>().HasCheckConstraint("CK_NominaDetalle_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NominaFuenteNovedad>().HasCheckConstraint("CK_NominaFuenteNovedad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NominaFuncionario>().HasCheckConstraint("CK_NominaFuncionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Notificacion>().HasCheckConstraint("CK_Notificacion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NotificacionDestinatario>().HasCheckConstraint("CK_NotificacionDestinatario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NotificacionDestinatarioLog>().HasCheckConstraint("CK_NotificacionDestinatarioLog_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Novedad>().HasCheckConstraint("CK_Novedad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NovedadSubperiodo>().HasCheckConstraint("CK_NovedadSubperiodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Ocupacion>().HasCheckConstraint("CK_Ocupacion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<OperadorPago>().HasCheckConstraint("CK_OperadorPago_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Pais>().HasCheckConstraint("CK_Pais_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ParametroGeneral>().HasCheckConstraint("CK_ParametroGeneral_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Parentesco>().HasCheckConstraint("CK_Parentesco_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<PeriodoContable>().HasCheckConstraint("CK_PeriodoContable_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Profesion>().HasCheckConstraint("CK_Profesion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ProrrogaAusentismo>().HasCheckConstraint("CK_ProrrogaAusentismo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<RepresentanteEmpresa>().HasCheckConstraint("CK_RepresentanteEmpresa_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<RequisicionPersonal>().HasCheckConstraint("CK_RequisicionPersonal_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Sexo>().HasCheckConstraint("CK_Sexo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<SolicitudPermiso>().HasCheckConstraint("CK_SolicitudPermiso_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<SoporteSolicitudPermiso>().HasCheckConstraint("CK_SoporteSolicitudPermiso_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<SubPeriodo>().HasCheckConstraint("CK_SubPeriodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TareaProgramada>().HasCheckConstraint("CK_TareaProgramada_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TareaProgramadaLog>().HasCheckConstraint("CK_TareasProgramadasLogs_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoAdministradora>().HasCheckConstraint("CK_TipoAdministradora_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoAccion>().HasCheckConstraint("CK_TipoAccion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoAusentismo>().HasCheckConstraint("CK_TipoAusentismo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoAusentismoConceptoNomina>().HasCheckConstraint("CK_TipoAusentismoConceptoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoBeneficio>().HasCheckConstraint("CK_TipoBeneficio_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoBeneficioRequisito>().HasCheckConstraint("CK_TipoBeneficioRequisito_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoContrato>().HasCheckConstraint("CK_TipoContrato_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoContribuyente>().HasCheckConstraint("CK_TipoContribuyente_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoCotizanteTipoPlanilla>().HasCheckConstraint("CK_TipoCotizanteTipoPlanilla_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoCuenta>().HasCheckConstraint("CK_TipoCuenta_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoDocumento>().HasCheckConstraint("CK_TipoDocumento_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoEmbargo>().HasCheckConstraint("CK_TipoEmbargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoEmbargoConceptoNomina>().HasCheckConstraint("CK_TipoEmbargoConceptoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoLiquidacion>().HasCheckConstraint("CK_TipoLiquidacion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoLiquidacionConcepto>().HasCheckConstraint("CK_TipoLiquidacionConcepto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoLiquidacionEstado>().HasCheckConstraint("CK_TipoLiquidacionEstado_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoLiquidacionModulo>().HasCheckConstraint("CK_TipoLiquidacionModulo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoMoneda>().HasCheckConstraint("CK_TipoMoneda_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoPeriodo>().HasCheckConstraint("CK_TipoPeriodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoSangre>().HasCheckConstraint("CK_TipoSangre_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoSoporte>().HasCheckConstraint("CK_TipoSoporte_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoVivienda>().HasCheckConstraint("CK_TipoVivienda_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Tercero>().HasCheckConstraint("CK_Tercero_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<VariableNomina>().HasCheckConstraint("CK_VariableNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<Notificacion>().HasCheckConstraint("CK_Notificacion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<NotificacionDestinatario>().HasCheckConstraint("CK_NotificacionDestinatario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<NotificacionDestinatarioLog>().HasCheckConstraint("CK_NotificacionDestinatarioLog_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<NominaDetalle>().HasCheckConstraint("CK_NominaDetalle_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<NominaFuenteNovedad>().HasCheckConstraint("CK_NominaFuenteNovedad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<SolicitudPermiso>().HasCheckConstraint("CK_SolicitudPermiso_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<SoporteSolicitudPermiso>().HasCheckConstraint("CK_SoporteSolicitudPermiso_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<LibroVacacion>().HasCheckConstraint("CK_LibroVacaciones_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<SolicitudVacacion>().HasCheckConstraint("CK_SolicitudVacaciones_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<NotificacionPlantilla>().HasCheckConstraint("CK_NotificacionPlantilla_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<SolicitudVacacionesInterrupcion>().HasCheckConstraint("CK_SolicitudVacacionesInterrupcion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<Sustituto>().HasCheckConstraint("CK_Sustituto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<CargoGrado>().HasCheckConstraint("CK_CargoGrado_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<CargoGrupo>().HasCheckConstraint("CK_CargoGrupo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<Grupo>().HasCheckConstraint("CK_Grupo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<AplicacionExterna>().HasCheckConstraint("CK_AplicacionExterna_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<AplicacionExternaCargo>().HasCheckConstraint("CK_AplicacionExternaCargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<Actividad>().HasCheckConstraint("CK_Actividad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ActividadCentroCosto>().HasCheckConstraint("CK_ActividadCentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<SolicitudCesantia>().HasCheckConstraint("CK_SolicitudCesantia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<MotivoSolicitudCesantia>().HasCheckConstraint("CK_MotivoSolicitudCesantia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<RangoUvt>().HasCheckConstraint("CK_RangoUvt_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<AplicacionExternaCargoDependiente>().HasCheckConstraint("CK_AplicacionExternaCargoDependiente_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<TipoHoraExtra>().HasCheckConstraint("CK_TipoHoraExtra_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CategoriaNovedad>().HasCheckConstraint("CK_CategoriaNovedad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<HoraExtra>().HasCheckConstraint("CK_HoraExtra_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CargoPresupuesto>().HasCheckConstraint("CK_CargoPresupuesto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<AnnoVigencia>().HasCheckConstraint("CK_AnnoVigencia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<TipoPersona>().HasCheckConstraint("CK_TipoPersonas_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoPlanilla>().HasCheckConstraint("CK_TipoPlanilla_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoCotizante>().HasCheckConstraint("CK_TipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoAportante>().HasCheckConstraint("CK_TipoAportante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ClaseAportante>().HasCheckConstraint("CK_ClaseAportante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<SubtipoCotizante>().HasCheckConstraint("CK_SubtipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoCotizanteSubtipoCotizante>().HasCheckConstraint("CK_TipoCotizanteSubtipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoAportanteTipoPlanilla>().HasCheckConstraint("CK_TipoAportanteTipoPlanilla_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoAportanteTipoCotizante>().HasCheckConstraint("CK_TipoAportanteTipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ClaseAportanteTipoCotizante>().HasCheckConstraint("CK_ClaseAportanteTipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoGastoViaje>().HasCheckConstraint("CK_TipoGastoViaje_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<GastoViaje>().HasCheckConstraint("CK_GastoViaje_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<CausalTerminacion>().HasCheckConstraint("CK_CausalTerminacion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<_EnlaceExterno>().HasCheckConstraint("CK_EnlaceExterno_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<_MenuFavorito>().HasCheckConstraint("CK_MenuFavorito_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ActividadFuncionario>().HasCheckConstraint("CK_ActividadFuncionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<FuncionarioCentroCosto>().HasCheckConstraint("CK_FuncionarioCentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<ActividadFuncionarioCentroCosto>().HasCheckConstraint("CK_ActividadFuncionarioCentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            modelBuilder.Entity<ConceptoNominaTipoAdministradora>().HasCheckConstraint("CK_ConceptoNominaTipoAdministradora_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoLiquidacionComprobante>().HasCheckConstraint("CK_TipoLiquidacionComprobante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NominaContabilidad>().HasCheckConstraint("CK_NominaContabilidad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<NominaCentroCosto>().HasCheckConstraint("CK_NominaCentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
            modelBuilder.Entity<TipoOtroSi>().HasCheckConstraint("CK_TipoOtroSi_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            #endregion

            #region CheckConstraint
            modelBuilder.Entity<Candidato>().HasCheckConstraint("CK_Candidato_Estado", "([Estado]='Postulado' OR [Estado]='Descartado' OR [Estado]='Competente' OR [Estado]='Elegible' OR [Estado]='NoApto' OR [Estado]='Seleccionado' OR [Estado]='Reprobado' OR [Estado]='Anulado')");

            modelBuilder.Entity<Contrato>().HasCheckConstraint("CK_Contrato_Estado", "([Estado]='Vigente' OR [Estado]='SinIniciar' OR [Estado]='Terminado' OR [Estado]='Cancelado' OR [Estado]='PendientePorLiquidar')");

            modelBuilder.Entity<ConceptoNominaElementoFormula>().HasCheckConstraint("CK_ConceptoNominaElementoFormula_Tipo", "([Tipo]='Concepto' OR [Tipo]='Funcion')");

            modelBuilder.Entity<ExperienciaLaboral>().HasCheckConstraint("CK_ExperienciaLaboral_Estado", "([Estado]='Pendiente' OR [Estado]='Rechazado' OR [Estado]='Validado')");

            modelBuilder.Entity<Funcionario>().HasCheckConstraint("CK_Funcionario_Estado", "([Estado]='Activo' OR [Estado]='EnVacaciones' OR [Estado]='Retirado' OR [Estado]='Seleccionado' OR [Estado]='Incapacitado')");

            modelBuilder.Entity<FuncionarioEstudio>().HasCheckConstraint("CK_FuncionarioEstudio_Estado", "([Estado]='Pendiente' OR [Estado]='Rechazado' OR [Estado]='Validado')");

            modelBuilder.Entity<InformacionFamiliar>().HasCheckConstraint("CK_InformacionFamiliar_Estado", "([Estado]='Pendiente' OR [Estado]='Rechazado' OR [Estado]='Validado')");

            modelBuilder.Entity<Notificacion>().HasCheckConstraint("CK_Notificacion_Tipo", "([Tipo]='MobilePush' OR [Tipo]='Email')");

            modelBuilder.Entity<NotificacionDestinatario>().HasCheckConstraint("CK_NotificacionDestinatario_Estado", "([Estado]='Pendiente' OR [Estado]='Enviado' OR [Estado]='Fallido')");

            modelBuilder.Entity<NotificacionDestinatarioLog>().HasCheckConstraint("CK_NotificacionDestinatarioLog_Estado", "([Estado]='Pendiente' OR [Estado]='Enviado' OR [Estado]='Fallido')");

            modelBuilder.Entity<Nomina>().HasCheckConstraint("CK_Nomina_Estado", "([Estado]='Inicializada' OR [Estado]='Modificada' OR [Estado]='EnLiquidacion' OR [Estado]='Liquidada' OR [Estado]='Aprobada' OR [Estado]='Aplicada')");

            modelBuilder.Entity<Libranza>().HasCheckConstraint("CK_Libranza_Estado", "([Estado]='Vigente' OR [Estado]='Terminada' OR [Estado]='Pendiente' OR [Estado]='Anulada')");

            modelBuilder.Entity<Embargo>().HasCheckConstraint("CK_Embargo_Estado", "([Estado]='Vigente' OR [Estado]='Pendiente' OR [Estado]='Liquidado' OR [Estado]='Anulado' OR [Estado]='Terminado')");

            modelBuilder.Entity<Beneficio>().HasCheckConstraint("CK_Beneficio_Estado", "([Estado] = 'EnTramite' OR[Estado] = 'Aprobada' OR[Estado] = 'Autorizada' OR[Estado] = 'Otorgada' OR[Estado] = 'EnReembolso' OR[Estado] = 'EnCondonacion' OR[Estado] = 'Condonada' OR[Estado] = 'Rechazada' OR[Estado] = 'Cancelada' OR[Estado] = 'Finalizada')");

            modelBuilder.Entity<SolicitudPermiso>().HasCheckConstraint("CK_SolicitudPermiso_Estado", "([Estado]='Aprobada' OR [Estado]='Autorizada' OR [Estado]='Cancelada' OR [Estado]='Rechazada' OR [Estado]='Solicitada')");

            modelBuilder.Entity<LibroVacacion>().HasCheckConstraint("CK_LibroVacaciones_Tipo", "([Tipo]='Anticipado' OR [Tipo]='Causado')");

            modelBuilder.Entity<SolicitudVacacion>().HasCheckConstraint("CK_SolicitudVacaciones_Estado", "([Estado]='Aprobada' OR [Estado]='Autorizada' OR [Estado]='Cancelada' OR [Estado]='EnCurso' OR [Estado]='Interrumpida' OR [Estado]='Rechazada' OR [Estado]='Solicitada'  OR [Estado]='Terminada' OR [Estado]='Anulada')");

            modelBuilder.Entity<VariableNomina>().HasCheckConstraint("CK_VariableNomina_TipoDato", "([TipoDato] = 'BIGINT' OR [TipoDato] = 'NUMERIC' OR [TipoDato] = 'BIT' OR [TipoDato] = 'SMALLINT' OR [TipoDato] = 'DECIMAL' OR [TipoDato] = 'SMALLMONEY' OR [TipoDato] = 'INT' OR [TipoDato] = 'TINYINT' OR [TipoDato] = 'MONEY' OR [TipoDato] = 'FLOAT' OR [TipoDato] = 'REAL' OR [TipoDato] = 'DATE' OR [TipoDato] = 'DATETIMEOFFSET' OR [TipoDato] = 'DATETIME2' OR [TipoDato] = 'SMALLDATETIME' OR [TipoDato] = 'DATETIME' OR [TipoDato] = 'TIME' OR [TipoDato] = 'CHAR' OR [TipoDato] = 'VARCHAR' OR [TipoDato] = 'TEXT' OR [TipoDato] = 'NCHAR' OR [TipoDato] = 'NVARCHAR' OR [TipoDato] = 'NTEXT' OR [TipoDato] = 'BINARY' OR [TipoDato] = 'VARBINARY' OR [TipoDato] = 'IMAGE' OR [TipoDato] = 'CURSOR' OR [TipoDato] = 'ROWVERSION' OR [TipoDato] = 'HIERARCHYID' OR [TipoDato] = 'UNIQUEIDENTIFIER' OR [TipoDato] = 'SQL_VARIANT' OR [TipoDato] = 'XML' OR [TipoDato] = 'TABLE')");

            modelBuilder.Entity<TareaProgramadaLog>().HasCheckConstraint("CK_TareaProgramadaLog_Estado", "([Estado]='Exitoso' OR [Estado]='Fallido')");

            modelBuilder.Entity<Cargo>().HasCheckConstraint("CK_Cargo_Clase", "([Clase]='CentroOperativo' OR [Clase]='Nacional')");

            modelBuilder.Entity<AplicacionExterna>().HasCheckConstraint("CK_TipoAplicacionExterna_Revisa", "([Revisa]='JefeInmediato' OR [Revisa]='Otro' OR [Revisa]='NoAplica')");

            modelBuilder.Entity<AplicacionExterna>().HasCheckConstraint("CK_TipoAplicacionExterna_Aprueba", "([Aprueba]='JefeInmediato' OR [Aprueba]='Otro' OR [Aprueba]='NoAplica')");

            modelBuilder.Entity<AplicacionExterna>().HasCheckConstraint("CK_TipoAplicacionExterna_Autoriza", "([Autoriza]='JefeInmediato' OR [Autoriza]='Otro' OR [Autoriza]='NoAplica')");

            modelBuilder.Entity<AplicacionExternaCargo>().HasCheckConstraint("CK_AplicacionExternaCargo_Tipo", "([Tipo]='Aprobacion' OR [Tipo]='Autorizacion' OR [Tipo]='Revision')");

            modelBuilder.Entity<RequisicionPersonal>().HasCheckConstraint("CK_RequisicionPersonal_Estado", "([estado]='Anulada' OR [estado]='Aprobada' OR [estado]='Autorizada' OR [estado]='Cancelada' OR [estado]='Cubierta' OR [estado]='Rechazada' OR [estado]='Revisada' OR [estado]='Solicitada')");

            modelBuilder.Entity<RequisicionPersonal>().HasCheckConstraint("CK_RequisicionPersonal_TipoReclutamiento", "([TipoReclutamiento]='Externa' OR [TipoReclutamiento]='Interna' OR [TipoReclutamiento]='Mixta')");

            modelBuilder.Entity<SolicitudCesantia>().HasCheckConstraint("CK_SolicitudCesantia_Estado", "([Estado]='EnTramite' OR [Estado]='Aprobada' OR [Estado]='Rechazada' OR [Estado]='Cancelada' OR [Estado]='Finalizada' )");

            modelBuilder.Entity<TipoHoraExtra>().HasCheckConstraint("CK_TipoHoraExtra_Tipo", "([Tipo]='RecargoNocturno' OR [Tipo]='HoraExtraDiurna' OR [Tipo]='HoraExtraNocturna' OR [Tipo]='HoraExtraFestivaDominicalDiurna' OR [Tipo]='HoraExtraFestivaDominicalNocturna'  OR [Tipo]='RecargoNocturnoDominicalFestivo' OR [Tipo]='DominicalFestivoCompensado' OR [Tipo]='DominicalFestivoNoCompensado')");

            modelBuilder.Entity<CategoriaNovedad>().HasCheckConstraint("CK_CategoriaNovedad_Modulo", "([Modulo]='Libranzas' OR [Modulo]='Embargos' OR [Modulo]='Ausentismos' OR [Modulo]='Beneficios' OR [Modulo]='HorasExtra'  OR [Modulo]='GastosViaje' OR [Modulo]='OtrasNovedades' )");

            modelBuilder.Entity<CategoriaNovedad>().HasCheckConstraint("CK_CategoriaNovedad_Clase", "([Clase]='Fija' OR [Clase]='Eventual' )");

            modelBuilder.Entity<Novedad>().HasCheckConstraint("CK_Novedad_Estado", "([Estado]='EnCurso' OR [Estado]='Pendiente' OR [Estado]='Liquidada' OR [Estado]='Anulada' OR [Estado]='Cancelada')");

            modelBuilder.Entity<CategoriaNovedad>().HasCheckConstraint("CK_CategoriaNovedad_UbicacionTercero", "([UbicacionTercero]='EntidadFinanciera' OR [UbicacionTercero]='Administradora' OR [UbicacionTercero]='OtrosTerceros' )");

            modelBuilder.Entity<HoraExtra>().HasCheckConstraint("CK_HoraExtra_FormaRegistro", "([FormaRegistro]='Manual' OR [FormaRegistro]='Automatico' )");

            modelBuilder.Entity<HoraExtra>().HasCheckConstraint("CK_HoraExtra_Estado", "([Estado]='Pendiente' OR [Estado]='Aplicada')");

            modelBuilder.Entity<HojaDeVidaEstudio>().HasCheckConstraint("CK_HojaDeVidaEstudio_EstadoEstudio", "([EstadoEstudio]='EnCurso' OR [EstadoEstudio]='Aplazado' OR [EstadoEstudio]='Abandonado' OR [EstadoEstudio]='Culminado')");

            modelBuilder.Entity<NominaDetalle>().HasCheckConstraint("CK_NominaDetalle_UnidadMedida", "([UnidadMedida]='Horas' OR [UnidadMedida]='Dias' OR [UnidadMedida]='Unidad' OR [UnidadMedida]='Porcentaje')");

            modelBuilder.Entity<AnnoVigencia>().HasCheckConstraint("CK_AnnoVigencia_Estado", "([Estado]='Vigente' OR [Estado]='Cerrado')");

            modelBuilder.Entity<TipoContrato>().HasCheckConstraint("CK_TipoContrato_Clase", "([Clase]='NoIntegral' OR [Clase]='Integral' OR [Clase]='Aprendizaje' OR [Clase]='Practicante' OR [Clase]='Variable')");

            modelBuilder.Entity<TipoDocumento>().HasCheckConstraint("CK_TipoDocumento_Formato", "([Formato]='Alfanumerico' OR [Formato]='Numerico')");

            modelBuilder.Entity<TipoGastoViaje>().HasCheckConstraint("CK_TipoGastoViaje_Estado", "([Tipo]='ViaticosHospedaje' OR [Tipo]='ViaticosAlimentacion' OR [Tipo]='FaltanteViaticos' OR [Tipo]='PagoAnticipoGV' OR [Tipo]='BaseViaticosAlimentacion' OR [Tipo]='BaseViaticosRetefuente' OR [Tipo]='BaseRetefuenteGV' OR [Tipo]='BaseViaticosHospedaje')");

            modelBuilder.Entity<TipoLiquidacion>().HasCheckConstraint("CK_TipoLiquidacion_OperacionTotal", "([OperacionTotal]='TotalDevengosMenosTotalDeducciones' or [OperacionTotal]='TotalCalculos' or [OperacionTotal]='SoloCalculosSinAgrupar' or [OperacionTotal]='TotalDeducciones')");

            modelBuilder.Entity<TipoLiquidacionModulo>().HasCheckConstraint("CK_TipoLiquidacionModulo_Modulo", "([Modulo]='Libranzas' or [Modulo]='Embargos' or [Modulo]='Ausentismos' or [Modulo]='Beneficios' or [Modulo]='HorasExtra' or [Modulo]='HorasExtra' or [Modulo]='GastosViaje' or [Modulo]='OtrasNovedades' or [Modulo]='Vacaciones' or [Modulo]='AnticipoCesantia')");

            modelBuilder.Entity<GastoViaje>().HasCheckConstraint("CK_GastoViaje_Estado", "([Estado]='Pendiente' OR [Estado]='Aplicada')");

            modelBuilder.Entity<ActividadFuncionario>().HasCheckConstraint("CK_ActividadFuncionario_Estado", "([Estado]='Pendiente' OR [Estado]='Aplicado' )");

            modelBuilder.Entity<FuncionarioCentroCosto>().HasCheckConstraint("CK_FuncionarioCentroCosto_Estado", "([Estado]='Pendiente' OR [Estado]='Aplicado' )");

            modelBuilder.Entity<TipoLiquidacionComprobante>().HasCheckConstraint("CK_TipoLiquidacionComprobante_TipoComprobante", "([TipoComprobante]='Contabilizacion' OR [TipoComprobante]='Transferencia' OR [TipoComprobante]='Reversion' )");
            modelBuilder.Entity<TipoLiquidacionComprobante>().HasCheckConstraint("CK_TipoLiquidacionComprobante_Naturaleza", "([Naturaleza]='Debito' OR [Naturaleza]='Credito' )");

            #endregion

            #region Enum
            modelBuilder.Entity<ActividadFuncionario>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoActividadFuncionario>());

            modelBuilder.Entity<FuncionarioCentroCosto>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoFuncionarioCentroCosto>());

            modelBuilder.Entity<FuncionarioCentroCosto>()
                .Property(t => t.FormaRegistro)
                .HasConversion(new EnumToStringConverter<FormaRegistroFuncionarioCentroCosto>());

            modelBuilder.Entity<AplicacionExterna>()
                .Property(t => t.Aprueba)
                .HasConversion(new EnumToStringConverter<TipoAplicacionExterna>());

            modelBuilder.Entity<AplicacionExterna>()
                .Property(t => t.Autoriza)
                .HasConversion(new EnumToStringConverter<TipoAplicacionExterna>());

            modelBuilder.Entity<AplicacionExternaCargo>()
                .Property(t => t.Tipo)
                .HasConversion(new EnumToStringConverter<TipoAplicacionExternaCargo>());

            modelBuilder.Entity<AnnoVigencia>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoAnnoVigencia>());

            modelBuilder.Entity<Candidato>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoCandidato>());

            modelBuilder.Entity<Beneficio>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoBeneficiosCorportativos>());

            modelBuilder.Entity<Beneficio>()
                .Property(t => t.OpcionAuxilioEducativo)
                .HasConversion(new EnumToStringConverter<OpcionAuxilioEducativo>());

            modelBuilder.Entity<Contrato>()
                .Property(t => t.ProcedimientoRetencion)
                .HasConversion(new EnumToStringConverter<ProcedimientoRetenciones>());

            modelBuilder.Entity<Contrato>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoContrato>());

            modelBuilder.Entity<ConceptoNomina>()
                .Property(t => t.OrigenCentroCosto)
                .HasConversion(new EnumToStringConverter<OrigenCentroCostoNomina>());

            modelBuilder.Entity<ConceptoNominaElementoFormula>()
                .Property(t => t.Tipo)
                .HasConversion(new EnumToStringConverter<TipoElementoFormula>());

            modelBuilder.Entity<Cargo>()
                .Property(t => t.Clase)
                .HasConversion(new EnumToStringConverter<ClaseCargo>());

            modelBuilder.Entity<CategoriaNovedad>()
                .Property(t => t.Modulo)
                .HasConversion(new EnumToStringConverter<ModuloSistema>());

            modelBuilder.Entity<CategoriaNovedad>()
                .Property(t => t.Clase)
                .HasConversion(new EnumToStringConverter<ClaseCategoriaNovedad>());

            modelBuilder.Entity<CategoriaNovedad>()
                .Property(t => t.UbicacionTercero)
                .HasConversion(new EnumToStringConverter<UbicacionTerceroCategoriaNovedad>());

            modelBuilder.Entity<ConceptoNomina>()
                .Property(t => t.OrigenTercero)
                .HasConversion(new EnumToStringConverter<OrigenTerceroNomina>());

            modelBuilder.Entity<ConceptoNomina>()
                .Property(t => t.ClaseConceptoNomina)
                .HasConversion(new EnumToStringConverter<ClaseConceptoNomina>());

            modelBuilder.Entity<ConceptoNomina>()
                .Property(t => t.TipoConceptoNomina)
                .HasConversion(new EnumToStringConverter<TipoConceptoNomina>());

            modelBuilder.Entity<ConceptoNomina>()
                .Property(t => t.UnidadMedida)
                .HasConversion(new EnumToStringConverter<UnidadMedida>());

            modelBuilder.Entity<CuentaContable>()
                .Property(t => t.Naturaleza)
                .HasConversion(new EnumToStringConverter<NaturalezaContable>());

            //modelBuilder.Entity<Dashboard>()
            //   .Property(t => t.Tipo)
            //   .HasConversion(new EnumToStringConverter<EnumTipoElemento>());
            //modelBuilder.Entity<Dashboard>()
            //   .Property(t => t.Subtipo)
            //   .HasConversion(new EnumToStringConverter<EnumSubtipoElemento>());

            modelBuilder.Entity<ExperienciaLaboral>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoInformacionFuncionario>());

            modelBuilder.Entity<JornadaLaboralDia>()
                .Property(t => t.Dia)
                .HasConversion(new EnumToStringConverter<DiaSemana>());

            modelBuilder.Entity<FuncionarioEstudio>()
               .Property(t => t.Estado)
               .HasConversion(new EnumToStringConverter<EstadoInformacionFuncionario>());

            modelBuilder
                .Entity<FuncionarioEstudio>()
                .Property(p => p.EstadoEstudio)
                .HasConversion(new EnumToStringConverter<EstadoEstudio>());

            modelBuilder
                .Entity<GastoViaje>()
                .Property(p => p.Estado)
                .HasConversion(new EnumToStringConverter<EstadoGastoViaje>());

            modelBuilder
                .Entity<HojaDeVidaEstudio>()
                .Property(p => p.EstadoEstudio)
                .HasConversion(new EnumToStringConverter<EstadoEstudio>());

            modelBuilder
                .Entity<HoraExtra>()
                .Property(p => p.Estado)
                .HasConversion(new EnumToStringConverter<EstadoHoraExtra>());

            modelBuilder
                .Entity<HoraExtra>()
                .Property(p => p.FormaRegistro)
                .HasConversion(new EnumToStringConverter<FormaRegistroHoraExtra>());

            modelBuilder.Entity<InformacionFamiliar>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoInformacionFuncionario>());

            modelBuilder.Entity<Notificacion>()
                .Property(t => t.Tipo)
                .HasConversion(new EnumToStringConverter<TipoNotificacion>());

            modelBuilder.Entity<NotificacionDestinatario>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoNotificacion>());

            modelBuilder.Entity<NotificacionDestinatarioLog>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoNotificacion>());

            modelBuilder.Entity<TipoDocumento>()
                .Property(t => t.Formato)
                .HasConversion(new EnumToStringConverter<FormatoValidacion>());

            modelBuilder
                .Entity<Parentesco>()
                .Property(p => p.Grado)
                .HasConversion(new EnumToStringConverter<GradoParentescos>());

            modelBuilder
               .Entity<TareaProgramadaLog>()
               .Property(t => t.Estado)
               .HasConversion(new EnumToStringConverter<EstadoTareaProgramada>());

            modelBuilder
               .Entity<TipoContribuyente>()
               .Property(t => t.Persona)
               .HasConversion(new EnumToStringConverter<TipoPersonaContribuyente>());

            modelBuilder.Entity<TipoAusentismo>()
             .Property(t => t.UnidadTiempo)
             .HasConversion(new EnumToStringConverter<UnidadTiempo>());

            modelBuilder
                .Entity<Parentesco>()
                .Property(p => p.Tipo)
                .HasConversion(new EnumToStringConverter<TipoParentescos>());

            modelBuilder
                .Entity<ParametroGeneral>()
                .Property(p => p.Tipo)
                .HasConversion(new EnumToStringConverter<TipoDato>());

            modelBuilder.Entity<FuncionNomina>()
               .Property(t => t.TipoFuncion)
               .HasConversion(new EnumToStringConverter<TipoFuncion>());

            modelBuilder.Entity<VariableNomina>()
              .Property(t => t.TipoDato)
              .HasConversion(new EnumToStringConverter<TipoDatoSql>());

            modelBuilder.Entity<VariableNomina>()
              .Property(t => t.TipoVariable)
              .HasConversion(new EnumToStringConverter<TipoVariable>());

            modelBuilder.Entity<TipoLiquidacionEstado>()
               .Property(t => t.EstadoFuncionario)
               .HasConversion(new EnumToStringConverter<EstadoFuncionario>());

            modelBuilder.Entity<TipoLiquidacionEstado>()
               .Property(t => t.EstadoContrato)
               .HasConversion(new EnumToStringConverter<EstadoContrato>());

            modelBuilder.Entity<Libranza>()
               .Property(t => t.Estado)
               .HasConversion(new EnumToStringConverter<EstadoLibranza>());

            modelBuilder.Entity<Embargo>()
               .Property(t => t.Estado)
               .HasConversion(new EnumToStringConverter<EstadoEmbargo>());

            modelBuilder.Entity<LibroVacacion>()
               .Property(t => t.Tipo)
               .HasConversion(new EnumToStringConverter<TipoLibroVacaciones>());

            modelBuilder.Entity<Novedad>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoNovedad>());
            modelBuilder.Entity<Novedad>()
                .Property(t => t.Unidad)
                .HasConversion(new EnumToStringConverter<UnidadMedida>());

            modelBuilder.Entity<RequisicionPersonal>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoRequisicionPersonal>());

            modelBuilder.Entity<RequisicionPersonal>()
                .Property(t => t.TipoReclutamiento)
                .HasConversion(new EnumToStringConverter<TipoReclutamiento>());

            modelBuilder.Entity<SolicitudCesantia>()
                .Property(t => t.Estado)
                .HasConversion(new EnumToStringConverter<EstadoCesantia>());

            modelBuilder.Entity<SolicitudVacacion>()
               .Property(t => t.Estado)
               .HasConversion(new EnumToStringConverter<EstadoSolicitudVacaciones>());

            modelBuilder.Entity<SolicitudPermiso>()
               .Property(t => t.Estado)
               .HasConversion(new EnumToStringConverter<EstadoSolicitudPermiso>());

            modelBuilder.Entity<TipoContrato>()
               .Property(t => t.Clase)
               .HasConversion(new EnumToStringConverter<ClaseTipoContrato>());

            modelBuilder.Entity<TipoLiquidacion>()
               .Property(t => t.Proceso)
               .HasConversion(new EnumToStringConverter<TipoLiquidacionProceso>());

            modelBuilder.Entity<TipoLiquidacion>()
               .Property(t => t.OperacionTotal)
               .HasConversion(new EnumToStringConverter<OperacionTotalTipoLiqidacion>());

            modelBuilder.Entity<TipoLiquidacionModulo>()
               .Property(t => t.Modulo)
               .HasConversion(new EnumToStringConverter<ModuloSistema>());

            modelBuilder.Entity<TipoHoraExtra>()
               .Property(t => t.Tipo)
               .HasConversion(new EnumToStringConverter<Infraestructura.Enumerador.TipoHoraExtra>());

            modelBuilder.Entity<TipoGastoViaje>()
               .Property(t => t.Tipo)
               .HasConversion(new EnumToStringConverter<TipoGastosViaje>());

            modelBuilder.Entity<TipoLiquidacionComprobante>()
               .Property(t => t.TipoComprobante)
               .HasConversion(new EnumToStringConverter<TipoComprobante>());

            modelBuilder.Entity<TipoLiquidacionComprobante>()
               .Property(t => t.Naturaleza)
               .HasConversion(new EnumToStringConverter<NaturalezaContable>());

            #endregion

            #region Query Filter
            modelBuilder.Entity<Actividad>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ActividadCentroCosto>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ActividadFuncionarioCentroCosto>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ActividadEconomica>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ActividadFuncionario>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Administradora>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<AplicacionExterna>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<AplicacionExternaCargo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<AplicacionExternaCargoDependiente>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<BeneficioAdjunto>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<AusentismoFuncionario>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<AnnoVigencia>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Candidato>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Calendario>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CargoDependencia>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CargoGrado>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CargoGrupo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CargoPresupuesto>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CargoReporta>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Cargo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CargoCentroCosto>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CategoriaNovedad>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CategoriaParametro>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CausalTerminacion>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CentroCosto>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CentroOperativo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CentroTrabajo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ContratoCentroTrabajo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ClaseAusentismo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ClaseAportanteTipoAportante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ClaseLibretaMilitar>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ConceptoNominaCuentaContable>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ConceptoBase>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ConceptoNomina>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ConceptoNominaTipoAdministradora>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ConceptoNominaElementoFormula>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ContratoAdministradora>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ContratoOtroSi>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Contrato>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CuentaContable>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<CuentaBancaria>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<DeduccionRetefuente>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<DependenciaJerarquia>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Dependencia>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<DiagnosticoCie>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<DivisionPoliticaNivel1>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<DivisionPoliticaNivel2>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<DocumentoFuncionario>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Embargo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<EmbargoConceptoNomina>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<EmbargoSubperiodo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<EntidadFinanciera>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<EstadoCivil>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ExperienciaLaboral>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<FormaPago>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<FuncionarioEstudio>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Funcionario>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<FuncionarioCentroCosto>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<FuncionNomina>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<FuncionVariable>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<GastoViaje>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Grupo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<GrupoNomina>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<HojaDeVida>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<HojaDeVidaDocumento>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<HojaDeVidaEstudio>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<HojaDeVidaExperienciaLaboral>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Idioma>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<InformacionBasica>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<InformacionFamiliar>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<HoraExtra>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<JornadaLaboralDia>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<JornadaLaboral>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Juzgado>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Libranza>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<LibranzaSubperiodo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<LibroVacacion>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<MotivoVacante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NivelCargo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NivelEducativo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NomenclaturaDian>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NaturalezaJuridica>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Nomina>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NominaContabilidad>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ConceptoNominaCuentaContable>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NominaFuenteNovedad>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NominaDetalle>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NominaFuncionario>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Notificacion>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NotificacionDestinatario>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NotificacionDestinatarioLog>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Novedad>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<NovedadSubperiodo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Ocupacion>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<OperadorPago>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Pais>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ParametroGeneral>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Parentesco>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<PeriodoContable>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Profesion>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ProrrogaAusentismo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<RangoUvt>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<RepresentanteEmpresa>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<RequisicionPersonal>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<SubPeriodo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<VariableNomina>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Sustituto>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<SolicitudCesantia>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<SolicitudVacacion>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<SolicitudVacacionesInterrupcion>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<SoporteSolicitudPermiso>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<Tercero>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoAccion>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoAusentismoConceptoNomina>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoAusentismo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoBeneficio>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoBeneficioRequisito>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoContrato>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoContribuyente>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoCotizanteTipoPlanilla>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoDocumento>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoEmbargo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoEmbargoConceptoNomina>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoGastoViaje>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoHoraExtra>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoLiquidacionConcepto>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoLiquidacion>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoLiquidacionEstado>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoLiquidacionModulo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoMoneda>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoOtroSi>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoPeriodo>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoVivienda>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<VariableNomina>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoPersona>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoPlanilla>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoCotizante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoAportante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ClaseAportante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<SubtipoCotizante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoCotizanteSubtipoCotizante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoAportanteTipoPlanilla>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoAportanteTipoCotizante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<TipoLiquidacionComprobante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<ClaseAportanteTipoCotizante>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<_EnlaceExterno>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);
            modelBuilder.Entity<_MenuFavorito>().HasQueryFilter(m => m.EstadoRegistro != EstadoRegistro.Eliminado);

            #endregion

            #region Datos de control
            modelBuilder.Entity<Actividad>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ActividadCentroCosto>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ActividadFuncionarioCentroCosto>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ActividadFuncionario>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<AnnoVigencia>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ActividadEconomica>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Administradora>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<AplicacionExterna>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<AplicacionExternaCargo>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<AplicacionExternaCargoDependiente>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<AusentismoFuncionario>()
                 .Property(t => t.EstadoRegistro)
                 .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Candidato>()
                 .Property(t => t.EstadoRegistro)
                 .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Beneficio>()
                 .Property(t => t.EstadoRegistro)
                 .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<BeneficioSubperiodo>()
                 .Property(t => t.EstadoRegistro)
                 .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CausalTerminacion>()
                 .Property(t => t.EstadoRegistro)
                 .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<BeneficioAdjunto>()
                 .Property(t => t.EstadoRegistro)
                 .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CargoDependencia>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CargoGrado>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CargoGrupo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CargoPresupuesto>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Cargo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CategoriaNovedad>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CategoriaParametro>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ConceptoBase>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ConceptoNomina>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ConceptoNominaTipoAdministradora>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ConceptoNominaElementoFormula>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoAusentismoConceptoNomina>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ConceptoNominaCuentaContable>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Contrato>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ContratoCentroTrabajo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ContratoAdministradora>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CentroTrabajo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CentroCosto>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CentroOperativo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<CuentaBancaria>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<DeduccionRetefuente>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Dependencia>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<DependenciaJerarquia>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<DiagnosticoCie>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<DivisionPoliticaNivel1>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<DivisionPoliticaNivel2>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<DocumentoFuncionario>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Embargo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<EmbargoConceptoNomina>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<EmbargoSubperiodo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<EntidadFinanciera>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<EstadoCivil>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ExperienciaLaboral>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<FormaPago>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Funcionario>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<FuncionarioCentroCosto>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<FuncionarioEstudio>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<FuncionNomina>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<GastoViaje>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Grupo>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<GrupoNomina>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<HojaDeVida>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<HojaDeVidaDocumento>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<HojaDeVidaEstudio>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<HojaDeVidaExperienciaLaboral>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<HoraExtra>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Idioma>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<InformacionBasica>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());


            modelBuilder.Entity<InformacionFamiliar>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Juzgado>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<JornadaLaboral>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Libranza>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<LibranzaSubperiodo>()
            .Property(t => t.EstadoRegistro)
            .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<LibroVacacion>()
            .Property(t => t.EstadoRegistro)
            .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NaturalezaJuridica>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NivelCargo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NivelEducativo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NomenclaturaDian>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Nomina>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NominaContabilidad>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NominaCentroCosto>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NominaFuenteNovedad>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NominaDetalle>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NominaFuncionario>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Notificacion>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NotificacionDestinatario>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NotificacionDestinatarioLog>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Novedad>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NovedadSubperiodo>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<PeriodoContable>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Ocupacion>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<OperadorPago>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Pais>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<NotificacionPlantilla>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ParametroGeneral>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Parentesco>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<RangoUvt>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<RequisicionPersonal>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Profesion>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ContratoOtroSi>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<SolicitudPermiso>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<SolicitudVacacion>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<SolicitudVacacionesInterrupcion>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<SoporteSolicitudPermiso>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Sustituto>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TareaProgramada>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TareaProgramadaLog>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Tercero>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoAccion>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoAdministradora>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoAusentismo>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoContrato>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoContribuyente>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoCotizanteTipoPlanilla>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoCuenta>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoDocumento>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoEmbargo>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoEmbargoConceptoNomina>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoGastoViaje>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoVivienda>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoMoneda>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoOtroSi>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoPeriodo>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<SolicitudCesantia>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<SubPeriodo>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Sexo>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoHoraExtra>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoLiquidacionConcepto>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoLiquidacion>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoLiquidacionEstado>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoLiquidacionModulo>()
              .Property(t => t.EstadoRegistro)
              .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<VariableNomina>()
               .Property(t => t.EstadoRegistro)
               .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<FuncionVariable>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<Tercero>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoBeneficio>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoBeneficioRequisito>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoPersona>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoPlanilla>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoCotizante>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoAportante>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ClaseAportante>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ClaseAportanteTipoAportante>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<SubtipoCotizante>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoCotizanteSubtipoCotizante>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoAportanteTipoPlanilla>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoAportanteTipoCotizante>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<ClaseAportanteTipoCotizante>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<_EnlaceExterno>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<_MenuFavorito>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());

            modelBuilder.Entity<TipoLiquidacionComprobante>()
                .Property(t => t.EstadoRegistro)
                .HasConversion(new EnumToStringConverter<EstadoRegistro>());


            #endregion

            base.OnModelCreating(modelBuilder);

        }
        #endregion

    }

}
