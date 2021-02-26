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

namespace ApiV3.Dominio.ContratoAdministradoras.Comandos.Actualizar
{
    public class ActualizarContratoAdministradoraRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoAdministradoraId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? AdministradoraId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicio { get; set; }

        [MaxLength(500, ErrorMessage = ConstantesErrores.Maximo + "500.")]
        public string Observacion { get; set; }

        //Se carga segun funcionario
        public int? ContratoId { get; set; }

        #endregion

        #region Validaciones Manuales
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
                        FuncionarioDatoActual funcionarioDatoActual = contexto.FuncionarioDatoActuales.FirstOrDefault(x => x.Id == funcionario.Id);
                        if (funcionarioDatoActual == null && funcionarioDatoActual.ContratoId == null)
                        {
                            errores.Add(new ValidationResult("El funcionario que intentas ingresar no tiene un contrato activo, por favor revise.",
                            new[] { "FuncionarioId" }));
                            return errores;
                        }
                        else
                        {
                            ContratoId = funcionarioDatoActual.ContratoId;
                        }
                    }
                }
                #endregion

                #region TipoAdministradoraId
                TipoAdministradora tipoAdministradora = contexto.TipoAdministradoras.FirstOrDefault(x => x.Id == TipoAdministradoraId);
                if (tipoAdministradora == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo administradora"),
                       new[] { "TipoAdministradoraId" }));
                    return errores;
                }
                #endregion

                #region AdministradoraId
                Administradora administradora = contexto.Administradoras.FirstOrDefault(x => x.Id == AdministradoraId);
                if (administradora == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("administradora"),
                       new[] { "AdministradoraId" }));
                }
                else if (administradora.TipoAdministradoraId != TipoAdministradoraId)
                {
                    errores.Add(new ValidationResult("La administradora no corresponde al tipo administrador ingresado.",
                       new[] { "AdministradoraId" }));
                }
                else
                {
                    ContratoAdministradora contratoAdministradora = contexto.ContratoAdministradoras.FirstOrDefault
                        (x => x.Id != Id &&
                        x.ContratoId == ContratoId &&
                        x.AdministradoraId == AdministradoraId &&
                        x.EstadoRegistro == EstadoRegistro.Activo &&
                        x.FechaFin == null);
                    if (contratoAdministradora != null)
                    {
                        errores.Add(new ValidationResult("La administradora que intentas ingresar se encuentra actualmente registrada para el funcionario.",
                      new[] { "AdministradoraId" }));
                    }
                }
                #endregion

                #region FechaInicio
                var hoy = DateTime.Today;
                var fechaInicio = (DateTime)FechaInicio;
                int liquidacion = (from nomina in contexto.Nominas
                                   join tipoLiquidacion in contexto.TipoLiquidaciones on nomina.TipoLiquidacionId equals tipoLiquidacion.Id
                                   where tipoLiquidacion.Codigo.Equals("PILA")
                                   && fechaInicio >= nomina.FechaInicio
                                   && fechaInicio <= nomina.FechaFinal
                                   && nomina.Estado == EstadoNomina.Aplicada
                                   && nomina.EstadoRegistro == EstadoRegistro.Activo
                                   select new { id = nomina.Id }).Count();

                if ((fechaInicio.Month < hoy.Month && fechaInicio.Year <= hoy.Year) || (hoy.Year == fechaInicio.Year && hoy.Month == fechaInicio.Month && liquidacion > 0))
                {
                    errores.Add(new ValidationResult("No es posible realizarse el cambio en la fecha de aplicación ingresada para el funcionario.",
                       new[] { "FechaInicio" }));
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
