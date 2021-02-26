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

namespace ApiV3.Dominio.RequisicionPersonales.Crear
{
    public class CrearRequisicionPersonalRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
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
                TipoContrato tipoContratoID = null;
                MotivoVacante motivoVacanteId = null;
                int cargoSolicitadoId = 0;
                var hoy = DateTime.Today;

                #region Info Solicitante

                if (CargoDependenciaSolicitanteId != null)
                {
                    var cargoDependenciaSolicitanteId = contexto.CargoDependencias.FirstOrDefault(x => x.Id == CargoDependenciaSolicitanteId);
                    if (cargoDependenciaSolicitanteId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("cargo"),
                           new[] { "CargoDependenciaSolicitanteId" }));
                    }
                    else
                    {
                        var cargoSolicitante = contexto.Cargos.FirstOrDefault(x => x.Id == cargoDependenciaSolicitanteId.CargoId);
                        if (cargoSolicitante.Clase == ClaseCargo.CentroOperativo && CentroOperativoSolicitanteId == null)
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                           new[] { "CentroOperativoSolicitanteId" }));
                        }
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
                        else if (motivoVacanteId.RequiereNombreAQuienReemplaza && FuncionarioAQuienReemplazaId != null)
                        {
                            RequisicionPersonal remplaza = contexto
                                                            .RequisicionPersonales
                                                            .FirstOrDefault(x => x.FuncionarioAQuienReemplazaId == FuncionarioAQuienReemplazaId &&
                                                            x.Estado != EstadoRequisicionPersonal.Anulada &&
                                                            x.Estado != EstadoRequisicionPersonal.Cancelada &&
                                                            x.Estado != EstadoRequisicionPersonal.Rechazada &&
                                                            x.Estado != EstadoRequisicionPersonal.Cubierta);

                            if (remplaza != null)
                            {
                                errores.Add(new ValidationResult("Ya existe una requisición para reemplazar este funcionario.",
                                new[] { "FuncionarioAQuienReemplazaId" }));
                            }


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
                        var cargoSolicitado = contexto.Cargos.FirstOrDefault(x => x.Id == cargoDependenciaSolicitadoId.CargoId);
                        if (cargoSolicitado.Clase == ClaseCargo.CentroOperativo && CentroOperativoSolicitadoId == null)
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                           new[] { "CentroOperativoSolicitadoID" }));
                        }

                        cargoSolicitadoId = cargoDependenciaSolicitadoId.CargoId;
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
                if (Cantidad != null && cargoSolicitadoId > 0)
                {
                    string mensaje = $"La compañía no cuenta con el presupuesto para contratar {Cantidad} funcionarios para este cargo." +
                            " Por favor acércate al departamento de gestión humana.";
                    int? presupuesto = contexto.CargoPresupuestos
                                             .Where(x => x.CargoId == cargoSolicitadoId && x.AnnoVigencia.Anno == hoy.Year)
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
                                                        x.Estado != EstadoRequisicionPersonal.Anulada)
                                            .Sum(x => x.Cantidad);

                        int? contratados = contexto.Contratos
                                            .Where(x => x.CargoDependenciaId == CargoDependenciaSolicitadoId &&
                                                        x.Estado != EstadoContrato.Cancelado &&
                                                        x.Estado != EstadoContrato.Terminado &&
                                                        x.Estado != EstadoContrato.PendientePorLiquidar)
                                            .Count();

                        int? otroSiQueNoestenEnContratados = (from otroSi in contexto.ContratoOtroSis
                                                              join contrato in contexto.Contratos on otroSi.ContratoId equals contrato.Id
                                                              where otroSi.CargoDependenciaId == CargoDependenciaSolicitadoId &&
                                                              contrato.CargoDependenciaId != CargoDependenciaSolicitadoId &&
                                                              otroSi.FechaAplicacion <= hoy &&
                                                              otroSi.FechaFinalizacion >= hoy
                                                              select new { id = otroSi.Id }).Count();


                        if (Cantidad > (presupuesto - (enRequisicion + contratados + otroSiQueNoestenEnContratados)))
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
                    if (FechaInicio < hoy)
                    {
                        errores.Add(new ValidationResult("La fecha de inicio de la contratación no puede ser menor a la fecha actual.",
                            new[] { "FechaInicio" }));
                    }
                    if (FechaInicio == hoy)
                    {
                        errores.Add(new ValidationResult("La fecha de inicio de la contratación no puede ser igual a la fecha actual.",
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
