using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.ContratoOtroSis.Comandos.Crear
{
    public class CrearContratoOtroSiRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$",
            ErrorMessage = ConstantesErrores.Numerico)]
        public int? ContratoId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$",
            ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoContratoId { get; set; }


        public DateTime? FechaFinalizacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaAplicacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$",
            ErrorMessage = ConstantesErrores.Numerico)]
        public int? CargoDependenciaId { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$",
            ErrorMessage = ConstantesErrores.Numerico)]
        public int? NumeroOtroSi { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 1, maximum: 9999999999999, ErrorMessage = ConstantesErrores.Rango + "1 - 9999999999999.")]
        public double? Sueldo { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$",
            ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroOperativoId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$",
            ErrorMessage = ConstantesErrores.Numerico)]
        public int? DivisionPoliticaNivel2Id { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(300, ErrorMessage = ConstantesErrores.Maximo + "300.")]
        public string Observaciones { get; set; }

        //valor calculado, si cambia la fecha de finalziacion
        public bool? Prorroga { get; set; }

        public int? numProrogas { get; set; }

        public bool Confirmacion { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                DateTime hoy = DateTime.Today;
                dynamic contratoActual;     // Cargar los datos actules Contrato u OtroSi
                bool HayCambios = false;    // Si hay un cambio en un campo se actualiza en true
                int duracionMaxima = 0;     // Segun tipo del contrato
                int prorrogaMaxima = 0;  // Segun tipo del contrato

                #region ContratoId
                var contratoId = contexto.Contratos.Find(ContratoId);
                if (contratoId == null)
                {
                    errores.Add(new ValidationResult("No existe el ContratoId.", new[] { "ContratoId" }));
                    return errores;
                }
                else
                {
                    if (contratoId.Estado == EstadoContrato.Terminado
                        || contratoId.Estado == EstadoContrato.SinIniciar
                        || contratoId.Estado == EstadoContrato.Cancelado)
                    {
                        errores.Add(new ValidationResult("No se pueden crear otrosí para un contrato en el estado que actualmente tiene.",
                            new[] { "ContratoId" }));
                        return errores;
                    };
                    //Se realzia validacion para cargar datos de otro si, o Contrato
                    #region Validar contrato o ultimo otro si


                    var maxOtrosi = contexto.ContratoOtroSis.Where(x => x.ContratoId == ContratoId &&
                                                                        x.FechaAplicacion <= hoy &&
                                                                        x.EstadoRegistro == EstadoRegistro.Activo)
                                                    .OrderByDescending(c => c.FechaAplicacion)
                                                    .FirstOrDefault();
                    
                    numProrogas = contexto.ContratoOtroSis
                                              .Where(x => x.ContratoId == ContratoId &&
                                                          x.EstadoRegistro == EstadoRegistro.Activo
                                              ).Count();


                    //retorna valores otroSi
                    if (maxOtrosi != null)
                    {
                        var camposOtroSi = new
                        {
                            maxOtrosi.ContratoId,
                            maxOtrosi.TipoContratoId,
                            maxOtrosi.FechaFinalizacion,
                            maxOtrosi.CargoDependenciaId,
                            FechaInicio = maxOtrosi.FechaAplicacion,
                            maxOtrosi.Sueldo,
                            maxOtrosi.CentroOperativoId,
                            maxOtrosi.DivisionPoliticaNivel2Id,
                            NumProrogas = numProrogas
                        };
                        contratoActual = camposOtroSi;
                    }
                    //retorna Valores contrato
                    else
                    {
                        var contrato = contexto.Contratos.FirstOrDefault(x => x.Id == ContratoId);
                        var camposContrato = new
                        {
                            ContratoId = contrato.Id,
                            contrato.TipoContratoId,
                            contrato.FechaFinalizacion,
                            contrato.CargoDependenciaId,
                            contrato.FechaInicio,
                            contrato.Sueldo,
                            contrato.CentroOperativoId,
                            contrato.DivisionPoliticaNivel2Id,
                            NumProrogas = numProrogas
                        };
                        contratoActual = camposContrato;
                    }
                }
                #endregion


                #endregion

                #region TipoContratoId
                //Valida si Existe un tipo de contrato cone el Id
                var tipoContratoId = contexto.TipoContratos.Find(TipoContratoId);
                if (tipoContratoId == null)
                {
                    errores.Add(new ValidationResult("No existe un tipo contrato con los datos ingresados.",
                        new[] { "TipoContratoId" }));
                }
                else
                {
                    duracionMaxima = tipoContratoId.DuracionMaxima;
                    prorrogaMaxima = tipoContratoId.CantidadProrrogas;

                    //Valida si hay cambios
                    if (contratoActual.TipoContratoId != TipoContratoId)
                    {
                        HayCambios = true;
                    }
                }
                #endregion

                #region FechaFinalizacion
                if (tipoContratoId.TerminoIndefinido == false)
                {
                    if (FechaFinalizacion == null)
                    {
                        errores.Add(new ValidationResult("Requerido", new[] { "FechaFinalizacion" }));
                    }
                    else
                    {
                        if (contratoActual.FechaFinalizacion != FechaFinalizacion)
                        {
                            HayCambios = true;
                            Prorroga = true;

                            if (FechaFinalizacion > contratoActual.FechaFinalizacion.AddDays(duracionMaxima))
                            {
                                errores.Add(new ValidationResult("La fecha de finalización supera el tiempo  permitido para el tipo de contrato.",
                                    new[] { "FechaFinalizacion" }));
                            }
                            else if (FechaFinalizacion < contratoActual.FechaFinalizacion)
                            {
                                errores.Add(new ValidationResult("La fecha de finalización del otro si no puede ser inferior a la fecha de finalización actual.",
                                    new[] { "FechaFinalizacion" }));
                            }
                            else if (contratoActual.NumProrogas > prorrogaMaxima)
                            {
                                if (FechaFinalizacion < contratoActual.FechaFinalizacion.AddDays(365))
                                {
                                    errores.Add(new ValidationResult("Se ha excedido la cantidad de prórrogas para el tipo de contrato del funcionario.",
                                        new[] { "FechaFinalizacion" }));
                                }
                            }
                        }
                    }

                }
                #endregion

                #region NumeroOtroSi
                NumeroOtroSi = contexto.ContratoOtroSis.Where(x => x.ContratoId == ContratoId).Count() + 1;
                #endregion

                #region FechaAplicacion
                if (FechaAplicacion != null)
                {
                    var existeFecha = contexto.ContratoOtroSis
                                              .FirstOrDefault(x => x.FechaAplicacion == FechaAplicacion &&
                                                                   x.ContratoId == ContratoId &&
                                                                   x.EstadoRegistro == EstadoRegistro.Activo);
                    if (existeFecha != null && Confirmacion == false)
                    {
                        errores.Add(new ValidationResult("La fecha de aplicación que intentas ingresar ya existe en un otrosí registrado para el funcionario, ¿Desea continuar?.",
                                   new[] { "DialogConfirmacion" }));
                    }
                    else
                    {
                        if (FechaAplicacion < contratoActual.FechaInicio)
                        {
                            errores.Add(new ValidationResult("La fecha de aplicación que intentas ingresar no se encuentra entre el período de inicio y finalización del contrato u otrosí.",
                                    new[] { "FechaAplicacion" }));
                        }
                        else if (tipoContratoId.TerminoIndefinido == false && FechaAplicacion > contratoActual.FechaFinalizacion)
                        {
                            errores.Add(new ValidationResult("La fecha de aplicación que intentas ingresar no se encuentra entre el período de inicio y finalización del contrato u otrosí.",
                                    new[] { "FechaAplicacion" }));
                        }
                    }
                }
                #endregion

                #region CargoDependenciaId
                //Valida si Existe un tipo de Cargo cone el Id
                var VcargoDependenciaId = contexto.CargoDependencias.Find(CargoDependenciaId);
                if (VcargoDependenciaId == null)
                {
                    errores.Add(new ValidationResult("No existe un Cargo-Dependencia con los datos ingresados.",
                        new[] { "CargoDependenciaId" }));
                }
                else
                {
                    if (contratoActual.CargoDependenciaId != CargoDependenciaId)
                    {
                        HayCambios = true;
                    }
                }

                #endregion

                #region Sueldo
                if (contratoActual.Sueldo != Sueldo)
                {
                    HayCambios = true;
                }
                if (contratoActual.Sueldo > Sueldo)
                {
                    errores.Add(new ValidationResult("El sueldo que ingresaste no debe ser menor, que el que actualmente tiene el funcionario.",
                        new[] { "Sueldo" }));
                }
                #endregion

                #region CentroOperativoId
                //Valida si Existe un CentroOperativo cone el Id
                var centroOperativoId = contexto.CentroOperativos.Where(x => x.Id == CentroOperativoId);
                if (centroOperativoId.FirstOrDefault() == null)
                {
                    errores.Add(new ValidationResult("No existe un centro operativo con los datos ingresados.",
                        new[] { "CentroOperativoId" }));
                }
                else
                {
                    if (contratoActual.CentroOperativoId != CentroOperativoId)
                    {
                        HayCambios = true;
                    }
                }
                #endregion

                #region DivisionPoliticaNivel2Id
                var municipio = contexto.DivisionPoliticaNiveles2.Find(DivisionPoliticaNivel2Id);
                if (municipio == null)
                {
                    errores.Add(new ValidationResult("No existe un municipio con los datos ingresados.",
                        new[] { "DivisionPoliticaNivel2Id" }));
                }
                else
                {
                    if (contratoActual.DivisionPoliticaNivel2Id != null && DivisionPoliticaNivel2Id != null)
                    {
                        if (contratoActual.DivisionPoliticaNivel2Id != DivisionPoliticaNivel2Id)
                        {
                            HayCambios = true;
                        }
                    }
                }
                #endregion

                #region Observaciones
                //Generamos el mesaje si no hay cambios
                if (!HayCambios)
                {
                    errores.Add(new ValidationResult("No hay cambios respecto a las condiciones actuales del contrato.",
                                                    new[] { "ContratoId" }));
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
