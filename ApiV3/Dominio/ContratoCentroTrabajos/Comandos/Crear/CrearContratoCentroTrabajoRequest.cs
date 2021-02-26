using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.ContratoCentroTrabajos.Comandos.Crear
{
    public class CrearContratoCentroTrabajoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
       
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroTrabajoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFinal { get; set; }

        [MaxLength(500, ErrorMessage = ConstantesErrores.Maximo + "500.")]
        public string Observacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? ContratoId { get; set; }

        #endregion

        #region Validacion Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region FuncionarioId, carga contaroId
                var funcionario = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                if (funcionario == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionario"),
                       new[] { "FuncionarioId" }));
                }
                else
                {
                    if (funcionario.Estado == EstadoFuncionario.Retirado || funcionario.Estado == EstadoFuncionario.Seleccionado)
                    {
                        errores.Add(new ValidationResult("El funcionario que intentas ingresar no se encuentra activo, por favor revise.",
                       new[] { "FuncionarioId" }));
                        return errores;
                    }
                    else
                    {
                        var contrato = contexto.Contratos.FirstOrDefault(x => x.FuncionarioId == funcionario.Id &&
                                                                            x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                            x.Estado == EstadoContrato.Vigente
                                                                            );
                        if (contrato == null)
                        {
                            errores.Add(new ValidationResult("El funcionario que intentas ingresar no tiene un contrato activo, por favor revise.",
                            new[] { "FuncionarioId" }));
                            return errores;
                        }
                        
                    }
                }
                #endregion

                #region CentroTrabajoId
                CentroTrabajo centroTrabajo = contexto.CentroTrabajos.FirstOrDefault(x => x.Id == CentroTrabajoId);
                if (centroTrabajo == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("centro de trabajo"),
                       new[] { "CentroTrabajoId" }));
                }
                else
                {
                    ContratoCentroTrabajo contratoCentroTrabajo = contexto.ContratoCentroTrabajos.FirstOrDefault
                        (x => x.ContratoId == ContratoId &&
                        x.CentroTrabajoId == CentroTrabajoId &&
                        x.EstadoRegistro == EstadoRegistro.Activo &&
                        x.FechaFin == null);
                    if (contratoCentroTrabajo != null)
                    {
                        errores.Add(new ValidationResult("El centro de trabajo que intentas ingresar se encuentra actualmente registrado para el funcionario.",
                           new[] { "CentroTrabajoId" }));
                    }
                }
                #endregion

                #region existe contrato
                var existeContrato = contexto.Contratos.FirstOrDefault(x => x.Id == ContratoId);
                if (existeContrato == null)
                {
                    errores.Add(new ValidationResult("No existe contrato", new[] { "Id" }));
                }
                else
                {
                    #region funcionario activo
                    var funcionarioActivo = contexto.Funcionarios.FirstOrDefault(x => x.Id == existeContrato.FuncionarioId);
                    if (funcionarioActivo.Estado != EstadoFuncionario.Activo)
                    {
                        errores.Add(new ValidationResult("El funcionario que intentas ingresar no se encuentra activo, por favor revise.",
                           new[] { "funcionarioId" }));
                    }
                    #endregion
                }
                #endregion

                #region FechaInicio
                // Valida que la pila tenga el mismo mes y año 
                FechaFinal = (DateTime)FechaInicio;
                var validarNominaTipoPila = contexto.Nominas.Include(x => x.TipoLiquidacion)
                                                            .Where(x => x.TipoLiquidacion.Codigo == "PILA")
                                                            .FirstOrDefault(x => x.FechaInicio.Year == FechaFinal.Value.Year &&
                                                                               x.FechaInicio.Month == FechaFinal.Value.Month);
                if (validarNominaTipoPila != null)
                {
                    errores.Add(new ValidationResult("No es posible realizarse el cambio en la fecha de aplicación ingresada para el funcionario.",
                           new[] { "snackError" }));
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
