using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.RequisicionPersonales.Actualizar
{
    public class ActualizarRequisicionPersonalRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }
        #region Info Solicitante
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoDependenciaSolicitanteId { get; set; }

        public int? CentroOperativoSolicitanteId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioSolicitanteId { get; set; }
        #endregion

        #region Info Cargo Solicitado
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoDependenciaSolicitadoId { get; set; }

        public int? CentroOperativoSolicitadoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? DivisionPoliticaNivel2Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1, 999, ErrorMessage = ConstantesErrores.Rango + "1 - 999.")]
        public int? Cantidad { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoContratoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CentroCostoId { get; set; }
        #endregion

        #region TiempoContratacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }
        #endregion

        #region  Info vacante
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? MotivoVacanteId { get; set; }

        public int? FuncionarioAQuienReemplazaId { get; set; }
        #endregion

        #region Pefil y Competencias del cargo
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(1000, ErrorMessage = ConstantesErrores.Maximo + "1000.")]
        public string PerfilCargo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(1000, ErrorMessage = ConstantesErrores.Maximo + "1000.")]
        public string CompetenciaCargo { get; set; }

        public TipoReclutamiento? TipoReclutamiento { get; set; }

        public double? Salario { get; set; }

        public bool? SalarioPortalReclutamiento { get; set; }

        public bool? PerfilPortalReclutamiento { get; set; }

        public bool? CompetenciaPortalReclutamiento { get; set; }


        [MaxLength(1000, ErrorMessage = ConstantesErrores.Maximo + "1000.")]
        public string Observacion { get; set; }

        [MaxLength(500, ErrorMessage = ConstantesErrores.Maximo + "500.")]
        public string Justificacion { get; set; }

        #endregion
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                RequisicionPersonal existeId = null;
                MotivoVacante motivoVacanteId = null;
                int cargoId = 0;
                var hoy = DateTime.Today;
                #region Id
                if (Id != null)
                {
                    existeId = contexto.RequisicionPersonales.FirstOrDefault(x => x.Id == Id);
                    if (existeId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("requisicion personal"),
                           new[] { "snack" }));
                        return errores;
                    }
                }
                #endregion

                #region Info Solicitante

                if (CargoDependenciaSolicitanteId != null)
                {
                    var cargoDependenciaSolicitanteId = contexto.CargoDependencias.FirstOrDefault(x => x.Id == CargoDependenciaSolicitanteId);
                    if (cargoDependenciaSolicitanteId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("cargo"),
                           new[] { "CargoDependenciaSolicitanteId" }));
                    }
                }

                if (CentroOperativoSolicitanteId != null)
                {
                    var centroOperativoSolicitanteId = contexto.CentroOperativos.FirstOrDefault(x => x.Id == CentroOperativoSolicitanteId);
                    if (centroOperativoSolicitanteId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("centro operativo"),
                           new[] { "CentroOperativoSolicitanteId" }));
                    }
                }

                if (FuncionarioSolicitanteId != null)
                {
                    var funcionarioSolicitanteId = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioSolicitanteId);
                    if (funcionarioSolicitanteId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionario"),
                           new[] { "FuncionarioSolicitanteId" }));
                    }
                }
                #endregion

                #region  Info vacante
                if (MotivoVacanteId != null)
                {
                    motivoVacanteId = contexto.MotivoVacantes.FirstOrDefault(x => x.Id == MotivoVacanteId);
                    if (motivoVacanteId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("motivo vacante"),
                           new[] { "MotivoVacanteId" }));
                        return errores;
                    }
                    else
                    {
                        if (motivoVacanteId.RequiereNombreAQuienReemplaza && FuncionarioAQuienReemplazaId == null)
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                           new[] { "FuncionarioAQuienReemplazaId" }));
                        }
                    }
                }
                if (FuncionarioAQuienReemplazaId != null)
                {
                    var funcionarioAQuienReemplazaId = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioAQuienReemplazaId);
                    if (funcionarioAQuienReemplazaId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionario"),
                           new[] { "FuncionarioAQuienReemplazaId" }));
                    }
                }
                #endregion

                #region Info Cargo Solicitado
                TipoContrato tipoContratoID = null;
                if (CargoDependenciaSolicitadoId != null)
                {
                    var cargoDependenciaSolicitadoId = contexto.CargoDependencias.FirstOrDefault(x => x.Id == CargoDependenciaSolicitadoId);
                    if (cargoDependenciaSolicitadoId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("cargo"),
                           new[] { "CargoDependenciaSolicitadoId" }));
                    }
                    else
                    {
                        cargoId = cargoDependenciaSolicitadoId.CargoId;
                    }
                }
                if (CentroOperativoSolicitadoId != null)
                {
                    var centroOperativoSolicitadoID = contexto.CentroOperativos.FirstOrDefault(x => x.Id == CentroOperativoSolicitadoId);
                    if (centroOperativoSolicitadoID == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("centro operativo"),
                           new[] { "CentroOperativoSolicitadoID" }));
                    }
                }
                if (DivisionPoliticaNivel2Id != null)
                {
                    var divisionPoliticaNivel2Id = contexto.DivisionPoliticaNiveles2.FirstOrDefault(x => x.Id == DivisionPoliticaNivel2Id);
                    if (divisionPoliticaNivel2Id == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("municipio"),
                           new[] { "DivisionPoliticaNivel2Id" }));
                    }
                }
                if (TipoContratoId != null)
                {
                    tipoContratoID = contexto.TipoContratos.FirstOrDefault(x => x.Id == TipoContratoId);
                    if (tipoContratoID == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo contrato"),
                           new[] { "TipoContratoID" }));
                    }
                }
                if (CentroCostoId != null)
                {
                    var centroCostoId = contexto.CentroCostos.FirstOrDefault(x => x.Id == CentroCostoId);
                    if (centroCostoId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("centro costo"),
                           new[] { "CentroCostoId" }));
                    }
                }
                if (Cantidad != null && cargoId > 0)
                {
                    string mensaje = $"La compañía no cuenta con el presupuesto para contratar {Cantidad} funcionarios para este cargo." +
                            " Por favor acércate al departamento de gestión humana.";
                    int? presupuesto = contexto.CargoPresupuestos
                                             .Where(x => x.CargoId == cargoId && x.AnnoVigencia.Anno == hoy.Year)
                                             .Sum(x => x.Cantidad);

                    if (motivoVacanteId.RequiereNombreAQuienReemplaza)
                    {
                        presupuesto++;
                    }


                    if (presupuesto != null && Cantidad <= presupuesto)
                    {
                        int? enRequisicion = contexto.RequisicionPersonales
                                            .Where(x => x.CargoDependenciaSolicitadoId == CargoDependenciaSolicitadoId &&
                                                   x.Estado != EstadoRequisicionPersonal.Cancelada &&
                                                   x.Estado != EstadoRequisicionPersonal.Rechazada &&
                                                   x.Estado != EstadoRequisicionPersonal.Anulada &&
                                                   x.Id != Id)
                                            .Sum(x => x.Cantidad);

                        int? contratados = contexto.Contratos
                                            .Where(x => x.CargoDependenciaId == CargoDependenciaSolicitadoId &&
                                                   x.Estado != EstadoContrato.Cancelado &&
                                                   x.Estado != EstadoContrato.Terminado &&
                                                   x.Estado != EstadoContrato.PendientePorLiquidar)
                                            .Count();

                        if (Cantidad > (presupuesto + enRequisicion + contratados))
                        {
                            errores.Add(new ValidationResult(mensaje, new[] { "snack" }));
                        }

                    }
                    else
                    {
                        errores.Add(new ValidationResult(mensaje, new[] { "snack" }));
                    }
                }
                #endregion

                #region TiempoContratacion
                if (tipoContratoID != null && !tipoContratoID.TerminoIndefinido && FechaFin == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "FechaFin" }));
                }
                if (FechaInicio != null)
                {
                    if (FechaInicio < existeId.FechaCreacion)
                    {
                        errores.Add(new ValidationResult("La fecha de inicio de la contratación no puede ser menor a la fecha creación.",
                            new[] { "FechaInicio" }));
                    }
                    if (FechaInicio == existeId.FechaCreacion)
                    {
                        errores.Add(new ValidationResult("La fecha de inicio de la contratación no puede ser igual a la fecha creación.",
                            new[] { "FechaInicio" }));
                    }
                    if (FechaFin != null && FechaInicio == FechaFin)
                    {
                        errores.Add(new ValidationResult("La fecha final no puede ser igual a la fecha de inicio de la contratación.",
                            new[] { "FechaFin" }));
                    }
                    if (FechaFin != null && FechaFin < FechaInicio)
                    {
                        errores.Add(new ValidationResult("La fecha final no puede ser menor a la fecha de inicio de la contratación.",
                            new[] { "FechaFin" }));
                    }

                }
                #endregion

                #region Salario
                if (Salario != null && Salario > 999999999)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.Rango + "$ 1 - $999.999.999",
                           new[] { "Salario" }));
                }
                #endregion

            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
        #endregion
    }
}
