using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Dependencias.Comandos.Parcial
{
    public class ParcialDependenciaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #region Activo
        public bool? Activo { get; set; }
        #endregion

        #endregion
        #region Validacion Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                #region Id
                // Elemento no existe
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var existe = dbContexto.Dependencias.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));

                    return errores;
                }
                #endregion
                #region Validacion de Activo 
                if (Activo != null)
                {
                    if (Activo == true)
                    {
                        var buscaPadre = dbContexto.DependenciaJerarquias.Where(x => x.DependenciaHijoId == Id).ToList();
                        if (buscaPadre != null)
                        {
                            foreach (var item in buscaPadre)
                            {
                                if (item.DependenciaPadreId != null)
                                {
                                    var validaEstadoPadre = dbContexto.Dependencias.FirstOrDefault(x => x.Id == item.DependenciaPadreId);

                                    if (validaEstadoPadre.EstadoRegistro == EstadoRegistro.Inactivo)
                                    {
                                        errores.Add(new ValidationResult(
                                                    $"No se puede activar. Debe primero activar la dependencia superior.",
                                                    new[] { "Id" })
                                                );
                                        return errores;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var buscaHijo = dbContexto.DependenciaJerarquias.Where(x => x.DependenciaPadreId == Id).ToList();
                        if (buscaHijo != null)
                        {
                            foreach (var item in buscaHijo)
                            {
                                var validaEstadoHijo = dbContexto.Dependencias.FirstOrDefault(x => x.Id == item.DependenciaHijoId);
                                if (validaEstadoHijo.EstadoRegistro == EstadoRegistro.Activo)
                                {
                                    errores.Add(new ValidationResult(
                                                $"No se puede inactivar. Debe primero inactivar la/s dependencia/s inferior/es.",
                                                new[] { "Id" })
                                            );
                                    return errores;
                                }

                            }

                        }
                    }

                }
                // Elemento Padre esta Activo

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
